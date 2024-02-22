using MediaLogger.Aplication.BL;

namespace MediaLoggerAPI.Middleware
{
    public class MediaMiddleware
    {
        private Token _token;
        public MediaMiddleware(Token token) 
        {
            _token = token;
        }

        public bool IsValidJwt(string token)
        {         
            try
            {
                if (string.IsNullOrEmpty(token)) return false;
                return _token.IsJwtTokenValid(token);
            }
            catch
            {
                return false;
            }
        }
    }
}
