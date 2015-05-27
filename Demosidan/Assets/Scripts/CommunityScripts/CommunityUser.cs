using UnityEngine;
using System.Collections;

public static class CommunityUser
{
    private static bool isLoggedIn = false;
    private static string username;
    private static string password;

    public static bool IsLoggedIn
    {
        get { return CommunityUser.isLoggedIn; }
        set { isLoggedIn = value; }
    }
    public static string Username
    {
        get { return CommunityUser.username; }
        set { username = value; }
    }
    public static string Password
    {
        get { return CommunityUser.password; }
        set { password = value; }
    }
}
