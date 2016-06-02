using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;

namespace ExorAIO.Champions.Veigar
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
            ///     The Support Mode Option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Hybrid &&
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear);
            }

            /// <summary>
            ///     The No AA while in Combo option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["noaacombo"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Bools.HasSheenBuff() ||
                    GameObjects.Player.ManaPercent < 10 ||
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo ||
                    (!Vars.Q.IsReady() &&
                        !Vars.W.IsReady() &&
                        !Vars.E.IsReady()));
            }

            /// <summary>
            ///     The No AA while in LastHit/LaneClear option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["qfarmmode"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    !Vars.Q.IsReady() ||
                    (Variables.Orbwalker.ActiveMode != OrbwalkingMode.LastHit &&
                        Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear));
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Bools.HasTear(GameObjects.Player) &&
                !GameObjects.Player.IsRecalling() &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["miscellaneous"]["tear"]) &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast(Game.CursorPos);
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
                        t.IsValidTarget(Vars.W.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical)))
                {
                    Vars.W.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.E.GetPrediction(Targets.Target).AoeTargetsHitCount >=
                    Vars.Menu["spells"]["e"]["enemies"].GetValue<MenuSliderButton>().SValue &&
                Vars.Menu["spells"]["e"]["enemies"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
            }
        }
    }
}