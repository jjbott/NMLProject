using NMProject.Services.PatentView;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NMProject.Test.Services.PatentView
{
    [TestFixture]
    public class QueryBuilderTest
    {
        public static IEnumerable<object[]> CanGenerateQueryTestCases()
        {
            return new List<object[]>
            {
                new object[] {
                    new List<Criteria> {
                        new Criteria(Property.Title, Operation.Contains, "test")
                    },
                    "{\"_text_phrase\":{\"patent_title\":\"test\"}}"
                },
                new object[] {
                    new List<Criteria> {
                        new Criteria(Property.Title, Operation.EQ, "test")
                    },
                    "{\"_eq\":{\"patent_title\":\"test\"}}"
                },
                new object[] {
                    new List<Criteria> {
                        new Criteria(Property.Title, Operation.Contains, "test"),
                        new Criteria(Property.Date, Operation.GT, new DateTime(2017,1,1).ToString("yyyy-MM-dd"))
                    },
                    "{\"_and\":[{\"_text_phrase\":{\"patent_title\":\"test\"}},{\"_gt\":{\"patent_date\":\"2017-01-01\"}}]}"
                },
            };
        }

        [Test, TestCaseSource("CanGenerateQueryTestCases")]
        public void CanGenerateQuery(IEnumerable<Criteria> criteria, string expectedQuery)
        {
            var queryGenerator = new QueryGenerator();
            var queryString = queryGenerator.Create(criteria);
            Assert.AreEqual(expectedQuery, queryString);
        }
    }
}
