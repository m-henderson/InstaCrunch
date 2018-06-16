using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SocialShark
{
    public class Actions
    {

        public static string Login(string username, string password, string csrf)
        {

            try
            {
                var proxyServer = ProxySharp.Proxy.GetSingleProxy();
                var loginCookies = new CookieCollection();
                byte[] bytes = ASCIIEncoding.UTF8.GetBytes("username=" + username + "&password=" + password);
                HttpWebRequest postReq = (HttpWebRequest)System.Net.WebRequest.Create("https://www.instagram.com/accounts/login/ajax/");
                WebHeaderCollection postHeaders = postReq.Headers;
                var proxy = new WebProxy(proxyServer);
                postReq.Proxy = proxy;
                postReq.Method = "POST";
                postReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
                postReq.Accept = "*/*";
                postHeaders.Add("Accept-Language", "it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3");
                postHeaders.Add("Accept-Encoding", "gzip, deflate");
                postReq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                postHeaders.Add("X-Instagram-AJAX", "1");
                postHeaders.Add("X-CSRFToken", csrf);
                postHeaders.Add("X-Requested-With", "XMLHttpRequest");
                postReq.Referer = "https://www.instagram.com/accounts";
                postReq.ContentLength = bytes.Length;
                var cookies = new CookieContainer();
                cookies.Add(new Cookie("csrftoken", csrf) { Domain = "instagram.com" });
                postReq.CookieContainer = cookies;
                postReq.KeepAlive = true;
                postHeaders.Add("Pragma", "no-cache");
                postHeaders.Add("Cache-Control", "no-cache");
                Stream postStream = postReq.GetRequestStream();
                postStream.Write(bytes, 0, bytes.Length);
                postStream.Close();
                HttpWebResponse postResponse;
                postResponse = (HttpWebResponse)postReq.GetResponse();
                loginCookies = postResponse.Cookies;
                StreamReader reader = new StreamReader(postResponse.GetResponseStream());
                Stream dataStream = postResponse.GetResponseStream();
                StreamReader reader2 = new StreamReader(dataStream);
                string responseFromServer = reader2.ReadToEnd();
                var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseFromServer);
                if (jsonResult["authenticated"] == true)
                {
                    var custString = "TRUE. PASSWORD: " + password;
                    return custString;
                }

                return "FALSE";
            }
            catch (Exception)
            {

                return "FALSE";
            }
        }
    }
}
