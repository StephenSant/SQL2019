using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public InputField username, 
                      password;
    

    public void EnterLogin()
    {
        StartCoroutine( LoginToDB(username.text,password.text));
    }

    IEnumerator LoginToDB (string _username, string _password)
    {
        string LoginURL = "http://localhost/squealsystem/Login.php";
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", _username);
        form.AddField("passwordPost", _password);
        WWW www = new WWW(LoginURL, form);
        yield return www;
        Debug.Log(www.text);
    }
}
