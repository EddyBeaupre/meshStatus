using System;

namespace meshUtils
{
    public class meshInfo
    {
        private Int32 sector;
        private String name;
        private String parent;
        private Int32 snr;
        private TimeSpan up;
        private TimeSpan association;
        private TimeSpan join;

        public Int32 getSector { get { return this.sector; } }
        public String getName { get { return this.name; } }
        public String getParent { get { return this.parent; } }
        public Int32 getSNR { get { return this.snr; } }
        public TimeSpan getUpTime { get { return this.up; } }
        public TimeSpan getAssociationTime { get { return this.association; } }
        public TimeSpan getJoinTime { get { return this.join; } }

        /// <summary>
        /// Convert WLC time to timespan.
        /// </summary>
        /// <param name="data">Timespan in WLC format (D days, HH h MM m SS s)</param>
        /// <returns>A TimeSpan with the WLC timespan</returns>
        private TimeSpan convertWLCTimeSpan(String data)
        {
            try
            {
                if (data != String.Empty)
                {
                    Int32 days = Convert.ToInt32(data.Substring(0, data.IndexOf(" days,")));
                    Int32 hours = Convert.ToInt32(data.Substring(data.IndexOf(", ") + 2, data.IndexOf(" h") - (data.IndexOf(", ") + 2)));
                    Int32 min = Convert.ToInt32(data.Substring(data.IndexOf("h ") + 2, data.IndexOf(" m") - (data.IndexOf("h ") + 2)));
                    Int32 sec = Convert.ToInt32(data.Substring(data.IndexOf("m ") + 2, data.IndexOf(" s") - (data.IndexOf("m ") + 2)));

                    return (new TimeSpan(days, hours, min, sec));
                }
                else
                {
                    return (new TimeSpan(0, 0, 0, 0));
                }
            }
            catch(Exception ex)
            {
                return (new TimeSpan(0, 0, 0, 0));
            }
        }

        public meshInfo(Int32 sc, String nm, String pr, Int32 sr, TimeSpan ut, TimeSpan at, TimeSpan jt)
        {
            this.sector = sc;
            this.name = nm;
            this.parent = pr;
            this.snr = sr;
            this.up = ut;
            this.association = at;
            this.join = jt;
        }

        public meshInfo(String sc, String nm, String pr, String sr, String ut, String at, String jt)
        {
            this.sector = Convert.ToInt32(sc);
            this.name = nm;
            this.parent = pr;
            this.snr = Convert.ToInt32(sr);
            this.up = convertWLCTimeSpan(ut);
            this.association = convertWLCTimeSpan(at);
            this.join = convertWLCTimeSpan(jt);
        }

        public meshInfo(Int32 sc, String nm, String pr, String sr, String ut, String at, String jt)
        {
            this.sector = sc;
            this.name = nm;
            this.parent = pr;
            this.snr = Convert.ToInt32(sr);
            this.up = convertWLCTimeSpan(ut);
            this.association = convertWLCTimeSpan(at);
            this.join = convertWLCTimeSpan(jt);
        }
    }
}
