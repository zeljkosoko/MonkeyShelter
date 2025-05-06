using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Test
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            });

            builder.Configure(app =>
            {
                //Turn of authentication
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers(); // without UseAuthentication, UseAuthorization
                });
            });
        }
    }
}
