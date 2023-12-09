using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CourseApp.Integration.Tests.Commons
{
    public class TestsEnvWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        private string _env = "tests";

        public TestsEnvWebApplicationFactory(string env)
        {
            _env = env;
        }
        public TestsEnvWebApplicationFactory()
        {

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(_env);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", _env);
            base.ConfigureWebHost(builder);
        }
    }
}
