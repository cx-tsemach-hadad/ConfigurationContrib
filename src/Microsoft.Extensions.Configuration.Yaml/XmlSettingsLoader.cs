using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Extensions.Configuration.Yaml
{
    /// <summary>
    /// Loads flat settings from an XML file. Every child element of the root element becomes
    /// one entry, keyed by the element name, with the element text as value.
    /// </summary>
    public static class XmlSettingsLoader
    {
        /// <summary>
        /// Loads the settings stored in the given XML file.
        /// </summary>
        /// <param name="path">The path of the XML file.</param>
        /// <returns>A dictionary of element names to element values.</returns>
        public static Dictionary<string, string> Load(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Parse(path);
        }

        private static Dictionary<string, string> Parse(string path)
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null
            };

            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (var reader = XmlReader.Create(path, settings))
            {
                var document = XDocument.Load(reader);
                foreach (var element in document.Root.Elements())
                {
                    values[element.Name.LocalName] = element.Value.Trim();
                }
            }

            return values;
        }
    }
}
