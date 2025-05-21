﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Core.Interfaces
{
    public interface IAssignmentService
    {
        public bool AddAssignment(Assignment assignment);
        public List<Assignment> ListAll();
        public List<Assignment> ListIncomplete();
        public List<Assignment> ListAssignmentsByPriority(Priority priority);
        public Assignment FindAssignmentByTitle(string title);
        public bool MarkAssignmentComplete(string title);
        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription, Priority newPriority);
        public bool DeleteAssignment(string title);
    }
}
