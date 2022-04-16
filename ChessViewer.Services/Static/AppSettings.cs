using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ChessViewer.Services.Static
{
    public static class AppSettings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["GameDb"].ConnectionString;
    }
}
