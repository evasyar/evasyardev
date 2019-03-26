using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Yumabot.Modules
{
    public class Cdtimer : ModuleBase<SocketCommandContext>
    {
        private static string _recipients;
        private static DateTime dtNow;
        private static DateTime dtElapsedTime;

        [Command("cdt")]
        public async Task CdtimerAsync()
        {
            await ReplyAsync("I am going to time you really soon");
        }

        /// <summary>
        /// duration, interval measured in minutes
        /// command entry is y!timer 5 1 [discord names of recipients]
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="interval"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        [Command("timer", RunMode=RunMode.Async)]
        public async Task CdtimerAsync(int duration, int interval, [Remainder]string recipients)
        {
            dtNow = DateTime.Now;
            dtElapsedTime = dtNow.AddMinutes(duration);
            _recipients = recipients;
            //await ReplyAsync(string.Format("It is now {0}. At {1}; dear {2},  ***IT WILL BE TIME TO PLAY RED DEAD SOON.*** :sunglasses: ", dtNow.ToString("MM/dd/yyyy hh:mm tt"), dtElapsedTime.ToString("MM/dd/yyyy hh:mm tt"), recipients.Replace(',', ' ')));
            
            await timerLoopedTask(interval);
        }

        private async Task timerLoopedTask(int interval)
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEventAsync);

            aTimer.Interval = (interval * 60) * 1000;
            aTimer.Enabled = true;

            await ReplyAsync(string.Format("It is now {0}. At {1}; dear {2},  ***IT WILL BE TIME TO PLAY RED DEAD SOON.*** :sunglasses: ", dtNow.ToString("MM/dd/yyyy hh:mm tt"), dtElapsedTime.ToString("MM/dd/yyyy hh:mm tt"), _recipients.Replace(',', ' ')));
        }

        private async void OnTimedEventAsync(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime < dtElapsedTime)
            {
                await ReplyAsync(string.Format("It is now {0}. At {1}; dear {2},  ***IT WILL BE TIME TO PLAY RED DEAD SOON.*** :sunglasses: ", e.SignalTime.ToString("MM/dd/yyyy hh:mm tt"), dtElapsedTime.ToString("MM/dd/yyyy hh:mm tt"), _recipients.Replace(',', ' ')));
            }
            else
            {
                await ReplyAsync(string.Format("It is now {0}. Dear {1};  ***IT IS TIME TO PLAY RED DEAD. Have a wonderful time*** :grin: ", dtElapsedTime.ToString("MM/dd/yyyy hh:mm tt"), _recipients.Replace(',', ' ')));
                ((Timer)sender).Enabled = false;
            }
        }
    }
}
