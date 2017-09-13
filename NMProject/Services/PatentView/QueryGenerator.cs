using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMProject.Services.PatentView
{
    public class QueryGenerator
    {
        public string Create(IEnumerable<Criteria> criterion)
        {
            var queryObjects = criterion.Select(c => CriteraToQueryObject(c)).ToList();
            if (queryObjects.Count == 1)
            {
                return JsonConvert.SerializeObject(queryObjects[0]);
            }

            var queryObject = new Dictionary<string, object>
            {
                ["_and"] = queryObjects
            };
            return JsonConvert.SerializeObject(queryObject);
        }

        private string GetCriteriaOperationText(Criteria criteria)
        {
            switch(criteria.Operation)
            {
                case Operation.Contains:
                    return criteria.Property.Type() == PropertyType.FullText ? "_text_phrase" : "_contains";
                case Operation.EQ:
                    return "_eq";
                case Operation.GT:
                    return "_gt";
                case Operation.GTE:
                    return "_gte";
                case Operation.LT:
                    return "_lt";
                case Operation.LTE:
                    return "_lte";
                default:
                    throw new ArgumentException($"Can not get apit text fro operation {criteria.Operation} and property type {criteria.Property.Type()}");
            }
        }

        private object CriteraToQueryObject(Criteria criteria)
        {
            var queryObject = new Dictionary<string, object>
            {
                [GetCriteriaOperationText(criteria)] = new Dictionary<string, object>() { { criteria.Property.ApiName(), criteria.Value } }
            };
            return queryObject;
        }
    }
}
