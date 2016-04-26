using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Nunu
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Q LaneClear Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Minions.Any() &&
                (GameObjects.Player.ManaPercent > ManaManager.NeededQMana ||
                 GameObjects.Player.Buffs.Any(b => b.Name.Equals("visionary"))) &&
                Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(Targets.Minions.FirstOrDefault(m => m.Health < Vars.Q.GetDamage(m)));
            }

            /// <summary>
            ///     The E Clear Logics.
            /// </summary>
            if (Vars.E.IsReady() &&
                (GameObjects.Player.ManaPercent > ManaManager.NeededEMana ||
                 GameObjects.Player.Buffs.Any(b => b.Name.Equals("visionary"))) &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The E LaneClear Logic.
                /// </summary>
                foreach (var minion in
                    Targets.Minions.Where(m => m.IsValidTarget(Vars.E.Range) && m.Health < Vars.E.GetDamage(m)))
                {
                    Vars.E.CastOnUnit(minion);
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                if (Targets.JungleMinions.Any())
                {
                    Vars.E.CastOnUnit(Targets.JungleMinions[0]);
                }
            }
        }
    }
}