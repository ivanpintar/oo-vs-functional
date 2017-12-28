using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PinetreeChat.DataAccess.Repositories;
using PinetreeChat.Domain.Services;
using PinetreeChat.Interfaces.Repositories;
using PinetreeChat.WebAPI.SignalR;

namespace PinetreeChat.WebAPI
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
            services.AddCors();
            services.AddMvc();
            services.AddSignalR();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<UserService, UserService>();
            services.AddTransient<ChatService, ChatService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
            app.UsePathBase("/PinetreeChat.CSharpAPI");
            app.UseMvc();
            app.UseSignalR(routes => routes.MapHub<ChatHub>("chatHub"));
        }
    }
}
