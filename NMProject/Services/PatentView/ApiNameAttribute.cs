using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class ApiNameAttribute : Attribute
    {
        public string Name { get; private set; }
        public ApiNameAttribute(string name)
        {
            Name = name;
        }
    }
}
