using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ChessViewer.Data;
using ChessViewer.Services.Static;

namespace ChessViewer.UI
{
    public static class AppConfig
    {
        public static void InitDatabase() 
        {
            string scriptPath = HttpContext.Current.Server.MapPath("~/App_Data/DataInitScript.sql");
            string initScript = null;
            if (File.Exists(scriptPath))
                initScript = File.ReadAllText(scriptPath);

            new SQLiteHelper(AppSettings.ConnectionString).InitDatabase(initScript);
        }
    }
}