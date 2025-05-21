using AssignmentManagementApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Core
{
    public class AssignmentFormatter : IAssignmentFormatter
    {
        public string Format(Assignment assignment)
        {
            return $"[{assignment.Id}] {assignment.Title} - Priority: {assignment.Priority} - {(assignment.IsComplete ? "Completed" : "Incomplete")}";
        }
    }
}
