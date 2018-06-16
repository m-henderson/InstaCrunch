using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SocialShark
{
    /// <summary>
    /// Class for organizing web request.
    /// </summary>
    public class WebRequest
    {
        public static string GetWebRequest(string url)
        {
            try
            {
                var proxyServer = ProxySharp.Proxy.GetSingleProxy();
                WebProxy proxy = new WebProxy(proxyServer);
                HttpWebRequest postReq = (HttpWebRequest)System.Net.WebRequest.Create(url);
                WebHeaderCollection postHeaders = postReq.Headers;
                postReq.Proxy = proxy;
                postReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
                postReq.Accept = "*/*";
                postHeaders.Add("Accept-Language", "it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3");
                postReq.Method = "GET";
                postReq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)postReq.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "";
                
            }
        }
    }
}
