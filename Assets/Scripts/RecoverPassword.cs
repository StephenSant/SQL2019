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
    public InputField email, codeCheck;
    public InputField enterPassword, reEnterPassword;

    public GameObject panel1, panel2, panel3;

    public void FindUser()
    {
        StartCoroutine(FindUsername(email.text));
    }

    public void SendEmail(string email, string username)
    {
        code = RandomString(6);
        MailMessage mail = new MailMessage();
        mail.To.Add(email);
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
        Debug.Log("Email sent");
        
        OpenPanel(2);
    }

    

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public void CheckCode()
    {
        if (codeCheck.text == code)
        {
            Debug.Log("Success");
            OpenPanel(3);
        }
        else
        {
            Debug.Log("Failed");
        }
    }

    public void ChangePasswordFunction()
    {
        if (enterPassword.text == reEnterPassword.text)
        {
            StartCoroutine(ChangePassword(enterPassword.text,email.text));
        }
        else
        {
            Debug.Log("Passwords must be the same!");
        }
    }

    IEnumerator FindUsername(string _email)
    {
        string FindUserURL = "http://localhost/squealsystem/CheckUser.php";
        WWWForm form = new WWWForm();
        form.AddField("emailPost", _email);
        WWW www = new WWW(FindUserURL, form);
        yield return www;
        Debug.Log(www.text);
        username = www.text;

        SendEmail(_email, username);
    }

    IEnumerator ChangePassword(string _newPassword,string _email)
    {
        string ChangePasswordURL = "http://localhost/squealsystem/UpdatePassword.php";
        WWWForm form = new WWWForm();
        form.AddField("passwordPost", _newPassword);
        form.AddField("emailPost", _email);
        WWW www = new WWW(ChangePasswordURL, form);
        yield return www;
        Debug.Log(www.text);
        if (www.text == "Password Changed")
        {
            Debug.Log("Good Job!");
        }
        Debug.Log(www.text);
    }

    public void OpenPanel(int panel)
    {
        switch (panel)
        {
            case 1:
                panel1.SetActive(true);
                panel2.SetActive(false);
                panel3.SetActive(false);
                break;
            case 2:
                panel1.SetActive(false);
                panel2.SetActive(true);
                panel3.SetActive(false);
                break;
            case 3:
                panel1.SetActive(false);
                panel2.SetActive(false);
                panel3.SetActive(true);
                break;
            default:
                Debug.LogError("Can only open panel 1, 2 or 3.");
                break;
        }
    }
}
