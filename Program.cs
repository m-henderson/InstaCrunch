using System;

namespace SocialShark
{
    class Program
    {
        // first arg username
        // second arg wordlist
        static void Main(string[] args)
        {
            string username;
            string wordlist;

            username = args[0];
            wordlist = args[1];

            Bruter.Start(username, wordlist);
        }
    }
}
