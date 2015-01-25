using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using meshUtils;

namespace meshStatusService
{
    public partial class serviceControl : ServiceBase
    {
        private delegate void mainCallback_delegate(int status, Object data);
        private meshConfigUtils config = new meshConfigUtils();
        private wlcPooler pooler = null;

        private EventLogger eventLogger = null;

        public serviceControl()
        {
            InitializeComponent();
            config.ReadXML();
            eventLogger = new EventLogger(this.EventLog, this.ServiceName, "Application", config.config.debugLevel);
        }


        public void mainCallback(int status, Object data)
        {
            try
            {
                switch (status)
                {
                    case 1:
                        DateTime timeStamp = DateTime.Now;
                        if (data != null)
                        {
                            String fileName;

                            if (config.config.dateTimeFileName == true)
                            {
                                fileName = Path.Combine(config.config.filePath, String.Concat("meshStatus-", timeStamp.ToString("yyyyMMddHHmmss"), ".csv"));
                            }
                            else
                            {
                                fileName = Path.Combine(config.config.filePath, String.Concat("meshStatus", ".csv"));
                            }
                            
                            try
                            {
                                try
                                {
                                        if (File.Exists(fileName))
                                        {
                                            File.Delete(fileName);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        eventLogger.WriteEntry("Impossible d'effacer " + fileName + ".", EventLogEntryType.Warning, ex);
                                    }
                                
                                using (StreamWriter outfile = new StreamWriter(fileName, false))
                                {
                                    foreach (meshInfo s in (List<meshInfo>)data)
                                    {
                                        outfile.WriteLine(s.getSector + "," + s.getName + "," + s.getParent + "," + s.getSNR + "," + s.getUpTime.ToString(@"dd\.hh\:mm\:ss") + "," + s.getAssociationTime.ToString(@"dd\.hh\:mm\:ss") + "," + s.getJoinTime.ToString(@"dd\.hh\:mm\:ss"));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                eventLogger.WriteEntry("Impossible de crée " + fileName + ".", EventLogEntryType.Warning, ex);
                            }
                        }
                        break;
                    case 0:
                        stopPooler();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                eventLogger.WriteEntry("Une exception est survenue.", EventLogEntryType.Error, ex);
                stopPooler();
            }
        }

        private void stopPooler()
        {
            if (pooler.threadRun)
            {
                pooler.threadRun = false;
            }
            else
            {
                Stop();
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                eventLogger.WriteEntry("Lecture de la configuration.", EventLogEntryType.Information);

                if (!Directory.Exists(config.config.filePath))
                {
                    eventLogger.WriteEntry("Le répertoire de destination n'existe pas. Vérifiez la configuration.", EventLogEntryType.Warning);
                    stopPooler();
                }

                if ((config.config.userPassword == String.Empty) || config.config.userPassword == null)
                {
                    eventLogger.WriteEntry("Un mot de passe est nécessaire. Vérifiez la configuration.", EventLogEntryType.Error);
                    stopPooler();
                }

                if ((config.config.userName == String.Empty) || config.config.userName == null)
                {
                    eventLogger.WriteEntry("Un nom d'utilisateur est nécessaire. Vérifiez la configuration.", EventLogEntryType.Error);
                    stopPooler();
                }

                if ((config.config.serverAddress == String.Empty) || config.config.serverAddress == null)
                {
                    eventLogger.WriteEntry("L'adresse du serveur est nécessaire. Vérifiez la configuration.", EventLogEntryType.Error);
                    stopPooler();
                }


                if (config.config.serviceTimeout < config.minServiceTimeout)
                {
                    eventLogger.WriteEntry("Ajuste l'interval minimum d'intérrogation du serveur a " + config.minServiceTimeout.ToString() + ".", EventLogEntryType.Warning);
                    config.config.serviceTimeout = config.minServiceTimeout;
                }

                eventLogger.WriteEntry("Démarrage du service.", EventLogEntryType.Information);
                pooler = new wlcPooler(Convert.ToInt32(config.config.serviceTimeout), config.config.serverAddress, config.Decrypt(config.config.userName), config.Decrypt(config.config.userPassword), mainCallback, eventLogger);
            }
            catch (Exception ex)
            {
                eventLogger.WriteEntry("Une exception est survenue.", EventLogEntryType.Error, ex);
                stopPooler();
            }

        }

        protected override void OnStop()
        {
            try
            {
                eventLogger.WriteEntry("Le service est arreter.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLogger.WriteEntry("Une exception est survenue.", EventLogEntryType.Error, ex);
            }
        }
    }
}
