using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
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
            ///     The Stacking Logics.
            /// </summary>
            if (Vars.Q.IsReady() &
                GameObjects.Player.ManaPercent > ManaManager.NeededTearMana)
            {
                /// <summary>
                ///     The Tear Stacking Logic.
                /// </summary>
                if (Bools.HasTear(GameObjects.Player) &&
                    Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                    GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                    Vars.Menu["miscellaneous"]["tear"].GetValue<MenuBool>().Value)
                {
                    Vars.Q.Cast(Game.CursorPos);
                }

                /// <summary>
                ///     The Passive Stacking Logic.
                /// </summary>
                if (Vars.Menu["miscellaneous"]["stacks"].GetValue<MenuSliderButton>().BValue &&
                    Vars.Menu["miscellaneous"]["stacks"].GetValue<MenuSliderButton>().SValue >
                        GameObjects.Player.GetBuffCount("RyzePassiveStack"))
                {
                    Vars.Q.Cast(Game.CursorPos);
                }
            }
        }
    }
}