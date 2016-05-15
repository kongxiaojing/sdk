using System;
using ExorAIO.Core;
using LeagueSharp.SDK;

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
            /// <summary>
            ///     Loads the Bootstrap.
            /// </summary>
            LeagueSharp.SDK.Bootstrap.Init(new string[1]);

            Events.OnLoad += (sender, eventArgs) =>
            {
                /// <summary>
                ///     Loads the Update checker.
                /// </summary>
                Updater.Check();
            };
        }
    }
}