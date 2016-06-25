using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.MissFortune
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
            /// <summary>
            ///     The Q Harass & Clear Logics.
            /// </summary>
            if (Vars.Q.IsReady())
            {
				if (!GameObjects.EnemyHeroes.Any(
					t =>
						!Invulnerable.Check(t) &&
						!t.IsValidTarget(Vars.Q.Range) &&
						t.IsValidTarget(Vars.Q2.Range-50f) &&
						Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
				{
					/// <summary>
					///     The LaneClear Q Logic.
					/// </summary>
					if (Targets.Minions.Any() &&
						GameObjects.Player.ManaPercent >
							ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
						Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue)
					{
						if (Vars.Q2.GetLineFarmLocation(Targets.Minions, 60).MinionsHit == 2)
						{
							Vars.Q.CastOnUnit(Targets.Minions[0]);
						}
					}
				}
				else
				{
					/// <summary>
					///     The Q Minion Harass Logic.
					/// </summary>
					if (GameObjects.Player.ManaPercent > 
							ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["extended"]["exlaneclear"]) &&
						Vars.Menu["spells"]["q"]["extended"]["exlaneclear"].GetValue<MenuSliderButton>().BValue)
					{
						foreach (var minion 
							in from minion
							in Targets.Minions.Where(
                                m =>
                                    m.IsValidTarget(Vars.Q.Range) &&
                                    Vars.Menu["spells"]["q"]["extended"]["exlaneclearkill"].GetValue<MenuBool>().Value
                                        ? m.Health <
                                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)
                                        : true)

							let polygon = new Geometry.Sector(
                                (Vector2)minion.ServerPosition,
                                (Vector2)minion.ServerPosition.Extend(GameObjects.Player.ServerPosition, -(Vars.Q2.Range - Vars.Q.Range)),
                                40f * (float)Math.PI / 180f,
                                Vars.Q2.Range - Vars.Q.Range)

                            where
                                !polygon.IsOutside((Vector2)GameObjects.EnemyHeroes.FirstOrDefault(
                                t =>
                                    !Invulnerable.Check(t) &&
                                    !t.IsValidTarget(Vars.Q.Range) &&
                                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                                    Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value).ServerPosition) &&
                                !polygon.IsOutside((Vector2)Movement.GetPrediction(
                                    GameObjects.EnemyHeroes.FirstOrDefault(
                                    t =>
                                        !Invulnerable.Check(t) &&
                                        !t.IsValidTarget(Vars.Q.Range) &&
                                        t.IsValidTarget(Vars.Q2.Range-50f) &&
                                        Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value),
                                    GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                                    t =>
                                        !Invulnerable.Check(t) &&
                                        !t.IsValidTarget(Vars.Q.Range) &&
                                        t.IsValidTarget(Vars.Q2.Range-50f) &&
                                        Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value).ServerPosition) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

							select minion)
						{
							Vars.Q.CastOnUnit(minion);
						}
					}
				}
            }

            /// <summary>
            ///     The W Clear Logics.
            /// </summary>
            if (Vars.W.IsReady())
            {
                /// <summary>
                ///     The W JungleClear Logics.
                /// </summary>
                if (Targets.JungleMinions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["jungleclear"]) &&
                    Vars.Menu["spells"]["w"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast();
                }

                /// <summary>
                ///     The W LaneClear Logics.
                /// </summary>
                else if (Targets.Minions.Contains(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["laneclear"]) &&
                    Vars.Menu["spells"]["w"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.W.Cast();
                }
            }

            /// <summary>
            ///     The E Clear Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The E JungleClear Logics.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                    Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.E.Cast(Targets.JungleMinions[0]);
                }

                /// <summary>
                ///     The E LaneClear Logics.
                /// </summary>
                else if (Targets.Minions.Any() &&
                    GameObjects.Player.ManaPercent >
                        ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                    Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.E.Cast(Targets.Minions[0]);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void JungleClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
			/// <summary>
			///     The JungleClear Q Logic.
			/// </summary>
			if (Vars.Q.IsReady() &&
				Targets.JungleMinions.Any(m => m.IsValidTarget(Vars.Q.Range)) &&
				GameObjects.Player.ManaPercent >
					ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
				Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
			{
                Vars.Q.CastOnUnit(Targets.JungleMinions[0]);
				return;
			}
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void BuildingClear(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(Variables.Orbwalker.GetTarget() is Obj_HQ) &&
                !(Variables.Orbwalker.GetTarget() is Obj_AI_Turret) &&
                !(Variables.Orbwalker.GetTarget() is Obj_BarracksDampener))
            {
                return;
            }

            /// <summary>
            ///     The W BuildingClear Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["buildings"]) &&
                Vars.Menu["spells"]["w"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.W.Cast();
            }
        }
    }
}