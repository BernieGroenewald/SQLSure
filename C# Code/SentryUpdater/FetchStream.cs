using System;
using System.IO;
using System.Net;
using System.Text;

namespace SentryUpdater
{
    public class FetchStream
    {
        public HttpWebResponse Response { get; private set; }
        public byte[] ResponseData { get; private set; }

        public void Load(string url)
        {
            try
            {
                var req = HttpWebRequest.Create(url) as HttpWebRequest;
                req.AllowAutoRedirect = false;

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
                            {
                                ms.WriteByte((byte)b);
                            }

                            ResponseData = ms.ToArray();
                        }

                        break;

                    default:
                        // This is unexpected.
                        Console.WriteLine(Response.StatusCode);
                        break;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(":Exception " + ex.Message);
                Response = ex.Response as HttpWebResponse;
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
