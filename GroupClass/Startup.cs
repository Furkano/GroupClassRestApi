using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Services.ClassUseCase;
using GroupClass.Core.Services.MemberUseCase;
using GroupClass.Core.Services.PostUseCase;
using GroupClass.Core.Services.UserUseCase;
using GroupClass.Infrastructure.Extensions;
using GroupClass.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GroupClass
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureMySqlContext(Configuration);
            services.ConfigureRepositoryWrapper();
            services.JwtAuthentication(Configuration);
            services.PolicyAuthorization();

            services.AddControllers();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            //User
            services.AddMediatR(typeof(CreateUserHandler));
            services.AddMediatR(typeof(UpdateUserHandler));
            services.AddMediatR(typeof(LoginUserHandler));
            //Post
            services.AddMediatR(typeof(CreatePostHandler));
            services.AddMediatR(typeof(UpdatePostHandler));
            services.AddMediatR(typeof(DeletePostHandler));
            //Class
            services.AddMediatR(typeof(CreateClassHandler));
            services.AddMediatR(typeof(GetClassWithEducationYearHandler));
            services.AddMediatR(typeof(DeleteClassHandler));
            services.AddMediatR(typeof(GetClassWithCodeHandler));
            //Member
            services.AddMediatR(typeof(AddMemberHandler));
            services.AddMediatR(typeof(DeleteMemberHandler));
            services.AddMediatR(typeof(GetClassMembersHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
