using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeagueSharp;

namespace ExorAIO.Core
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
                                c.DownloadString(
                                    "https://raw.githubusercontent.com/nabbhacker/SDKExoryREPO/master/ExorAIO/Properties/AssemblyInfo.cs");
                            var match =
                                new Regex(
                                    @"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]")
                                    .Match(rawVersion);

                            if (match.Success)
                            {
                                var gitVersion =
                                    new Version(
                                        $"{match.Groups[1]}.{match.Groups[2]}.{match.Groups[3]}.{match.Groups[4]}");

                                if (gitVersion != typeof(Updater).Assembly.GetName().Version)
                                {
                                    Game.PrintChat(
                                        $"[SDK]<b><font color='#009aff'>Exor</font></b>AIO: <font color='#009aff'>Ultima</font> - Outdated & newer version available!</font> ({gitVersion})");
                                }
                                else
                                {
                                    /// <summary>
                                    ///     Loads the AIO.
                                    /// </summary>
                                    AIO.OnLoad();
                                }
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Game.PrintChat(
                            "<font color=\"#FFF280\">Exception thrown at [SDK]ExorAIO.Updater, make a screenshot of the console and send it to Exory.");
                    }
                }
            );
        }
    }
}