namespace CaseStudy.Api;

public class ConnectionOptions
{
    public string Username { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Port { get; set; } = 0;
    public string Host { get; set; } = null!;
    public static string Position => "ConnectionString";
}