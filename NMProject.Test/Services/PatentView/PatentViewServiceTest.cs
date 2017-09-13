using NMProject.Services.PatentView;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NMProject.Test.Services.PatentView
{
    [TestFixture]
    public class PatentViewServiceTest
    {
        [Test, Explicit]
        public void CanGetResults()
        {
            var service = new PatentViewService();
            var criterion = new Criteria[]
            {
                new Criteria(Property.Title, Operation.Contains, "test")
            };
            var task = service.SearchAsync(criterion);
            task.Wait();
        }
    }
}
