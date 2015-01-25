using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace meshUtils
{
    public class wlcCollector
    {
        private PasswordConnectionInfo sshInfo;
        private SshClient sshClient;
        private ShellStream sshShell;
        private String meshData = String.Empty;
        private List<meshInfo> info = null;

        private String userName;
        private String userPassword;

        private EventLogger eventLogger;

        private void Logger(String msg, EventLogEntryType level, Exception ex = null)
        {
            if (ex != null)
            {
                eventLogger.WriteEntry(msg, level, ex);
            }
            else
            {
                eventLogger.WriteEntry(msg, level);
            }
        }
        public List<meshInfo> getInfo { get { return this.info; } }

        /// <summary>
        /// Capture until the next prompt
        /// </summary>
        /// <param name="stream">Stream to capture from</param>
        /// <param name="prompt">Prompt to wait for</param>
        /// <param name="time">timeout in seconds</param>
        /// <returns>String object of the captured text</returns>
        private String waitPrompt(String prompt, int time)
        {
            try
            {
                TimeSpan timeout = new TimeSpan(0, 0, time);
                return (sshShell.Expect(prompt, timeout));
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.waitPrompt : Exception :", EventLogEntryType.Error, ex);
                return String.Empty;
            }
        }
        /// <summary>
        /// Capture until the next prompt then send a command.
        /// </summary>
        /// <param name="stream">Stream to capture from</param>
        /// <param name="prompt">Prompt to wait for before sending command</param>
        /// <param name="command">Command to send to the stream</param>
        /// <param name="time">timeout in seconds</param>
        /// <returns>String object of the captured text</returns>
        private String waitPromptCmd(String prompt, String command, int time)
        {
            try
            {
                String data = waitPrompt(prompt, time);

                if (data != null)
                {
                    writeStream(command);
                }

                return data;
            }

            catch (Exception ex)
            {
                Logger("wlcCollector.waitPromptCmd : Exception :", EventLogEntryType.Error, ex);
                return String.Empty;
            }
        }
        /// <summary>
        /// Send a command then capture the result.
        /// </summary>
        /// <param name="stream">Stream to capture from</param>
        /// <param name="prompt">Prompt to wait for after the command is sent</param>
        /// <param name="command">Command to send to the stream</param>
        /// <param name="time">Timeout in seconds</param>
        /// <returns>String object of the captured text</returns>
        private String cmdPromptWait(String prompt, String command, int time)
        {
            try
            {
                if (writeStream(command))
                {
                    return (waitPrompt(prompt, time));
                }
                else
                {
                    return String.Empty;
                }
            }

            catch (Exception ex)
            {
                Logger("wlcCollector.cmdPromptWait : Exception :", EventLogEntryType.Error, ex);
                return String.Empty;
            }


        }
        /// <summary>
        /// write a command to the stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="command">Command to send to the stream</param>
        private bool writeStream(String command)
        {
            try
            {
                StreamWriter writer = new StreamWriter(sshShell);
                writer.AutoFlush = true;
                writer.NewLine = "\r";

                writer.WriteLine(command);
                return true;
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.writeStream : Exception :", EventLogEntryType.Error, ex);
                return false;
            }
        }

        private bool getMeshApTree()
        {
            try
            {
                String data;
                Logger("wlcCollector.getMeshApTree : En attente de 'User'.", EventLogEntryType.Information);
                data = waitPromptCmd("User:", userName, 60);

                if (data == null)
                {
                    Logger("wlcCollector.getMeshApTree : Délais dépasser.", EventLogEntryType.Information);
                    return false;
                }

                Logger("wlcCollector.getMeshApTree : En attente de 'Password'.", EventLogEntryType.Information);
                data = waitPromptCmd("Password:", userPassword, 30);
                if (data == null)
                {
                    Logger("wlcCollector.getMeshApTree : Délais dépasser.", EventLogEntryType.Information);
                    return false;
                }

                Logger("wlcCollector.getMeshApTree : En attente de '(Cisco Controller)'.", EventLogEntryType.Information);
                data = waitPromptCmd("(Cisco Controller) >", "config paging disable", 30);
                if (data == null)
                {
                    Logger("wlcCollector.getMeshApTree : Délais dépasser.", EventLogEntryType.Information);
                    return false;
                }

                Logger("wlcCollector.getMeshApTree : En attente de '(Cisco Controller)'.", EventLogEntryType.Information);
                data = waitPrompt("(Cisco Controller) >", 30);

                if (data == null)
                {
                    Logger("wlcCollector.getMeshApTree : Délais dépasser.", EventLogEntryType.Information);
                    return false;
                }

                Logger("wlcCollector.getMeshApTree : En attente de 'show mesh ap tree'.", EventLogEntryType.Information);
                data = cmdPromptWait("(Cisco Controller) >", "show mesh ap tree", 30);

                if (data == null)
                {
                    Logger("wlcCollector.getMeshApTree : Délais dépasser.", EventLogEntryType.Information);
                    return false;
                }

                Logger("wlcCollector.getMeshApTree : Traitement de la réponse du contrôleur.", EventLogEntryType.Information);
                data = data.Replace("\n", "");
                String[] stringSeparator = new String[] { "\r" };
                String[] tmp = data.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                data = String.Empty;
                Boolean stringAdd = false;
                foreach (String s in tmp)
                {
                    if (s.StartsWith("[Sector"))
                    {
                        if (stringAdd)
                        {
                            data = data + Environment.NewLine;
                        }
                        else
                        {
                            stringAdd = true;
                        }
                    }

                    if (s.StartsWith("Number"))
                    {
                        stringAdd = false;
                    }

                    if (stringAdd)
                    {
                        if (!s.StartsWith("----------"))
                        {
                            data = data + s.Replace("|-", "").Trim() + Environment.NewLine;
                        }
                    }
                }

                meshData = data;
                Logger("wlcCollector.getMeshApTree : Fin.", EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.getMeshApTree : Exception :", EventLogEntryType.Error, ex);
                return false;
            }
        }


        private int countSector()
        {
            try
            {
                if (meshData != String.Empty)
                {
                    int count = 0;
                    String[] stringSeparator = new String[] { Environment.NewLine };
                    String[] tmp = meshData.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                    foreach (String s in tmp)
                    {
                        if (s.StartsWith("[Sector"))
                        {
                            count++;
                        }
                    }

                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.countSector : Exception :", EventLogEntryType.Error, ex);
                return 0;
            }
        }

        private String getSector(int sector)
        {
            try
            {
                if (meshData != String.Empty)
                {
                    bool found = false;
                    String sc = String.Empty;

                    String[] stringSeparator = new String[] { Environment.NewLine };
                    String[] tmp = meshData.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                    foreach (String s in tmp)
                    {
                        if (found)
                        {
                            if (!s.StartsWith("[Sector"))
                            {
                                sc += s + Environment.NewLine;
                            }
                            else
                            {
                                found = false;
                            }
                        }

                        if (s.StartsWith("[Sector " + sector.ToString() + "]"))
                        {
                            found = true;
                        }
                    }

                    return sc;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.getSector : Exception :", EventLogEntryType.Error, ex);
                return String.Empty;
            }

        }

        private String[] parseAP(String s)
        {

            try
            {
                int x = s.IndexOf("[");
                String[] stringSeparator = new String[] { "," };
                String[] apInfo = s.Substring(x + 1, s.Length - (x + 2)).Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                return new String[] { s.Substring(0, x), apInfo[0], apInfo[1], apInfo[2] };
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.parseAP : Exception :", EventLogEntryType.Error, ex);
                return new String[] { String.Empty };
            }
        }

        private List<meshInfo> parseMesh()
        {
            try
            {
                if (meshData != String.Empty)
                {
                    List<meshInfo> data = new List<meshInfo>();
                    for (int i = 1; i <= countSector(); i++)
                    {
                        data.AddRange(parseSector(i));
                    }

                    return (data);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.parseMesh : Exception :", EventLogEntryType.Error, ex);
                return null;
            }
        }

        private List<meshInfo> parseSector(int sector)
        {
            try
            {
                List<String[]> apInfo = new List<string[]>();
                List<meshInfo> data = new List<meshInfo>();

                String[] stringSeparator = new String[] { Environment.NewLine };
                String[] tmp = getSector(sector).Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in tmp)
                {
                    apInfo.Add(parseAP(s));
                }

                for (int i = 0; i < apInfo.Count; i++)
                {
                    String apParent = String.Empty;
                    String[] apDetail = getAPState(apInfo[i][0]);

                    if (apInfo[i][1] == "*")
                    {
                        apInfo[i][1] = "0";
                    }

                    if (apInfo[i][2] == "*")
                    {
                        apInfo[i][2] = "0";
                    }

                    if (Convert.ToInt32(apInfo[i][1]) > 0)
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            if (Convert.ToInt32(apInfo[i][1]) > Convert.ToInt32(apInfo[j][1]))
                            {
                                apParent = apInfo[j][0];
                                break;
                            }
                        }
                    }
                    data.Add(new meshInfo(sector, apInfo[i][0], apParent, apInfo[i][2], apDetail[0], apDetail[1], apDetail[2]));
                }

                return data;
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.parseSector : Exception :", EventLogEntryType.Error, ex);
                return null;
            }
        }

        private String[] getAPState(String name)
        {
            try
            {
                String upTime = String.Empty;
                String capTime = String.Empty;
                String joinTime = String.Empty;
                String data = cmdPromptWait("(Cisco Controller) >", "show ap config general " + name, 30);

                if (data == null)
                {
                    return null;
                }

                data = data.Replace("\n", "");
                String[] stringSeparator = new String[] { "\r" };
                String[] tmp = data.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in tmp)
                {
                    if (s.StartsWith("AP Up Time"))
                    {
                        upTime = s.Substring(50);
                    }
                    if (s.StartsWith("AP LWAPP Up Time"))
                    {
                        capTime = s.Substring(50);
                    }
                    if (s.StartsWith("Join Taken Time"))
                    {
                        joinTime = s.Substring(50);
                    }
                }
                return new String[] { upTime, capTime, joinTime };
            }
            catch (Exception ex)
            {
                Logger("wlcCollector.getAPState : Exception :", EventLogEntryType.Error, ex);
                return new String[] { String.Empty };
            }
        }

        public wlcCollector(String address, String name, String password, EventLogger eventlogger = null)
        {
            eventLogger = eventlogger;
            try
            {
                Logger("wlcCollector : Démarrage", EventLogEntryType.Information);

                userName = name;
                userPassword = password;

                Logger("wlcCollector : Création du client SSH", EventLogEntryType.Information);
                sshInfo = new PasswordConnectionInfo(address, name, password);
                sshClient = new SshClient(sshInfo);
                Logger("wlcCollector : Connexion du client SSH", EventLogEntryType.Information);
                sshClient.Connect();
                Logger("wlcCollector : Création du shell SSH", EventLogEntryType.Information);
                sshShell = sshClient.CreateShellStream("dumb", 80, 25, 800, 600, 1024);

                if (getMeshApTree())
                {
                    info = parseMesh();
                }
                Logger("wlcCollector : Arret", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Logger("wlcCollector : Exception : ", EventLogEntryType.Information, ex);
            }
        }
    }
}
