using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Quinn
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Quinn
    {
        /// <summary>
        ///     Loads Quinn.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            if (GameObjects.Player.IsWindingUp ||
                GameObjects.Player.IsRecalling() ||
                Vars.R.Instance.Name.Equals("QuinnRFinale"))
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Logics.Combo(args);
                    break;

                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;

                case OrbwalkingMode.LaneClear:
                    Logics.Clear(args);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Physical, false) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(args.Sender);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Vars.E.IsReady() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Physical, false) &&
                Vars.Menu["spells"]["e"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.E.CastOnUnit(args.Sender);
            }
        }

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnAction(object sender, OrbwalkingActionArgs args)
        {
            switch (args.Type)
            {
                case OrbwalkingType.BeforeAttack:

                    /// <summary>
                    ///     Check for R Instance.
                    /// </summary>
                    if (Vars.R.Instance.Name.Equals("QuinnRFinale"))
                    {
                        args.Process = false;
                    }

                    /// <summary>
                    ///     The Target Forcing Logic.
                    /// </summary>
                    if (args.Target is Obj_AI_Hero)
                    {
                        if (!GameObjects.EnemyHeroes.Any(
                            t =>
                                t.IsValidTarget(Vars.AARange) &&
                                t.HasBuff("quinnw")))
                        {
                            Variables.Orbwalker.ForceTarget = null;
                            return;
                        }

                        Variables.Orbwalker.ForceTarget = GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                t.IsValidTarget(Vars.AARange) &&
                                t.HasBuff("quinnw"));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}