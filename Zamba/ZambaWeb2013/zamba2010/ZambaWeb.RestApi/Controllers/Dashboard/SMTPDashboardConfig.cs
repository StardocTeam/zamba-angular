using System;

public static class SMTPDashboard
{
    public static string user { get; set; } = "emiliano.alvarez@stardoc.com.ar";
    public static string password { get; set; } = "K3YJFxd92Z4TOENv";
    public static string from { get; set; } = "emiliano.alvarez@stardoc.com.ar";
    public static string port { get; set; } = "587";
    public static string smtpServer{ get; set; } = "smtp-relay.brevo.com";
    public static Boolean enableSsl{ get; set; } = false;
}