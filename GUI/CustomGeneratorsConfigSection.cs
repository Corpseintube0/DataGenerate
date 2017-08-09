using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GUI
{
    public class CustomGeneratorsConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("CG")]
        public CustomGeneratorCollection CustomGeneratorItems
        {
            get { return ((CustomGeneratorCollection)(base["CG"])); }
        }
    }
}
