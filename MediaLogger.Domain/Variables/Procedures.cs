namespace MediaLogger.Domain.Variables
{
    public static class Procedures
    {
        public static string EXEC { get { return "EXEC"; } }


        public static string SECURITY_SP_USERS_GETALL { get { return "[security].[SP_GetAllUsers]"; } }
        public static string SECURITY_SP_USERS_GETBYID { get { return "[security].[SP_GetUser]"; } }
        public static string SECURITY_SP_USERS_GETPWD { get { return "[security].[SP_GetPasswordUser]"; } }



        public static string BUSINESS_SP_PAYPAD_GETALL { get { return "[business].[SP_GetAllPayPad]"; } }
        public static string BUSINESS_SP_PAYPAD_GETPWD { get { return "[business].[SP_GetPasswordPaypad]"; } }
        public static string BUSINESS_SP_CLIENTS_GETALL { get { return "[business].[SP_GetAllClients]"; } }

        public static string BUSINESS_SP_GETLOGS { get { return "[SP_GetLog]"; } }
        public static string BUSINESS_SP_CREATELOGS { get { return "[SP_CreateLog]"; } }
        public static string BUSINESS_SP_UPDATELOGS { get { return "[SP_UpdateLog]"; } }


    }
}
