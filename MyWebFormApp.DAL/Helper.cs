using Microsoft.Extensions.Configuration;

namespace MyWebFormApp.DAL
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["LatihanDbConnectionString"] == null)
            {
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return MyConfig.GetConnectionString("LatihanDbConnectionString");
            }
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["LatihanDbConnectionString"].ConnectionString;
            return connString;
        }
    }
}
