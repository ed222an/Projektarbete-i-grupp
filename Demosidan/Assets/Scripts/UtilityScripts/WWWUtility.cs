using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WWWUtility
{
    public static WWW CreateWWWWithHeaders(string URL, Dictionary<string, string> headers)
    {
        int count = headers.Count;

        URL += "?";

        foreach (KeyValuePair<string, string> entry in headers)
        {
            if (count > 1)
            {
                count -= 1;
                URL += entry.Key + "=" + entry.Value + "&";
            }
            else
            {
                URL += entry.Key + "=" + entry.Value;
            }
        }

        WWW w = new WWW(URL);

        return w;
    }
}
