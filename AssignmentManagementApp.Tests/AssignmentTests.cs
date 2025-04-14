using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Tests
{
    public class AssignmentTests
    {

        [Fact]
        public void Assignment_Should_Have_A_Title()
        {
            var assignment = new Assignment();
            Assert.NotNull(assignment.Title);
        }

    }
}
