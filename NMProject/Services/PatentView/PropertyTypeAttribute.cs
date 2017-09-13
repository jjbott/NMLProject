using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class PropertyTypeAttribute : Attribute
    {
        public PropertyType Type { get; private set; }
        public PropertyTypeAttribute(PropertyType type)
        {
            Type = type;
        }
    }
}
