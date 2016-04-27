using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Cassiopeia
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

            /// <summary>
            ///     The No AA while in Combo option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["noaa"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Bools.HasSheenBuff() ||
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo ||
                    GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).Level < 1);
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Bools.HasTear(GameObjects.Player) &&
                !GameObjects.Player.IsRecalling() &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                GameObjects.Player.ManaPercent > ManaManager.NeededTearMana &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Game.CursorPos);
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Bools.HasAnyImmunity(t, true) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(target.ServerPosition);
                    return;
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !Vars.Q.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Bools.HasAnyImmunity(t, true) &&
                        t.IsValidTarget(Vars.W.Range)))
                {
                    Vars.W.Cast(target.ServerPosition);
                }
            }
        }
    }
}