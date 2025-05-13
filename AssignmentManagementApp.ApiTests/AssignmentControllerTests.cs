using AssignmentManagementApp.Api;
using AssignmentManagementApp.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AssignmentManagementApp.ApiTests
{
    public class AssignmentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _httpClient;

        public AssignmentControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task When_API_Create_Assignments_Then_It_Should_Succeed()
        {
            // Arrange
            var json = new StringContent(
                JsonSerializer.Serialize(new { title = "Simple Task", description = "do something simple" }),
                Encoding.UTF8, "application/json");

            // Act
            var createResponse = await _httpClient.PostAsync("/api/Assignment", json);

            // Assert
            createResponse.EnsureSuccessStatusCode();

            
        }

        [Fact]
        public async Task When_API_Get_Assignments_Then_It_Should_Return_Assignments_Lists()
        {
            // Arrange
            var json = new StringContent(
                JsonSerializer.Serialize(new { title = "Simple Task", description = "do something simple"}),
                Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/api/Assignment", json);
            
            
            var getResponse = await _httpClient.GetAsync("/api/Assignment");
            
            // Act
            var getAssignmentJson = await getResponse.Content.ReadAsStringAsync();
            var assignment = JsonSerializer.Deserialize<List<Assignment>>(getAssignmentJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assert
            getResponse.EnsureSuccessStatusCode();
            Assert.Contains(assignment, assignments => assignments.Title == "Simple Task");
        }
    }
}