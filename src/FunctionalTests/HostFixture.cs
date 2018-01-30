using Api.Controllers;
using Microsoft.AspNetCore.TestHost;
using Respawn;
using System;
using System.Reflection;
using Xunit.Sdk;

namespace FunctionalTests
{
    public class HostFixture
        : IDisposable
    {
        private static readonly Checkpoint Checkpoint = new Checkpoint();

        public TestServer Server { get; private set; }

        public HostFixture()
        {
            Server = new TestServerBuilder()
                .WithStartup<TestStartup>()
                .Build();

            Server.Host.MigrateDbContext<FooContext>((ctx, sp) =>
            {
                ctx.Foo.Add(new Foo() { Bar = "MyBar" });
                ctx.SaveChanges();
            });

            Checkpoint.TablesToIgnore = new string[] { "MasterData" };
        }

        public void Dispose()
        {
            if (Server != null)
            {
                Server.Dispose();
            }
        }

        public static void ResetCheckpoint()
        {
            //TODO:use configuration
            Checkpoint.Reset("Server=.;Initial Catalog=FooDatabase;Integrated Security=true");
        }
    }
    public class ResetDatabase : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            HostFixture.ResetCheckpoint();
        }
    }
}
