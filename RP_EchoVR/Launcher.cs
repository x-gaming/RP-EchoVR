/*  this program updates the discord rich presence for the game Echo VR
 *  it displays various inforamtion about the game using the echo API
 * 
 *  author: x_gaming 
 *  version: 1.0
 *  created: 03.11.2020
 */

using Discord;

namespace RP_EchoVR {
    class Launcher {
        static void Main(string[] args) {
            DiscordController controller;
            if (args.Length == 0) {
                System.Console.Title = "RP_EchoVR";
                controller = new DiscordController(true);
            } else {
                controller = new DiscordController();
            }
            controller.Init(773432354274410527);
        }
    }
}
