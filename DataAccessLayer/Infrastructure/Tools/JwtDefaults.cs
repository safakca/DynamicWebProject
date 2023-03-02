namespace DataAccessLayer.Infrastructure.Tools
{
    public class JwtDefaults
    {
        public const string ValidAudience = "http://localhost";
        public const string ValidIssuer = "http://localhost";
        public const string Key = "keykeykeykeykey4.";  //16 haneden kücük se IDX10653 bu hata cikiyor
        public const int Expire = 5;  //TODO: token yaşam süresi (saat,gün ayarı yapılacak !)
    }
}
