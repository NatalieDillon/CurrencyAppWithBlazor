using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCurrencyApp.classes
{
    public class RatesDto
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates {get; set;}

        public RatesDto() {
            Base = string.Empty;
            Rates = new Dictionary<string, decimal> { };    
        }

        
    }
}
