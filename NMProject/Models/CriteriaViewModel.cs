using NMProject.Services.PatentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Models
{
    public class CriteriaViewModel
    {
        public Property Property { get; set; }
        public Operation Operation { get; set; }
        public string Value { get; set; }

    }
}
