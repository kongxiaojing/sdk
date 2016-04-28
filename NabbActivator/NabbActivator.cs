using System;
//using System.Linq;
using LeagueSharp;

namespace NabbActivator
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Index
    {
        /// <summary>
        ///     Loads the Activator.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initialize the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initialize the spells.
            /// </summary>
            ISpells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the resetters.
            /// </summary>
            Resetters.Initialize();
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            /*
            if (Targets.Target != null)
            {
                foreach (var buff in Targets.Target.Buffs.Where(b => b.Caster.IsMe))
                {
                    Console.WriteLine(buff.Name);
                }
            }
            */

            /*
            foreach (var buff in ObjectManager.Player.Buffs.Where(b => b.Caster.IsMe))
            {
                Console.WriteLine(buff.Name);
            }
            */

            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Loads the spells logics.
            /// </summary>
            Activator.Spells(args);

            /// <summary>
            ///     Loads the offensives logics.
            /// </summary>
            Activator.Offensives(args);

            /// <summary>
            ///     Loads the defensives logics.
            /// </summary>
            Activator.Defensives(args);

            /// <summary>
            ///     Loads the consumables logics.
            /// </summary>
            Activator.Consumables(args);
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            /// <summary>
            ///     Loads the special items logics.
            /// </summary>
            Activator.Specials(sender, args);

            /// <summary>
            ///     Loads the resetter-items logics.
            /// </summary>
            Activator.Resetters(sender, args);
        }

        /// <summary>
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="args">The <see cref="Obj_AI_BaseBuffAddEventArgs" /> instance containing the event data.</param>
        public static void OnBuffAdd(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            /// <summary>
            ///     Loads the cleansers logics.
            /// </summary>
            Activator.Cleansers(sender, args);
        }
    }
}