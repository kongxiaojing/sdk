using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Lux
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
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() || !Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target) ||
                Targets.Target.HasBuff("luxilluminatingfraulein"))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() && Targets.Target.IsValidTarget(Vars.E.Range) &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState != 1 &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Count(x => x.IsMinion) <= 1 &&
                    !Vars.E.IsReady() || GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1)
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
                }
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() && Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            !Bools.HasAnyImmunity(t) && Bools.IsImmobile(Targets.Target) &&
                            t.IsValidTarget(Vars.R.Range)))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(target).CastPosition);
                }
            }
        }
    }
}