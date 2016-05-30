using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace ExorAIO.Champions.Vayne
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
            /// <summary>
            ///     The Focus Logic (W Stacks).
            /// </summary>
            foreach (var target in GameObjects.EnemyHeroes.Where(
                t =>
                    t.IsValidTarget(Vars.AARange) &&
                    t.GetBuffCount("vaynesilvereddebuff") == 2))
            {
                Variables.Orbwalker.ForceTarget = target;
            }

            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Stealth Logic.
            /// </summary>
            if (GameObjects.Player.HasBuff("vaynetumblefade"))
            {
                Variables.Orbwalker.SetAttackState(
                    !GameObjects.Player.HasBuff("summonerexhaust") ||
                    !GameObjects.Player.HasBuffOfType(BuffType.Blind) ||
                    !Vars.Menu["miscellaneous"]["stealth"].GetValue<MenuBool>().Value);
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                !GameObjects.Player.IsDashing() &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(GameObjects.Player.BoundingRadius) &&
                        Vars.Menu["spells"]["e"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        if ((target.ServerPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                            (Vars.E2.GetPrediction(target).UnitPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&

                            (target.ServerPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall() &&
                            (Vars.E2.GetPrediction(target).UnitPosition + Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall())
                        {
                            Vars.E.CastOnUnit(target);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Automatic R -> Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.R.IsReady())
            {
                if (GameObjects.Player.HasBuff("summonerexhaust") ||
                    GameObjects.Player.HasBuffOfType(BuffType.Blind))
                {
                    Vars.R.Cast();

                    DelayAction.Add(GameObjects.Player.HasBuff("summonerexhaust") 
                        ? 1500 
                        : 500, () =>
                    {
                        Vars.Q.Cast(Game.CursorPos);
                    });
                }
            }
        }
    }
}