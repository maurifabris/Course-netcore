using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adrian.core.Config
{
    public static class ConfigSecurity
    {
        public static string AppName { get; set; }
        public static int RequiredLength { get; set; }
        public static bool RequireNonLetterOrDigit { get; set; }
        public static bool RequireDigit { get; set; }
        public static bool RequireLowercase { get; set; }
        public static bool RequireUppercase { get; set; }
        public static int MaxFailedAccessAttempts { get; set; }
        public static int LockoutTime { get; set; }
        public static bool RolesAlreadyCreated { get; set; }
        public static string FirstUser { get; set; }
    }
}
