using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using UnityEngine.UI;

public class RecoverPassword : MonoBehaviour
{
    private static System.Random random = new System.Random();
    public string code;
    public string username;
    public InputField email;
    public void SendEmail()
    {
        code = RandomString(6);
        MailMessage mail = new MailMessage();
        mail.To.Add(email.text);
        mail.From = new MailAddress("stephen.sant123@gmail.com", "E-mail Bot 3000");
        mail.Subject = "SQueaL Password Reset";
        mail.Body = "Hello " + username + ",\nTo reset your password enter this code into your game: " + code;
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new System.Net.NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
        { return true; };

        smtpServer.Send(mail);
        Debug.Log("Success");
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
