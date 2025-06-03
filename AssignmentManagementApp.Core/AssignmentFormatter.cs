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
            return $"[{assignment.Id}] {assignment.Title} - {assignment.Description} - Priority: {assignment.Priority} - {(assignment.IsComplete ? "Completed" : "Incomplete")} - Due Date: {assignment.DueDate}\n Notes: {assignment.Notes}";
        }
    }
}
