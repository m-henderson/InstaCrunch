using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SocialShark
{
    /// <summary>
    /// Class for organizing methods that handle tokens.
    /// </summary>
    class Tokens
    {
        public static string GetCSRFToken()
        {
            string test = WebRequest.GetWebRequest("https://instagram.com/accounts/login/");
            string csrf_token = Regex.Match(test, @"(?<=""csrf_token"":"")(.*?)(?="")").Value;
            return csrf_token;
        }
    }
}
