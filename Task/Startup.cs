
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task
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
            //services.AddIdentity<User, IdentityRole>();

            //services.AddCors(CorsOptions => CorsOptions.AddPolicy("MyPolicy",
            //  builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
            ////services.AddDbContext<DbContext>(option => {
            ////    option.UseSqlServer(Configuration.GetConnectionString("CS"),
            ////        options => options.EnableRetryOnFailure());
            //});
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task", Version = "v1" });
            });
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<UserManager<User>>();
            //services.AddTransient<RoleManager<IdentityRole>>();
            //services.AddTransient<AccountAppService>();
            //services.AddTransient<CategoryAppService>();
            services.AddHttpContextAccessor();//allow me to get user information such as id
           // services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
