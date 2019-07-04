using System;
using psalt;

namespace TestEncrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing encryption. Press Enter to proceed..");
            Console.ReadLine();
            var saltPass = new Psalt("d:\\test");

            var username = "User";
            var password = "pass";

            Console.WriteLine("Encrypting password for User: " + username + "...");
            Console.WriteLine("Old Password: " + password);
            Console.WriteLine("New Password: " + saltPass.EncryptPassword(username, password));

            username = "User2";
            password = "pass";

            Console.WriteLine("Encrypting password for User: " + username + "...");
            Console.WriteLine("Old Password: " + password);
            Console.WriteLine("New Password: " + saltPass.EncryptPassword(username, password));

            username = "User3";
            password = "pass";

            Console.WriteLine("Encrypting password for User: " + username + "...");
            Console.WriteLine("Old Password: " + password);
            Console.WriteLine("New Password: " + saltPass.EncryptPassword(username, password));

            Console.WriteLine("done.");
            Console.ReadLine();

        }
    }
}
