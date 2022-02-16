using AgendaManager.Services.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Services.IoC
{
    public static class ServicesRegistry
    {
        public static void AddServicesRegistry( this IServiceCollection services )
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IInvitationService, InvitationService>();
        }
    }
}
