using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SentryUpdater
{
    internal class Manifest
    {
        private string _data;

        public Manifest(string data)
        {
            Load(data);
        }

        public int Version { get; private set; }
        public int CheckInterval { get; private set; }
        public string RemoteConfigUri { get; private set; }
        public string SecurityToken { get; private set; }
        public string BaseUri { get; private set; }
        public string BaseFileName { get; private set; }
        public string BaseFileExtension { get; private set; }
        public string[] Payloads { get; private set; }

        private void Load(string data)
        {
            _data = data;

            try
            {
                // Load config from XML

                var xml = XDocument.Parse(data);

                if (xml.Root.Name.LocalName != "Manifest")
                {
                    Log.Write("Root XML element '{0}' is not recognized, stopping.", xml.Root.Name);
                    return;
                }

                // Set properties.

                Version = int.Parse(xml.Root.Attribute("version").Value);
                CheckInterval = int.Parse(xml.Root.Element("CheckInterval").Value);
                SecurityToken = xml.Root.Element("SecurityToken").Value;
                //RemoteConfigUri = xml.Root.Element("RemoteConfigUri").Value;
                BaseFileName = xml.Root.Element("BaseFileName").Value;
                BaseFileExtension = xml.Root.Element("BaseFileExtension").Value;
                BaseUri = xml.Root.Element("BaseUri").Value;
                RemoteConfigUri = BaseUri + BaseFileName.Trim() + (Version + 1).ToString() + BaseFileExtension;
                Payloads = xml.Root.Elements("Payload").Select(x => x.Value).ToArray();
            }

            catch (Exception ex)
            {
                Log.Write("Error: {0}", ex.Message);
                return;
            }
        }

        public void Write(string path)
        {
            File.WriteAllText(path, _data);
        }
    }
}
