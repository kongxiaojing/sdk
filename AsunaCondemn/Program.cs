using LeagueSharp.SDKEx;

namespace AsunaCondemn
{
    /// <summary>
    ///     The application class.
    /// </summary>
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
                Updater.Check();
            };
        }
    }
}