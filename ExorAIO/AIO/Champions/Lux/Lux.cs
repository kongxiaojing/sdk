using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Lux
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Lux
    {
        /// <summary>
        ///     Defines the missile object for the E.
        /// </summary>
        public static GameObject EMissile = null;

        /// <summary>
        ///     Loads Lux.
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
            GameObject.OnCreate += delegate(GameObject obj, EventArgs args2)
            {
                if (obj.Name.Contains("Lux_Base_E_tar"))
                {
                    EMissile = obj;
                }
            };

            GameObject.OnDelete += delegate(GameObject obj, EventArgs args2)
            {
                if (obj.Name.Contains("Lux_Base_E_tar"))
                {
                    EMissile = null;
                }
            };

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

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

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
            if (Vars.Q.IsReady() &&
                args.IsDirectedToPlayer &&
                args.Sender.IsValidTarget(Vars.Q.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                Vars.Menu["spells"]["q"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(args.Sender.ServerPosition);
            }
        }
    }
}