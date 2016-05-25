namespace NabbTracker
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Tracker
    {
        /// <summary>
        ///     Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initialize the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initialize the SpellTracker.
            /// </summary>
            SpellTracker.Initialize();

            /// <summary>
            ///     Initialize the ExpTracker.
            /// </summary>
            ExpTracker.Initialize();
        }
    }
}