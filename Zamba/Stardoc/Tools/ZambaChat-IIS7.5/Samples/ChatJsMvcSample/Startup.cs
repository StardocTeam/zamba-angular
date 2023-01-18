using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using System.Web.Cors;
using System.Web.Http;
using ChatJsMvcSample.App_Start;
using System.Net.WebSockets;
using Microsoft.AspNet.WebSockets;
using Microsoft.AspNet.WebSockets.Server;
using Microsoft.Web.WebSockets;
using Microsoft.AspNet.Builder;

[assembly: OwinStartup(typeof(ChatJsMvcSample.Startup))]

namespace ChatJsMvcSample
{
    public class Startup
    {
        //private static readonly Lazy<CorsOptions> SignalrCorsOptions = new Lazy<CorsOptions>(() =>
        //{
        //    return new CorsOptions
        //    {
        //        PolicyProvider = new CorsPolicyProvider
        //        {
        //            PolicyResolver = context =>
        //            {
        //                var policy = new CorsPolicy();
        //                policy.AllowAnyOrigin = true;
        //                policy.AllowAnyMethod = true;
        //                policy.AllowAnyHeader = true;
        //                policy.SupportsCredentials = false;
        //                return Task.FromResult(policy);
        //            }
        //        }
        //    };
        //});

        public void Configuration(IAppBuilder app)
        {

            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(SignalrCorsOptions.Value);
            //    map.RunSignalR(new HubConfiguration());
            //});
            ////now start the WebAPI app
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            //app.UseWebSockets();
            var config = new HubConfiguration();
            config.EnableJSONP = true;


            //app.UseCors(CorsOptions.AllowAll);

            config.EnableJavaScriptProxies = true;
            app.MapSignalR(config);
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(new HubConfiguration { EnableJSONP = true });
            });
        }

        //public void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddMvc();
        //    //Add Cors support to the service
        //    services.AddCors();

        //    var policy = new Microsoft.AspNet.Cors.Core.CorsPolicy();

        //    policy.Headers.Add("*");
        //    policy.Methods.Add("*");
        //    policy.Origins.Add("*");
        //    policy.SupportsCredentials = true;

        //    services.ConfigureCors(x => x.AddPolicy("AllPolicy", policy));

        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    // Configure the HTTP request pipeline.

        //    app.UseStaticFiles();
        //    //Use the new policy globally
        //    app.UseCors("AllPolicy");
        //    // Add MVC to the request pipeline.
        //    app.UseMvc();
        //}
    }
}
