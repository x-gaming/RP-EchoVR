using System;

namespace RP_EchoVR {
    class DataConverter {
        private static int[] lastScores = { -1, -1 };
        private static string lastResult = "";

        public static string GetDetails(string match_type) {
            string details = match_type.Replace('_', ' ');
            return details;
        }

        public static string GetState(string client_name, string map_name, string game_status, int blue_points, int orange_points, LastScore last_score, Team[] teams) {
            string state;
            if (map_name.Equals("mpl_lobby_b2")) {
                if (lastScores[0] == -1 || lastScores[1] == -1) {
                    state = "having fun in lobby";
                } else {
                    state = "last " + lastResult + ": [" + lastScores[0] + ":" + lastScores[1] + "]";
                }
            } else {
                switch (game_status) {
                    case "playing":
                        state = "B [" + blue_points + ":" + orange_points + "] O";
                        break;
                    case "score":
                        state = last_score.team + " scored (" + last_score.point_amount + ")";
                        break;
                    case "post_match":
                    case "post_sudden_death":
                        lastScores[0] = blue_points;
                        lastScores[1] = orange_points;
                        if (blue_points > orange_points) {
                            lastResult = GetResult(client_name, teams[0].players);
                        } else {
                            lastResult = GetResult(client_name, teams[1].players);
                        }
                        state = lastResult + ": [" + lastScores[0] + ":" + lastScores[1] + "]";
                        break;
                    default:
                        state = game_status.Replace('_', ' ');
                        break;
                }
            }
            return state;
        }

        public static long GetEndTime(string match_type, float game_clock, string game_status) {
            long start = 0;
            if (match_type.Equals("Echo_Arena") || match_type.Equals("Echo_Arena_Private")) {
                if (!game_status.Equals("post_match") || !game_status.Equals("post_sudden_death")) {
                    start = DateTimeOffset.Now.ToUnixTimeSeconds();
                    start += (long)game_clock + 1;
                }
            }
            return start;
        }

        private static string GetResult(string client, Player[] players) {
            string result = "defeat";
            foreach (Player player in players) {
                if (player.name.Equals(client)) {
                    result = "victory";
                    break;
                }
            }
            return result;
        }
    }
}
