using CourseApp.Integration.Tests.Commons;
using Newtonsoft.Json.Linq;

namespace CourseApp.Integration.Tests
{
    [TestClass]
    public class AuthTests
    {
        private readonly TestsEnvWebApplicationFactory<CoursesApp.Program> _factor;
        private const string TEST_URI_EXAMPLE = "/api/v1/accounts";
        private const string TEST_URI_HEALTHCHECK = "/hc";
        private const string TEST_AUTH_SCHEME = "Bearer";
        private const string TEST_PROPER_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJodHRwczovL3NlbGxpbnRlZ3JvLmNsb3VkIiwiYXVkIjoiaHR0cHM6Ly9kZXZlbG9wbWVudC5zZWxsaW50ZWdyby5jbG91ZCIsInN1YiI6IjEyMzQ1Njc4OTAiLCJuYW1lIjoiSm9obiBEb2UiLCJpYXQiOjE1MTYyMzkwMjJ9.29NAhGZgaizQizeEDqudZQjcy_jDPzElXVocoj_fQ7A";
        private const string TEST_INVALID_TOKEN = "ey";

        public AuthTests()
        {
            _factor = new TestsEnvWebApplicationFactory<CoursesApp.Program>();
        }

        [TestMethod]
        public async Task AuthTest_ProperJwt_ShouldPass()
        {
            using var client = _factor.CreateClient();
            client.DefaultRequestHeaders.Authorization = new(TEST_AUTH_SCHEME, TEST_PROPER_TOKEN);
            var response = await client.GetAsync(TEST_URI_EXAMPLE);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task AuthTest_HealthCheckController_NoAuth_ShouldPassWith200OK()
        {
            using var client = _factor.CreateClient();
            client.DefaultRequestHeaders.Authorization = new(TEST_AUTH_SCHEME, TEST_INVALID_TOKEN);
            var response = await client.GetAsync(TEST_URI_HEALTHCHECK);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            var res = JObject.Parse(await response.Content.ReadAsStringAsync());
            Assert.AreEqual("Healthy", res["status"]?.Value<string>());
        }
    }
}