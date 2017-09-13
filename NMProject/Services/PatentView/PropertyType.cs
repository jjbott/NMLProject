using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public enum PropertyType
    {
        [ValidOperations(Operation.EQ, Operation.Contains, Operation.GT, Operation.GTE, Operation.LT, Operation.LTE)]
        String,
        [ValidOperations(Operation.EQ, Operation.Contains)] // More operations are techincally valid, but these make the most sense
        FullText,
        [ValidOperations(Operation.EQ, Operation.GT, Operation.GTE, Operation.LT, Operation.LTE)]
        Date,
        [ValidOperations(Operation.EQ, Operation.GT, Operation.GTE, Operation.LT, Operation.LTE)]
        Integer,
        [ValidOperations(Operation.EQ, Operation.GT, Operation.GTE, Operation.LT, Operation.LTE)]
        Float
    }
}
