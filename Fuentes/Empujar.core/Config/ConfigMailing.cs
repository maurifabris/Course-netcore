using System;
using System.Collections.Generic;
using System.Text;

namespace Adrian.core.Config
{
    public class ConfigMailing
    {
        public static string From { get; set; }
        public static string SMTPServer { get; set; }
        public static string IMAPServer { get; set; }
        public static int SMTPPort { get; set; }
        public static int IMAPPort { get; set; }
        public static bool EnableSSL { get; set; }
        public static string User { get; set; }
        public static string Pass { get; set; }
    }
}
