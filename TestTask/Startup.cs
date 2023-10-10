using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Data.Entities;
using TestTask.DataAccess.Abstractions;
using TestTask.DataAccess.Implementations;

namespace TestTask
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var ConfigurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddInMemoryCollection();

            Configuration = ConfigurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
