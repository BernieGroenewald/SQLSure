using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;

namespace SentryUpdater
{
    public class Fetch
    {
        public Fetch()
        {
            Headers = new WebHeaderCollection();
            Retries = 5;
            Timeout = 60000;
        }

        public WebHeaderCollection Headers { get; private set; }
        public HttpWebResponse Response { get; private set; }
        public NetworkCredential Credential { get; set; }
        public byte[] ResponseData { get; private set; }
        public int Retries { get; set; }
        public int Timeout { get; set; }
        public int RetrySleep { get; set; }
        public bool Success { get; private set; }

        public void Load(string url)
        {
            for (int retry = 0; retry < Retries; retry++)
            {
                try
                {
                    HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    
                    var req = HttpWebRequest.Create(url) as HttpWebRequest;
                    
                    IWebProxy proxy = req.Proxy;

                    if (proxy != null)
                    {
                        string proxyuri = proxy.GetProxy(req.RequestUri).ToString();
                        req.UseDefaultCredentials = true;
                        req.Proxy = new WebProxy(proxyuri, false);
                        req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        
                    }

                    req.CachePolicy = noCachePolicy;

                    req.AllowAutoRedirect = true;
                    ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;

                    if (Credential != null)
                    {
                        req.Credentials = Credential;
                    }

                    req.Headers = Headers;
                    req.Timeout = Timeout;

                    
                    Response = req.GetResponse() as HttpWebResponse;

                    switch (Response.StatusCode)
                    {
                        case HttpStatusCode.Found:
                            // This is a redirect to an error page, so ignore.
                            Console.WriteLine("Found (302), ignoring ");
                            break;

                        case HttpStatusCode.OK:
                            // This is a valid page.
                            using (var sr = Response.GetResponseStream())
                            using (var ms = new MemoryStream())
                            {
                                for (int b; (b = sr.ReadByte()) != -1;)
                                    ms.WriteByte((byte)b);
                                ResponseData = ms.ToArray();
                            }
                            break;

                        default:
                            // This is unexpected.
                            Console.WriteLine(Response.StatusCode);
                            break;
                    }

                    Success = true;
                    break;
                }
                catch (WebException ex)
                {
                    Console.WriteLine(":Exception " + ex.Message);
                    Response = ex.Response as HttpWebResponse;

                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        Thread.Sleep(RetrySleep);
                        continue;
                    }
                    break;
                }
            }
        }

        public static byte[] Get(string url)
        {
            var f = new Fetch();
            f.Load(url);
            return f.ResponseData;
        }

        public string GetString()
        {
            var encoder = string.IsNullOrEmpty(Response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(Response.ContentEncoding);

            if (ResponseData == null)
            {
                return string.Empty;
            }

            return encoder.GetString(ResponseData);
        }
    }
}
