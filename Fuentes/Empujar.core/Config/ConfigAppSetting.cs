using System;
using System.Collections.Generic;
using System.Text;

namespace Adrian.core.Config
{
    public static class ConfigAppSetting
    {
        public static string SeparadorDeListas { get; set; }
        public static string SeparadorDecimalProhibido { get; set; }
        public static string UserTypeAdmin { get; set; }
        public static string UserTypeBackEnd { get; set; }
        public static string UserTypeOperador { get; set; }
        public static string AppUrl { get; set; }
        public static string NotFoundMessage { get; set; }
        public static string DBErrorMessage { get; set; }
    }

}
