using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class Inventor
    { 
        [JsonProperty(propertyName: "inventor_first_name")]
        public string InventorFirstName { get; set; }

        [JsonProperty(propertyName: "inventor_last_name")]
        public string InventorLastName { get; set; }
    }
}
