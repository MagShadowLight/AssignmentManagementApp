using AssignmentManagementApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManagementApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // GET: api/assignment
        [HttpGet]
        public IActionResult GetAllAssignment() => Ok(_assignmentService.ListAll());

        // POST: api/assignment
        [HttpPost]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            var createdAssignment = _assignmentService.AddAssignment(assignment);
            return CreatedAtAction(nameof(GetAllAssignment), createdAssignment);
        }

        // DELETE: api/assignment/delete
        [HttpDelete("{Title}")]
        public IActionResult DeleteAssignment(String Title)
        {
            Console.WriteLine($"{Title}");
            _assignmentService.DeleteAssignment(Title);
            return NoContent();
        }

    }
}
