using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Lucian
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
            ///     The Automatic R Orbwalking.
            /// </summary>
            if (GameObjects.Player.HasBuff("LucianR"))
            {
                DelayAction.Add(
                    (int) (100 + Game.Ping / 2f),
                    () => { GameObjects.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos); });
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (Vars.R.IsReady() && Targets.Target.IsValidTarget(Vars.R.Range) &&
                Vars.Menu["spells"]["r"]["boolrsa"].GetValue<MenuBool>().Value)
            {
                if (!GameObjects.Player.HasBuff("LucianR") &&
                    Vars.Menu["spells"]["r"]["keyrsa"].GetValue<MenuKeyBind>().Active)
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).CastPosition);
                }
                else if (GameObjects.Player.HasBuff("LucianR") &&
                         !Vars.Menu["spells"]["r"]["keyrsa"].GetValue<MenuKeyBind>().Active)
                {
                    Vars.R.Cast();
                }
            }
        }
    }
}