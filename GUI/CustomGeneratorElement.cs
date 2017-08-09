using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GUI
{
    public class CustomGeneratorElement : ConfigurationElement
    {
        [ConfigurationProperty("generatorName", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string CustomName
        {
            get { return ((string)(base["generatorName"])); }
            set { base["generatorName"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
