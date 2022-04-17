using System;
using System.Configuration;

namespace ChessViewer.Services.Static
{
    public static class AppSettings
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["GameDb"].ConnectionString;
    }
}
