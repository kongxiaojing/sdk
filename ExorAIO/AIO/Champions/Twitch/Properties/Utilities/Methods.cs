using LeagueSharp;

namespace ExorAIO.Champions.Twitch
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
            Game.OnUpdate += Twitch.OnUpdate;
            Spellbook.OnCastSpell += Twitch.OnCastSpell;
        }
    }
}