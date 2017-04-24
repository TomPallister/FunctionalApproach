using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FunctionalApproach
{
    using DataAccess;
    using Functions;
    using Model;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Func<Func<int, Value, Value>> databaseUpdate = Database.Update;
            Func<int, Value, Value> update = ValueFunctions.Update(databaseUpdate);

            Func<Action<int>> databaseDelete = Database.Delete;
            Action<int> delete = ValueFunctions.Delete(databaseDelete);

            Func<Func<IEnumerable<Value>>> databaseGetAll = Database.ReadAll;
            Func<IEnumerable<Value>> readAll = ValueFunctions.ReadAll(databaseGetAll);

            Func<Func<int, Value>> databaseGetById = Database.ReadById;
            Func<int, Value> readById = ValueFunctions.ReadAllById(databaseGetById);

            Func<Func<Value, Value>> databaseCreate = Database.Create;
            Func<Value, Value> create = ValueFunctions.Create(databaseCreate);

            services.AddSingleton(readAll);
            services.AddSingleton(readById);
            services.AddSingleton(create);
            services.AddSingleton(update);
            services.AddSingleton(delete);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
