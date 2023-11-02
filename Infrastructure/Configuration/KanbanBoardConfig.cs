namespace KanbanBoard.Api.Utils.Configurations
{
    public static class KanbanBoardConfig
    {
        public static string JwtIssuer { get; set; } = string.Empty;
        public static string JwtAudience { get; set; } = string.Empty;
        public static string JwtSecretKey { get; set; } = string.Empty;


        public static void JwtConfig(IConfiguration configuration)
        {
            JwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? configuration.GetSection("Jwt")["Issuer"]!;
            JwtAudience = Environment.GetEnvironmentVariable("JwtAudience") ?? configuration.GetSection("Jwt")["Audience"]!;
            JwtSecretKey = Environment.GetEnvironmentVariable("JwtSecretKey") ?? configuration.GetSection("Jwt")["SecretKey"]!;
        }

    }
}