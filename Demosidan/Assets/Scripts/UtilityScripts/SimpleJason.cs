using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class SimpleJason
{
    public static Dictionary<string, string> ConvertJSON(string JSON)
    {
        Dictionary<string, string> values = new Dictionary<string, string>();

        Regex pattern = new Regex("[\"|{|}]");
        JSON = pattern.Replace(JSON, "");

        int loopAmount = JSON.Count(x => x == ',') + 1;

        string key;
        string value;

        for (int i = 0; i < loopAmount; i++)
        {
            key = Separate(JSON, ':');
            JSON = JSON.Substring(JSON.IndexOf(':') + 1);

            //If it's the last loop, there's no , so just take the entire string
            if (i == loopAmount - 1)
            {
                value = JSON;
            }
            else
            {
                value = Separate(JSON, ',');
                JSON = JSON.Substring(JSON.IndexOf(',') + 1);
            }

            values.Add(key, value);
        }

        return values;
    }

    private static string Separate(string s, char separator)
    {
        int l = s.IndexOf(separator);

        if (l > 0)
            return s.Substring(0, l);
        return "";
    }
}
