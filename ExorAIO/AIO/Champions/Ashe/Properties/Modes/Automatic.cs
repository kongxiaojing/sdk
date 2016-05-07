using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Ashe
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
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.W.Range)))
                {
                    if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any(c => c is Obj_AI_Minion))
                    {
                        Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1000f) == 0 &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["vision"]) &&
                Vars.Menu["spells"]["e"]["vision"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.EnemyHeroes.Any(
                    x =>
                        !x.IsDead &&
                        !x.IsVisible))
                {
                    if (GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).Ammo >=
                        (Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value ? 2 : 1))
                    {
                        Vars.E.Cast(Vars.Locations
                            .Where(d => GameObjects.Player.Distance(d) > 1500f)
                            .OrderBy(d2 => GameObjects.Player.Distance(d2))
                            .FirstOrDefault());
                    }
                }
            }
        }
    }
}