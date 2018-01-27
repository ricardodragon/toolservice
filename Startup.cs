using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using toolservice.Actions;
using toolservice.Data;
using toolservice.Service;
using toolservice.Service.Interface;

namespace toolservice {
    public class Startup {

        public Startup (IConfiguration configuration) {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));
            services.AddSingleton<IConfiguration> (Configuration);
            services.AddSingleton<IThingService, ThingService> ();
            services.AddSingleton<IThingGroupService, ThingGroupService> ();
            services.AddTransient<IToolTypeService, ToolTypeService> ();
            services.AddTransient<IToolService, ToolService> ();
            services.AddTransient<IStateTransitionHistoryService, StateTransitionHistoryService> ();
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseNpgsql (Configuration.GetConnectionString ("ToolDB")));
            services.AddTransient<IStateManagementService, StateManagementService> ();
            services.AddTransient<IStateManagementService, StateManagementService> ();
            services.AddTransient<IAssociateToolService, AssociateToolService> ();
            services.AddSingleton<IList<IPostStateChangeAction>> (sp =>

                new List<IPostStateChangeAction> {

                    new TriggerAction (Configuration)

                }
            );

            services.AddMvc ().AddJsonOptions (options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseResponseCaching ();
            app.UseCors ("CorsPolicy");
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();

        }
    }
}