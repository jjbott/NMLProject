using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class PatentViewResult
    {
        [JsonProperty("patents")]
        public IEnumerable<Patent> Patents { get; set; }
        [JsonProperty(propertyName: "count")]
        public int Count { get; set; }
        [JsonProperty(propertyName: "total_patent_count")]
        public int Total { get; set; }

    }
}
