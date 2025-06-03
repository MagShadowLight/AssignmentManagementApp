
using AssignmentManagementApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Core
{
    public class AssignmentService : IAssignmentService
    {

        private readonly IAssignmentFormatter _assignmentFormatter;
        private readonly IAppLogger _logger;

        public AssignmentService(IAssignmentFormatter assignmentFormatter, IAppLogger logger)
        {
            _assignmentFormatter = assignmentFormatter;
            _logger = logger;
        }

        private readonly List<Assignment> assignments = new();

        public bool AddAssignment(Assignment assignment)
        {
            if (assignment == null)
            {
                _logger.Log("Error: title is blank");
                throw new ArgumentNullException(nameof(assignment));
            }


            assignments.Add(assignment);
            return true;

        }

        public List<Assignment> ListAll()
        {

            return assignments;
        }

        public List<Assignment> ListIncomplete()
        {
            return assignments.Where(a => !a.IsComplete).ToList();
        }

        public Assignment FindAssignmentByTitle(string title)
        {
            return assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool MarkAssignmentComplete(string title)
        {
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
            {
                return false;
            }

            assignment.MarkComplete();
            return true;
        }

        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription, Priority newPriority, string newNote)
        {
            var assignment = FindAssignmentByTitle(oldTitle);
            if (assignment == null)
            {
                return false;
            }

            if (!oldTitle.Equals(newTitle, StringComparison.OrdinalIgnoreCase) && assignments.Any(a => a.Title.Equals(newTitle, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            assignment.Update(newTitle, newDescription, newPriority, newNote);
            return true;
        }

        public bool DeleteAssignment(string title)
        {
            var assignment = FindAssignmentByTitle(title);
            if (assignment == null)
            {
                return false;
            }

            assignments.Remove(assignment);
            return true;
        }

        public List<Assignment> ListAssignmentsByPriority(Priority priority)
        {
            return assignments.Where(a => a.Priority == priority).ToList();
        }
        
    }
}
