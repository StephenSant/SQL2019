using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
    #region Variables
    public InputField
        username,
        password,
        email,
        confPassword;
    public GameObject textField;
    public Text text;
    #endregion

    public void CreateUser()
    {
        if (password.text == confPassword.text)
        {
            StartCoroutine(CreateUser(username.text, password.text, email.text));
        }
        else
        {
            Debug.Log("Your passwords don't match.");
            text.text = "Your passwords don't match.";
        }
    }

    IEnumerator CreateUser(string _username, string _password, string _email)
    {
        string createUserURL = "http://localhost/squealsystem/InsertUser.php";

        WWWForm insertUserForm = new WWWForm();
        insertUserForm.AddField("usernamePost", _username);
        insertUserForm.AddField("passwordPost", _password);
        insertUserForm.AddField("emailPost", _email);

        WWW www = new WWW(createUserURL, insertUserForm);

        yield return www;

        Debug.Log(www.text);

        if(www.text == "User Already ExistsEmail Already Exists")
        {
            text.text = "User Already Exists";
        }
        else
        {
            text.text = www.text;
        }
        textField.SetActive(www.text != "");
    }


}
