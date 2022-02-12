using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Core.Model
{
    public class EmailModel
    {
        public string Message { get; set; }

        public List<string> Destinataries { get; set; }
    }

    public class EmailConfig
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string DisplayName { get; set; }
    }
}
