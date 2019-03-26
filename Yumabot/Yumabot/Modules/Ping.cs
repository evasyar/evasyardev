using Discord.Commands;
using System.Threading.Tasks;

namespace Yumabot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("**Hello**     _Discord_   !");
        }
    }
}
