using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class ValidOperationsAttribute : Attribute
    {
        public List<Operation> ValidOperations { get; private set; }
        public ValidOperationsAttribute(params Operation[] operations)
        {
            ValidOperations = new List<Operation>(operations);
        }
    }
}
