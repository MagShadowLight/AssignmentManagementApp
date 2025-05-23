﻿using AssignmentManagementApp.Core;
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
        public void Constructor_ValidInput_ShouldCreateAssignment()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Equal("Read Chapter 2", assignment.Title);
            Assert.Equal("Summarize key points", assignment.Description);
        }

        [Fact]
        public void Constructor_BlankTitle_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Assignment("", "Valid description"));
        }

        [Fact]
        public void Update_BlankTitleOrDescription_ShouldThrowException()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", ""));
            Assert.Throws<ArgumentException>(() => assignment.Update("", "Valid description"));
        }
    }
}
