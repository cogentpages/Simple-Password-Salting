using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace psalt
{
    public class Psalt 
    {
        private string SaltPath { get; set; }
        private List<UserSalt> ExistingSalts { get; set; }

        /// <summary>
        /// Initialize Psalt with the desired path for the collection of salts. If path is not defined, this will be saved to location of the application.
        /// </summary>
        /// <param name="saltFilePath"></param>
        public Psalt(string saltFilePath = null)
        {
            SaltPath = Path.Combine(string.IsNullOrEmpty(saltFilePath) ? 
                                    Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) : 
                                    saltFilePath, 
                                    "salt.dat");
        }

        private string GetUserSalt(string userName)
        {
            string userSalt = "";

            if (File.Exists(SaltPath))
            {
                using (StreamReader r = new StreamReader(SaltPath))
                {
                    var json = r.ReadToEnd();
                    ExistingSalts = JsonConvert.DeserializeObject<List<UserSalt>>(json);
                    userSalt = ExistingSalts?.Where(u => u == null || u.Username == userName).Select(a => a.Salt).SingleOrDefault();
                }
            }
            return userSalt;
        }

        private void SetUserSalt(string userName, string newSalt, bool update)
        {
            var userSalt = new UserSalt
            {
                Username = userName,
                Salt = newSalt
            };

            string newContent = JsonConvert.SerializeObject(userSalt);

            using(StreamWriter r = new StreamWriter(SaltPath, false))
            {
                if (ExistingSalts == null)
                    ExistingSalts = new List<UserSalt> { userSalt };
                else
                {
                    var existingPair = ExistingSalts.Where(u => u.Username == userName).SingleOrDefault();

                    if(existingPair == null && !update)
                        ExistingSalts.Add(userSalt);

                    if(existingPair != null && update)
                        existingPair.Salt = newSalt;
                }

                r.WriteLine(JsonConvert.SerializeObject(ExistingSalts));

            }
        }


        private string GenerateSalt()
        {
            byte[] bytes = new byte[16];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Generates encrypted password with salt. If the user exists with the salt, then the password will be generated along with the saved salt.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="update"></param>
        /// <returns>Encrypted Password</returns>
        public string EncryptPassword(string username, string password, bool? update = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("User Name and Password are required");

            var userSalt = GenerateSalt();

            if(update.Value == false)
            {
                var existingSalt = GetUserSalt(username);
                userSalt = string.IsNullOrEmpty(existingSalt) ? userSalt : existingSalt;
            }

            SetUserSalt(username, userSalt, update.Value);
            var newPassword = userSalt + password;

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256CryptoServiceProvider hasher = new SHA256CryptoServiceProvider();
            return BitConverter.ToString(hasher.ComputeHash(encoder.GetBytes(newPassword))).Replace("-", "");
        }

        
    }

    public class UserSalt
    {
        public string Username { get; set; }
        public string Salt { get; set; }
    }
}
