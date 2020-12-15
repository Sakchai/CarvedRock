using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL;
using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YesSql.Provider.SqlServer;

namespace CarvedRock.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("CarvedRock");
            services.AddDbContext<CarvedRockDbContext>(options =>
               options.UseSqlServer(connection), ServiceLifetime.Scoped);

            // this add in YesSql Db
            services.AddDbProvider(options =>options.UseSqlServer(connection));

            services
                        .AddSingleton<ISchema, CarvedRockSchema>()
                        .AddSingleton<IReviewMessageService, ReviewMessageService>()
                        .AddScoped<IProductRepository, ProductRepository>()
                        .AddScoped<IProductReviewRepository, ProductReviewRepository>()
                        .AddScoped<ICustomerRepository, CustomerRepository>()
                        .AddSingleton<ProductType>()
                        .AddGraphQL((options, provider) =>
                        {
                            options.EnableMetrics = Environment.IsDevelopment();
                            var logger = provider.GetRequiredService<ILogger<Startup>>();
                            options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
                        })

                        .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                        .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                        .AddWebSockets()
                        .AddDataLoader()
                        .AddGraphTypes(typeof(CarvedRockSchema));

            // this is used to use DI in GraphQl for scoped services
            services.AddDefer();
            services.AddHttpScope();

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        //public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(builder =>
            //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //app.UseWebSockets();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ISchema>();
            app.UseGraphQL<ISchema>();

            // ui/graphiql
            app.UseGraphiQLServer();

            // ui/playground
            app.UseGraphQLPlayground();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
        }
    }
}