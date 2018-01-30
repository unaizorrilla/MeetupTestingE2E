using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionalTests
{
    class TestServerBuilder
    {
        Type _startupType;

        public TestServerBuilder WithStartup<TStartup>()
            where TStartup:class
        {
            _startupType = typeof(TStartup);

            return this;
        }

        public TestServer Build()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup(_startupType);

            return new TestServer(webHostBuilder);
        }
    }
}
