using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace meshUtils
{
    public class meshConfig
    {
        public String userName;
        public String userPassword;
        public String serverAddress;
        public Int32 serverTimeout;
        public Int32 serviceTimeout;
        public String filePath;
        public Boolean dateTimeFileName;
        public Boolean debugMode;
        public EventLogEntryType debugLevel;
    }

    public class meshConfigUtils
    {

        public meshConfig config;

        static readonly string PasswordHash = "arhKwqRFQtgs3wUmcT3Jq38g";
        static readonly string SaltKey = "dOo6Ias0r4S7rgSitTH43miD";
        static readonly string VIKey = "INXS0xrYb1GhdwKi";

        public readonly Int32 minServerTimeout = 60;
        public readonly Int32 minServiceTimeout = 300;


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

                    validateConfig();

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

        public void validateConfig()
        {
            if (config.serverTimeout < minServerTimeout)
            {
                config.serverTimeout = minServerTimeout;
            }
            if (config.serviceTimeout < minServiceTimeout)
            {
                config.serviceTimeout = minServiceTimeout;
            }

            if (config.debugLevel < EventLogEntryType.Error)
            {
                config.debugLevel = EventLogEntryType.Error;
            }

            if (config.debugLevel > EventLogEntryType.FailureAudit)
            {
                config.debugLevel = EventLogEntryType.FailureAudit;
            }
        }

        public Boolean WriteXML()
        {
            try
            {
                validateConfig();   

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
