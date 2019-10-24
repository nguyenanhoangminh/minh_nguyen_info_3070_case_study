using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Xunit;
using HelpdeskViewModels;
namespace CaseStudyTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            List<DepartmentViewModel> AllVms = vm.GetAll();
            Assert.NotEmpty(AllVms);

        }
    }
}
