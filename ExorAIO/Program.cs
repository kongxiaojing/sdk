using ExorAIO.Core;
using LeagueSharp.SDK;
using Bootstrap = LeagueSharp.SDK.Bootstrap;

namespace ExorAIO
{
    internal class Program
    {
        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Events.OnLoad += (sender, eventArgs) =>
            {
                /// <summary>
                ///     Loads the Update checker.
                /// </summary>
                Updater.Check();

                /// <summary>
                ///     Loads the Bootstrap.
                /// </summary>
                Bootstrap.Init(new string[1]);
            };
        }
    }
}