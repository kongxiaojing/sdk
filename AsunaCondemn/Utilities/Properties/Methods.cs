using LeagueSharp;

namespace AsunaCondemn
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Condem.OnUpdate;
        }
    }
}