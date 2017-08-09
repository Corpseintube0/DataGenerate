using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GUI
{
    [ConfigurationCollection(typeof(CustomGeneratorElement))]
    public class CustomGeneratorCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CustomGeneratorElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CustomGeneratorElement)(element)).CustomName;
        }

        public CustomGeneratorElement this[int idx]
        {
            get { return (CustomGeneratorElement)BaseGet(idx); }
        }
    }
}
