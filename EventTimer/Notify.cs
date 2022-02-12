using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTimer.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace EventTimer
{
    public class Notify
    {
        private readonly IEventTimeService _service;

        public Notify(IEventTimeService service)
        {
            _service = service;
        }

        [FunctionName("Notify")]
        public async Task Run([TimerTrigger("*/1 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var isSent = await _service.NotifyByEmail(2);

            if( isSent )
            {
                log.LogInformation("Email Sended");
            }

            log.LogInformation("Waiting for another event.....");
        }
    }
}
