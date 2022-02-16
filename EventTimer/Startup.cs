using AgendaManager.Model.Context;
using AgendaManager.Services.IoC;
using AgendaManager.Services.Service;
using EventTimer.Model;
using EventTimer.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(EventTimer.Startup))]
namespace EventTimer
{
    public class Startup : FunctionsStartup
    {
        IConfiguration Configuration;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;

            var connectionString = Configuration["ConnectionString"];

            builder.Services.Configure<TimeConfig>(Configuration.GetSection("TimeConfig"));

            builder.Services.AddTransient<IInvitationService, InvitationService>();

            builder.Services.AddDbContext<AgendaContext>(op => SqliteDbContextOptionsBuilderExtensions.UseSqlite(op, connectionString));

            builder.Services.AddTransient<IEventTimeService, EventTimeService>();
            builder.Services.AddServicesRegistry();

        }
    }
}
