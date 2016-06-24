using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeagueSharp;

namespace NabbActivator
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
                                c.DownloadString("https://raw.githubusercontent.com/nabbhacker/SDKExoryREPO/master/NabbActivator/Properties/AssemblyInfo.cs");

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
                                        $"[SDK]<b><font color='#FF0000'>Nabb</font></b>Activator: <font color='#FF0000'>Ultima</font> - Outdated & newer version available!</font> ({gitVersion})");
                                    return;
                                }

                                /// <summary>
                                ///     Loads the activator index.
                                /// </summary>
                                Index.OnLoad();

                                /// <summary>
                                ///     Tells the player the assembly has been loaded.
                                /// </summary>
                                Game.PrintChat("[SDK]<b><font color='#FF0000'>Nabb</font></b>Activator: <font color='#FF0000'>Ultima</font> - Loaded!");
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Game.PrintChat("<font color=\"#FFF280\">Exception thrown at [SDK]NabbActivator.Updater, make a screenshot of the console and send it to Exory.");
                    }
                }
            );
        }
    }
}