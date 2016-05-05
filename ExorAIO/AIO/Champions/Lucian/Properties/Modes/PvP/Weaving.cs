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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void Weaving(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is Obj_AI_Hero) ||
                Invulnerable.Check(args.Target as Obj_AI_Hero))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!GameObjects.Player.IsUnderEnemyTurret() ||
                    (args.Target as Obj_AI_Hero).Health < GameObjects.Player.GetAutoAttackDamage(args.Target as Obj_AI_Hero)*2)
                {
                    if ((args.Target as Obj_AI_Hero).CountEnemyHeroesInRange(700f) >= 2 ||
                        GameObjects.Player.Distance(Game.CursorPos) < Vars.AARange)
                    {
                        Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(Game.CursorPos, 25));
                    }
                    else
                    {
                        Vars.E.Cast(Game.CursorPos);
                    }
                    return;
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                (args.Target as Obj_AI_Hero).IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.CastOnUnit(args.Target as Obj_AI_Hero);
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(Vars.W.GetPrediction(args.Target as Obj_AI_Hero).UnitPosition);
            }
        }
    }
}