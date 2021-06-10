using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_translateC (string inputString, string languageFrom, string  languageTo, out string outputString )
    {
        // Put your code herestring result = null;

        inputString = System.Net.WebUtility.UrlEncode(inputString);
        string url = string.Format(@"https://translation.googleapis.com/language/translate/v2?q={0}&target={1}&source={2}&key=AIzaSyCVYoFnJoIYZh53-Mm5-nJWTUA5beTEO_A", inputString, languageTo, languageFrom);

        string result = String.Empty;

        using (System.Net.WebClient web = new System.Net.WebClient())
        {
            web.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "Mozilla/5.0");
            web.Headers.Add(System.Net.HttpRequestHeader.AcceptCharset, "UTF-8");
            web.Encoding = Encoding.UTF8;
            result = web.DownloadString(url);
        }
        string pattern = "(?<=\"translatedText\"\\ *:\\ *\")(?:\\\\\"|[^\"])*";

        Match m = Regex.Match(result, pattern);
        if (m.Success) result = m.Value;
                
        outputString = result;
        return;
    }

    

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void sp_DetectLanguage(string inputString, out string outputString)
    {
        // Put your code herestring result = null;

        inputString = System.Net.WebUtility.UrlEncode(inputString);

        string url = string.Format(@"https://translation.googleapis.com/language/translate/v2/detect?q={0}&key=AIzaSyCVYoFnJoIYZh53-Mm5-nJWTUA5beTEO_A", inputString);

        string result = String.Empty;

        using (System.Net.WebClient web = new System.Net.WebClient())
        {
            web.Headers.Add(System.Net.HttpRequestHeader.UserAgent, "Mozilla/5.0");
            web.Headers.Add(System.Net.HttpRequestHeader.AcceptCharset, "UTF-8");
            web.Encoding = Encoding.UTF8;

            result = web.DownloadString(url);
        }
        string pattern = "(?<=\"language\"\\ *:\\ *\")(?:\\\\\"|[^\"])*";

        Match m = Regex.Match(result, pattern);
        if (m.Success) result = m.Value;

        outputString = result;
        return;
    }
}
