using System;
using System.IO;
using Xunit;

namespace Microsoft.Extensions.Configuration.Yaml.Test
{
    public class XmlSettingsLoaderTests
    {
        private static string WriteTempFile(string content)
        {
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".xml");
            File.WriteAllText(path, content);
            return path;
        }

        [Fact]
        public void LoadReadsElementNamesAndValues()
        {
            var path = WriteTempFile("<settings><name>service</name><port> 8080 </port></settings>");
            try
            {
                var values = XmlSettingsLoader.Load(path);

                Assert.Equal(2, values.Count);
                Assert.Equal("service", values["name"]);
                Assert.Equal("8080", values["port"]);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void LoadReturnsEmptyDictionaryForEmptyDocument()
        {
            var path = WriteTempFile("<settings />");
            try
            {
                Assert.Empty(XmlSettingsLoader.Load(path));
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
