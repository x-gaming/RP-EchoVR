using RP_EchoVR;
using System;
using System.Diagnostics;

namespace Discord {
    class DiscordController {

        private readonly bool manually;
        private readonly long startTime;

        public DiscordController(bool manually = false) {
            this.manually = manually;
            startTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public void Start(Discord discord) {
            DataFetcher fetcher = new DataFetcher("http://127.0.0.1:6721/session");
            try {
                while (manually || Process.GetProcessesByName("echovr").Length > 0) {
                    UpdateActivity(discord, fetcher);
                    discord.RunCallbacks();
                }
            } finally {
                ClearActivity(discord);
                discord.Dispose();
            }
        }

        private void UpdateActivity(Discord discord, DataFetcher fetcher) {
            EchoData data = fetcher.UseTestData();

            var activity = new Activity {
                State = DataConverter.GetState(data.client_name, data.map_name, data.game_status, data.blue_points, data.orange_points, data.last_score, data.teams),
                Details = DataConverter.GetDetails(data.match_type),
                Timestamps = {
                Start = startTime,
                End = DataConverter.GetEndTime(data.match_type, data.game_clock, data.game_status, startTime),
                },
                Assets = {
                    LargeImage = "echo",
                    LargeText = "Echo VR",
                    SmallImage = "echo_logo",
                    SmallText = "Echo Arena",
                },
                Instance = true,
            };

            discord.GetActivityManager().UpdateActivity(activity, result => {
                if (result != Result.Ok) {
                    Console.WriteLine("failed to update activity");
                }
            });
        }

        private void ClearActivity(Discord discord) {
            discord.GetActivityManager().ClearActivity(result => {
                if (result != Result.Ok) {
                    Console.WriteLine("failed to clear activity");
                }
            });
            discord.RunCallbacks();
        }
    }
}
