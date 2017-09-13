using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class PatentViewService
    { 
        public async Task<PatentViewResult> SearchAsync(IEnumerable<Criteria> criterion)
        {
            var client = new HttpClient();
            
            var queryString = new QueryGenerator().Create(criterion);
            var resultJson = await client.GetStringAsync("http://www.patentsview.org/api/patents/query?q=" + queryString + "&f=[\"patent_id\", \"patent_title\",\"patent_abstract\",\"patent_date\",\"inventor_first_name\",\"inventor_last_name\"]&s=[{\"patent_title\":\"asc\"}]");
            return JsonConvert.DeserializeObject<PatentViewResult>(resultJson);
        }  
    }
}
