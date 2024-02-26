using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Variables
{
    public static class AppSettings
    {
        public static string SALT_INTERNAL { get { return "App:InternalSaltKey"; } }
        public static string CORS_NAME { get { return "MediaLoggerSecurityPolicy"; } }
        public static string Login_Api_Key { get { return "Api-Key:Login-Key"; }  }
        public static string JwtSecret { get { return "Jwt:Secret"; } }
        public static string VideosDirectory { get { return "Directories:Videos"; } }
        public static string connectionMongo { get {return "ConnectionStrings:MongoDb"; } }
        public static string ConnetionDashboardSQL { get { return "ConnectionStrings:Dashboard"; } }
        public static string ConnetionLogsSQL { get { return "ConnectionStrings:Logs"; } }
        public static string DataBase { get { return "MongoDb:DataBase"; } }
        public static string Colletion { get { return "MongoDb:Colletion"; } }
    }
}
