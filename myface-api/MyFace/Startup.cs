using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyFace.Repositories;

namespace MyFace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static string CORS_POLICY_NAME = "_myfaceCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyFaceDbContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlite("Data Source=myface.db");
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    CORS_POLICY_NAME,
                    builder =>
                        builder
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                );
            });

           

            services.AddControllers();

            services.AddTransient<IInteractionsRepo, InteractionsRepo>();
            services.AddTransient<IPostsRepo, PostsRepo>();
            services.AddTransient<IUsersRepo, UsersRepo>();
            

                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(CORS_POLICY_NAME);

            app.UseMiddleware<Helpers.BasicAuth>();

            app.UseHttpsRedirection();

            app.UseRouting();            

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
