
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp
{
    public class AssignmentService
    {
        private readonly List<Assignment> assignments = new();

        public void AddAssignment(Assignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            assignments.Add(assignment);
        }

        public List<Assignment> ListAll()
        {
            return assignments;
        }

        public List<Assignment> ListIncomplete()
        {
            return assignments.Where(a => !a.IsComplete).ToList();
        }
    }
}
