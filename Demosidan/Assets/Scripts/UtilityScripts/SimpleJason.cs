using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class SimpleJason
{
    public static Dictionary<string, string> ConvertJSON(string JSON)
    {
        if (string.IsNullOrEmpty(JSON))
        {
            return null;
        }
        
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

    public static List<Dictionary<string, string>> ConvertJSONMany(string JSON)
    {
        if (string.IsNullOrEmpty(JSON))
        {
            return null;
        }

        List<Dictionary<string, string>> valuesList = new List<Dictionary<string, string>>();

        Dictionary<string, string> values = new Dictionary<string, string>(); ;
        string key, value;
        bool atEnd;

        if (JSON[0] == '[')
        {
            JSON = JSON.Substring(1);
            atEnd = false;

            while (!atEnd)
            {
                if (JSON.Length != 0)
                {
                    if (JSON[0] == '{')
                    {
                        values = new Dictionary<string, string>();
                        JSON = JSON.Substring(1);
                    }
                    else if (JSON[0] == '\"')
                    {
                        JSON = JSON.Substring(1);
                        key = Separate(JSON, '"');
                        JSON = JSON.Substring(JSON.IndexOf(':') + 1);

                        JSON = JSON.Substring(1);
                        value = Separate(JSON, '"');
                        JSON = JSON.Substring(JSON.IndexOf('"') + 1);

                        if (JSON[0] == ',')
                            JSON = JSON.Substring(1);

                        values.Add(key, value);
                    }
                    else if (JSON[0] == '}')
                    {
                        valuesList.Add(values);
                        JSON = JSON.Substring(2);
                    }
                    else if (JSON[0] == ']')
                        atEnd = true;
                }
                else
                    atEnd = true;
            }
        }

        return valuesList;
    }

    private static string Separate(string s, char separator)
    {
        int l = s.IndexOf(separator);

        if (l > 0)
            return s.Substring(0, l);
        return "";
    }
}
