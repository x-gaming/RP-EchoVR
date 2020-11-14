using RP_EchoVR;
using System;
using System.Diagnostics;
using System.Threading;

namespace Discord {
    class DiscordController {

        private readonly bool manually;
        private readonly long startTime;

        public DiscordController(bool manually = false) {
            this.manually = manually;
            startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
        public void Init(long id) {
            Discord discord = new Discord(id, (ulong) CreateFlags.Default);
            DataFetcher fetcher = new DataFetcher("http://127.0.0.1:6721/session");
            Activity activity = new Activity {
                State = "starting...",
                Details = "starting...",
                Timestamps = {
                    Start = startTime,
                },
                Assets = {
                    LargeImage = "echo",
                    LargeText = "Echo VR",
                    SmallImage = "echo_logo",
                    SmallText = "Echo Arena",
                },
            };
            while (manually || Process.GetProcessesByName("echovr").Length > 0) {
                EchoData data = fetcher.GetData();
                activity.State = DataConverter.GetState(data.client_name, data.map_name, data.game_status, data.blue_points, data.orange_points, data.last_score, data.teams);
                activity.Details = DataConverter.GetDetails(data.match_type);
                activity.Timestamps.End = DataConverter.GetEndTime(data.match_type, data.game_clock, data.game_status);

                ActivityManager manager = discord.GetActivityManager();
                manager.UpdateActivity(activity, result => {
                    if (result != Result.Ok) {
                        Console.WriteLine("failed");
                    }
                });
                Thread.Sleep(50);
                discord.RunCallbacks();
            }
            discord.GetActivityManager().ClearActivity(result => { });
            discord.Dispose();
        }
    }
}
