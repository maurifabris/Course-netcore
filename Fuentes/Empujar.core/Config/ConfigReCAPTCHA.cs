using System;
using System.Collections.Generic;
using System.Text;

namespace Adrian.core.Config
{
    public static class ConfigReCAPTCHA
    {
        public static string SiteKey { get; set; }
        public static string SecretKey { get; set; }
        public static string VerifyUrl { get; set; }
        public static decimal MinAllowedScore { get; set; }
    }

}
