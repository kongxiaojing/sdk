using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;

namespace ExorAIO.Champions.Pantheon
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            Variables.Orbwalker.SetAttackState(!GameObjects.Player.HasBuff("pantheonesound"));
            Variables.Orbwalker.SetMovementState(!GameObjects.Player.HasBuff("pantheonesound"));
        }
    }
}