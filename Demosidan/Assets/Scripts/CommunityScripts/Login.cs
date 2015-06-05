using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Login : MonoBehaviour 
{
    public InputField usernameInput;
    public InputField passwordInput;
    public GameObject messageBox;
    public Text messageText;
    public GameObject loadingPanel;
    private static GetAchievements g;

    private Dictionary<string,string> header = new Dictionary<string,string>();

    private string url = "http://www.metalgenre.se/api/achievements/GetUser.php";

    public void DoLogin()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            messageText.text = "An input field can't be empty";
            messageBox.SetActive(true);
            return;
        }

        header.Clear();
        header.Add("username", usernameInput.text);
        header.Add("password", AES.encrypt(passwordInput.text));
        StartCoroutine("TryLogin");
    }

    private IEnumerator TryLogin()
    {
        loadingPanel.SetActive(true);

        WWW getUser = WWWUtility.CreateWWWWithHeaders(url, header);
        
        yield return getUser;

        if (!string.IsNullOrEmpty(getUser.error) || string.IsNullOrEmpty(getUser.text))
        {
            print(getUser.error);
            messageText.text = "An error has occured! Please check your internet connection";
            messageBox.SetActive(true);
        }
        else
        {
            Dictionary<string, string> values = SimpleJason.ConvertJSON(getUser.text);

            if (values != null)
            {
                string result;
                values.TryGetValue("result", out result);

                if (result == "1")
                {
                    CommunityUser.IsLoggedIn = true;
                    CommunityUser.Username = usernameInput.text;
                    CommunityUser.Password = AES.encrypt(passwordInput.text);

                    messageText.text = "You have successfully logged in";
                    messageBox.SetActive(true);
                }
                else
                {
                    messageText.text = "The provided credentials are invalid, please try again";
                    messageBox.SetActive(true);
                    passwordInput.text = "";
                }
            }
        }

        loadingPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        if (CommunityUser.IsLoggedIn)
        {
            Application.LoadLevel(0);
        }
    }
}