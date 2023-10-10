using Serilog;

namespace TestTask
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, lc) =>
                {
                    lc.MinimumLevel.Information().WriteTo.Console();
                    //lc.MinimumLevel.Warning().WriteTo.File(@"Logger\log.log");

                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}