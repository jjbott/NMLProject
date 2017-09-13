using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class Criteria
    {
        public Property Property { get; private set; }
        public Operation Operation { get; private set; }
        public string Value { get; private set; }

        public Criteria(Property property, Operation operation, string value)
        {
            Property = property;
            Operation = operation;
            Value = value;

        }

        public Criteria(string property, string operation, string value)
        {
            Property = Enum.GetValues(typeof(Property)).Cast<Property>().Where(p => p.ApiName() == property).Single();
            Operation = Enum.GetValues(typeof(Operation)).Cast<Operation>().Where(o => o.ToString() == operation).Single();
            Value = value;
        }
    }
}
