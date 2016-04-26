using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Kalista
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Kalista
    {
        /// <summary>
        ///     Loads Kalista.
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

            /// <summary>
            ///     Initializes the damage drawings.
            /// </summary>
            Healthbars.Initialize();
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

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            if (GameObjects.Player.IsWindingUp || GameObjects.Player.IsDashing())
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && AutoAttack.IsAutoAttack(args.SData.Name) &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo)
            {
                Logics.Weaving(sender, args);
            }
        }

        /// <summary>
        ///     Triggers when there is a valid unkillable minion around.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnOrbwalkerAction(object sender, OrbwalkingActionArgs args)
        {
            if (!args.Target.IsValidTarget())
            {
                return;
            }

            switch (args.Type)
            {
                case OrbwalkingType.NonKillableMinion:
                    OnNonKillableMinion(args);
                    break;
            }
        }

        /// <summary>
        ///     Triggers when there is a valid unkillable minion around.
        /// </summary>
        /// <param name="args">The <see cref="OrbwalkingActionArgs" /> instance containing the event data.</param>
        public static void OnNonKillableMinion(OrbwalkingActionArgs args)
        {
            if (Vars.E.IsReady() && Bools.IsPerfectRendTarget(args.Target as Obj_AI_Minion) &&
                (args.Target as Obj_AI_Minion).Health < KillSteal.GetPerfectRendDamage(args.Target as Obj_AI_Minion))
            {
                Vars.E.Cast();
            }
        }
    }
}