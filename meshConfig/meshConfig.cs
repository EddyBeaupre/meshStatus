using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace meshConfig
{
    public class meshConfig
    {
        public String userName;
        public String userPassword;
        public String serverAddress;
        public Int32 serverTimeout;
        public String filePath;
    }

    public class meshConfigUtils
    {

        public meshConfig config;

        static readonly string PasswordHash = "arhKwqRFQtgs3wUmcT3Jq38g";
        static readonly string SaltKey = "dOo6Ias0r4S7rgSitTH43miD";
        static readonly string VIKey = "INXS0xrYb1GhdwKi";

        String configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "meshStatusConfig.xml");

        public Boolean ReadXML()
        {
            try
            {
                if (File.Exists(configFile))
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(meshConfig));
                    System.IO.StreamReader file = new System.IO.StreamReader(configFile);
                    config = (meshConfig)reader.Deserialize(file);
                    file.Close();
                    return true;
                }
                else
                {
                    config = new meshConfig();
                    return false;
                }
            }
            catch
            {
                config = new meshConfig();
                return false;
            }
        }

        public Boolean WriteXML()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(meshConfig));
                System.IO.StreamWriter file = new System.IO.StreamWriter(configFile);
                writer.Serialize(file, config);
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Encrypt(string plainText)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                byte[] cipherTextBytes;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string Decrypt(string encryptedText)
        {
            try
            {
                if ((encryptedText != String.Empty) && (encryptedText != null))
                {
                    byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                    byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                    var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                    var memoryStream = new MemoryStream(cipherTextBytes);
                    var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    memoryStream.Close();
                    cryptoStream.Close();
                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
