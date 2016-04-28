using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
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
            ///     The Clear R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.E.IsReady() &&
                Vars.Menu["spells"]["r"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear R Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    if (Targets.Minions.Count() >= 3)
                    {
                        Vars.R.Cast();
                    }
                }

                /// <summary>
                ///     The JungleClear R Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.R.Cast();
                }
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededQMana &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear Q Logic.
                /// </summary>
                foreach (var minion in Targets.Minions.Where(m => m.Health < Vars.Q.GetDamage(m)))
                {
                    if (!Vars.Q.GetPrediction(minion).CollisionObjects.Any(c => c.IsMinion))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(minion).UnitPosition);
                        return;
                    }
                }

                /// <summary>
                ///     The JungleClear Q Logic.
                /// </summary>
                if (Targets.JungleMinions.Any())
                {
                    Vars.Q.Cast(Targets.JungleMinions[0].ServerPosition);
                }
            }

            /// <summary>
            ///     The Clear W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededWMana &&
                Vars.Menu["spells"]["w"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear W Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    Vars.W.CastOnUnit(Targets.Minions[0]);
                }

                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                if (Targets.JungleMinions.Any())
                {
                    Vars.W.CastOnUnit(Targets.JungleMinions[0]);
                }
            }

            /// <summary>
            ///     The Clear E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > ManaManager.NeededEMana &&
                Vars.Menu["spells"]["e"]["clear"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     The LaneClear E Logic.
                /// </summary>
                if (Targets.Minions.Any())
                {
                    if (Targets.Minions.Count(m => m.Distance(Targets.Minions[0]) < 200f) >= 3)
                    {
                        Vars.W.CastOnUnit(Targets.Minions[0]);
                    }
                }

                /// <summary>
                ///     The JungleClear W Logic.
                /// </summary>
                else if (Targets.JungleMinions.Any())
                {
                    Vars.W.CastOnUnit(Targets.JungleMinions[0]);
                }
            }
        }
    }
}