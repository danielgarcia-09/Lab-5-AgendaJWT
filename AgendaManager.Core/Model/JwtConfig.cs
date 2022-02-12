using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Core.Model
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }

        public int ExpirationInMinutes { get; set; } 

    }
}
