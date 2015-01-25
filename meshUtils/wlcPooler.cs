using System;
using System.Diagnostics;
using System.Threading;

namespace meshUtils
{
    public class wlcPooler
    {
        public delegate void mainCallback(int status, Object data);
        private mainCallback callback = null;
        private Thread threadWorker;
        public Boolean threadRun = false;
        private String wlcIP;
        private String wlcUser;
        private String wlcPassword;
        private Int32 wlcScanTime;
        private EventLogger eventLogger = null;

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

        public wlcPooler(Int32 scan, String ip, String user, String password, mainCallback cb, EventLogger eventlogger = null)
        {
            eventLogger = eventlogger;
            try
            {
                callback = cb;
                wlcIP = ip;
                wlcUser = user;
                wlcPassword = password;
                wlcScanTime = scan;
                this.threadWorker = new Thread(new ThreadStart(this.pooler));
                this.threadWorker.Name = "meshStatus.pooler";
                this.threadWorker.IsBackground = true;
                this.threadWorker.Start();

                Logger("wlcPooler : initialisé.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                    Logger("wlcPooler : Erreur lors de l'initialisation.", EventLogEntryType.Information, ex);
            }
        }

        private void pooler()
        {
            try
            {
                Boolean sleep = false;
                threadRun = true;
                Logger("wlcPooler.pooler : Démarrage.", EventLogEntryType.Information);
                while (threadRun)
                {
                    if (sleep)
                    {
                        for (int i = 0; i < wlcScanTime; i++)
                        {
                            if (threadRun)
                            {
                                callback(2, (Object)"Prochain scan : " + (wlcScanTime - i).ToString() + " secondes");
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    if (threadRun)
                    {
                        callback(2, (Object)"Scan en progression...");
                        wlcCollector wlc = new wlcCollector(wlcIP, wlcUser, wlcPassword, eventLogger);
                        callback(1, (Object)wlc.getInfo);
                    }
                    if (!sleep)
                    {
                        sleep = true;
                    }
                }
                Logger("wlcPooler.pooler : Arret.", EventLogEntryType.Information);
                callback(0, null);
            }
            catch (Exception ex)
            {
                threadRun = false;
                Logger("wlcPooler.pooler : Exception.", EventLogEntryType.Error, ex);
                callback(2, (Object)"Erreur :" + ex.ToString());
                callback(0, null);
            }
        }

    }
}
