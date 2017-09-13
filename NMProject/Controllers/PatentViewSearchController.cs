using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NMProject.Models;
using Newtonsoft.Json;
using NMProject.Services.PatentView;
using System.Collections;
using Microsoft.AspNetCore.Cors;

namespace NMProject.Controllers
{
    [Route("api/[controller]")]
    public class PatentViewSearchController : Controller
    {
        [EnableCors("http://localhost:5000")]
        [HttpGet]
        public async Task<IEnumerable<Patent>> Get(string queryJson)
        {
            // /api/PatentViewSearch?queryJson=[["patent_title", "Contains", "test"],["patent_title", "Contains", "thing"]]

            var query = JsonConvert.DeserializeObject<IEnumerable<string[]>>(queryJson);
            var criterion = query.Select(c => new Criteria(c[0], c[1], c[2])).ToList();
            var service = new PatentViewService();
            var searchResults = await service.SearchAsync(criterion);
            return searchResults.Patents ?? new List<Patent>();
        }
    }
}
