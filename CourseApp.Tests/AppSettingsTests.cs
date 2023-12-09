using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Resources = CourseApp.Tests.Properties.Resources;
namespace CourseApp.Tests
{
    [TestClass]
    public class AppSettingsTests
    {
        [TestMethod]
        public void Ensure_AppSettings_MatchSchema()
        {
            var schema = JSchema.Parse(Resources.appsettingsSchema_v1_json);
            var settings = JObject.Parse(Resources.projectAppSettings_json);

            var isValid = settings.IsValid(schema, out IList<string> errorsList);
            Assert.IsTrue(isValid, JToken.FromObject(errorsList).ToString());

        }
    }
}