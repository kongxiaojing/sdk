using LeagueSharp;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Lux
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Lux.OnUpdate;
            Events.OnGapCloser += Lux.OnGapCloser;
        }
    }
}