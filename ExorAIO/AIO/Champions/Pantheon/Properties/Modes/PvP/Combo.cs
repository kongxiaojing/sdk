using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

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
        public static void Combo(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() || Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            if (!Bools.HasSheenBuff() || !Targets.Target.IsValidTarget(Vars.AARange))
            {
                /// <summary>
                ///     The Combo Q Logic.
                /// </summary>
                if (Vars.Q.IsReady() && Targets.Target.IsValidTarget(Vars.Q.Range) &&
                    !GameObjects.Player.HasBuff("pantheonesound") &&
                    !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                    Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                {
                    Vars.Q.CastOnUnit(Targets.Target);
                }

                /// <summary>
                ///     The Combo W Logic.
                /// </summary>
                if (Vars.W.IsReady() && !Bools.IsImmobile(Targets.Target) && Targets.Target.IsValidTarget(Vars.W.Range) &&
                    !GameObjects.Player.HasBuff("pantheonesound") &&
                    Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
                {
                    if (!Targets.Target.IsValidTarget(Vars.AARange) ||
                        !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                        GameObjects.Player.GetBuffCount("pantheonpassivecounter") < 3)
                    {
                        Vars.W.CastOnUnit(Targets.Target);
                    }
                }

                /// <summary>
                ///     The Combo E Logic.
                /// </summary>
                if (Vars.E.IsReady() && !GameObjects.Player.HasBuff("pantheonpassiveshield") &&
                    GameObjects.Player.CountEnemyHeroesInRange(Vars.E.Range) > 0 &&
                    Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
                {
                    Vars.E.Cast(Targets.Target.Position);
                }
            }
        }
    }
}