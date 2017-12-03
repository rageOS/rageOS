using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GTANetworkAPI;

namespace RageOS
{
    public class ServerTime : Script
    {
        public static string[] dayNames = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
        System.Threading.Thread timeThread = null;
        private Timer timer;

        public ServerTime()
        {
            API.OnResourceStart += DayNightInit;
            API.OnResourceStop += DayNightExit;
        }

        public void DayNightInit()
        {
            API.Delay(5000, true, () =>
            {
                int day = (int)DateTime.Now.DayOfWeek;
                API.SetWorldSyncedData("DAYNIGHT_DAY", day); // aka dayNames[0] - Monday
                API.SetWorldSyncedData("DAYNIGHT_DAY_STRING", dayNames[day]);
                API.SetWorldSyncedData("DAYNIGHT_HOUR", DateTime.Now.Hour);
                API.SetWorldSyncedData("DAYNIGHT_MINUTE", DateTime.Now.Minute);
                API.SetWorldSyncedData("DAYNIGHT_RENDER_ICON", false);
                DayNightPrepareText();

                foreach (var player in API.GetAllPlayers()) API.FreezePlayerTime(player, true);

                timeThread = new System.Threading.Thread(UpdateTime);
                timeThread.Start();

                timer = new Timer();
                timer.Interval = 10000;
                timer.Elapsed += TimerEventHandler;
                timer.AutoReset = true;
                timer.Enabled = true;
            });
        }

        public void DayNightExit()
        {
            timer.Stop();
            if (timeThread != null) timeThread.Abort();
            timeThread = null;
        }

        public static void DayNightPrepareText()
        {
            API.Shared.SetWorldSyncedData("DAYNIGHT_TEXT", API.Shared.GetWorldSyncedData("DAYNIGHT_HOUR").ToString("D2") + ":" + API.Shared.GetWorldSyncedData("DAYNIGHT_MINUTE").ToString("D2") + " Uhr");
        }

        public void TimerEventHandler(System.Object source, ElapsedEventArgs e)
        {
            UpdateTime();
        }
        public void UpdateTime()
        {
            int add = 1;
            int minute = API.GetWorldSyncedData("DAYNIGHT_MINUTE");
            int hour = API.GetWorldSyncedData("DAYNIGHT_HOUR");
            if (hour >= 22 || hour <= 4)
            {
                add = 2;
            }
            minute = minute + add;

            if (minute >= 60)
            {
                minute = 0;
                hour = hour + 1;

                API.SetWorldSyncedData("DAYNIGHT_MINUTE", minute);

                if (hour >= 24)
                {
                    hour = 0;
                    API.SetWorldSyncedData("DAYNIGHT_HOUR", hour);

                    API.SetWorldSyncedData("DAYNIGHT_DAY", API.GetWorldSyncedData("DAYNIGHT_DAY") + 1);
                    if (API.GetWorldSyncedData("DAYNIGHT_DAY") == dayNames.Length) API.SetWorldSyncedData("DAYNIGHT_DAY", 0);
                    API.SetWorldSyncedData("DAYNIGHT_DAY_STRING", dayNames[API.GetWorldSyncedData("DAYNIGHT_DAY")]);
                }
                else
                {
                    API.SetWorldSyncedData("DAYNIGHT_HOUR", hour);
                }
            }
            else
            {
                API.SetWorldSyncedData("DAYNIGHT_MINUTE", minute);
            }

            API.SetTime(hour, minute, 0);
            DayNightPrepareText();
        }

    }
}
