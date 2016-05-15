using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.SDK;

namespace AsunaCondemn
{
    /// <summary>
    ///     The Updater class.
    /// </summary>
    public static class Updater
    {
        /// <summary>
        ///     Checks the assembly version and compares it to the respective remote github folder's.
        /// </summary>
        public static void Check()
        {
            Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        using (var c = new WebClient())
                        {
                            var rawVersion =
                                c.DownloadString("https://raw.githubusercontent.com/nabbhacker/SDKExoryREPO/master/AsunaCondemn/Properties/AssemblyInfo.cs");

                            var match =
                                new Regex(
                                    @"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]")
                                    .Match(rawVersion);

                            if (match.Success)
                            {
                                var gitVersion = new Version($"{match.Groups[1]}.{match.Groups[2]}.{match.Groups[3]}.{match.Groups[4]}");

                                if (gitVersion != typeof(Updater).Assembly.GetName().Version)
                                {
                                    Game.PrintChat(
                                        $"[SDK]<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Outdated & newer version available!</font> ({gitVersion})");
                                    return;
                                }

                                if (!GameObjects.Player.ChampionName.Equals("Vayne"))
                                {
                                    Game.PrintChat(
                                        $"[SDK]<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Not Loaded: Vayne not Found.</font>");
                                    return;
                                }

                                /// <summary>
                                ///     Loads the assembly.
                                /// </summary>
                                Condem.OnLoad();

                                /// <summary>
                                ///     Tells the player the assembly has been loaded.
                                /// </summary>
                                Game.PrintChat("[SDK]<b><font color='#009aff'>Asuna</font></b>Condemn: <font color='#009aff'>Ultima</font> - Loaded!");
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Game.PrintChat("<font color=\"#FFF280\">Exception thrown at [SDK]AsunaCondemn.Updater, make a screenshot of the console and send it to Exory.");
                    }
                }
            );
        }
    }
}