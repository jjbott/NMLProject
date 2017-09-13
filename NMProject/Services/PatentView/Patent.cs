using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class Patent
    {
        [JsonProperty("patent_id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "patent_title")]
        public string Title { get; set; }

        [JsonProperty(propertyName: "patent_abstract")]
        public string Abstract { get; set; }

        [JsonProperty(propertyName: "patent_date")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime DateGranted { get; set; }

        [JsonProperty(propertyName: "inventor_first_name")]
        public string InventorFirstName { get { return Inventors.First().InventorFirstName; } set { Inventors.First().InventorFirstName = value; } }

        [JsonProperty(propertyName: "inventor_last_name")]
        public string InventorLastName { get { return Inventors.First().InventorLastName; } set { Inventors.First().InventorLastName = value; } }

        public Inventor[] Inventors { get; set; }
    }
}
