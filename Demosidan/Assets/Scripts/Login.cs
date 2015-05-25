using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Login : MonoBehaviour 
{
    public InputField usernameInput;
    public InputField passwordInput;

    private static bool isLoggedIn = false;
    private static string username;
    private static string password;
    private Dictionary<string,string> header = new Dictionary<string,string>();

    private string url = "http://www.metalgenre.se/api/achievements/GetUser.php";

    public static bool IsLoggedIn
    {
        get { return Login.isLoggedIn; }
    }
    public static string Username
    {
        get { return Login.username; }
    }
    public static string Password
    {
        get { return Login.password; }
    }

	void Start () 
    {
        
	}
	
	void Update () 
    {
	}

    private WWW CreateWWWWithHeaders(string URL, Dictionary<string, string> headers)
    {
        int count = headers.Count;

        foreach (KeyValuePair<string, string> entry in headers)
        {
            if (count > 1)
            {
                count -= 1;
                URL += "?" + entry.Key + "=" + entry.Value + "&";
            }
            else
            {
                URL += "?" + entry.Key + "=" + entry.Value;
            }
        }

        WWW w = new WWW(URL);

        return w;
    }

    public void DoLogin()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            return;
        }

        header.Clear();
        header.Add("username", usernameInput.text);
        header.Add("password", passwordInput.text);
        StartCoroutine("TryLogin");


    }
    //TODO: Give user feedback messages like "logged in" or "wrong credentials" etc
    //TODO: Check if internet connection
    //TODO: Check the JSON string if 1 or 0
    //TODO: Set isloggedin depending on json
    //TODO: Send back to mainmenu if logged in, disable login button until logged out or game exit
    private IEnumerator TryLogin()
    {
        WWW getUser = CreateWWWWithHeaders(url, header);
        
        yield return getUser;

        if (!string.IsNullOrEmpty(getUser.error))
            print(getUser.error);
        else
            print(getUser.text);


    }
}