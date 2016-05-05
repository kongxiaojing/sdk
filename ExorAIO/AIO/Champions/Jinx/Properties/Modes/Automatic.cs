using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Jinx
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
            ///     The Q Switching Logics.
            /// </summary>
            if (Vars.Q.IsReady())
            {
                /// <summary>
                ///     PowPow.Range -> FishBones Logics.
                /// </summary>
                if (!GameObjects.Player.HasBuff("JinxQ"))
                {
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        /// <summary>
                        ///     The Q Combo Enable Logics,
                        ///     The Q Harass Enable Logics.
                        /// </summary>
                        case OrbwalkingMode.Combo:
                        case OrbwalkingMode.Hybrid:

                            /// <summary>
                            ///     Start if:
                            ///     The target is a valid minion. (Target Check).
                            /// </summary>
                            if (!Targets.Target.IsValidTarget())
                            {
                                return;
                            }

                            /// <summary>
                            ///     Enable if:
                            ///     If you are in combo mode, the combo option is enabled. (Option check).
                            /// </summary>
                            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo)
                            {
                                if (!Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                                {
                                    return;
                                }
                            }

                            /// <summary>
                            ///     Enable if:
                            ///     If you are in mixed mode, the mixed option is enabled.. (Option check).
                            ///     and respects the ManaManager check. (Mana check).
                            /// </summary>
                            else if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Hybrid)
                            {
                                if (GameObjects.Player.ManaPercent <
                                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["q"]["harass"]) ||
                                    !Vars.Menu["spells"]["q"]["harass"].GetValue<MenuSliderButton>().BValue)
                                {
                                    return;
                                }
                            }

                            /// <summary>
                            ///     Enable if:
                            ///     2 Or more enemies in explosion range from the target. (AOE Logic),
                            ///     No hero in PowPow Range but 1 or more heroes in FishBones range. (Range Logic).
                            /// </summary>
                            if (Variables.Orbwalker.GetTarget() as Obj_AI_Hero != null &&
                                (Variables.Orbwalker.GetTarget() as Obj_AI_Hero).IsValidTarget())
                            {
                                /// <summary>
                                ///     Disable if:
                                ///     Less than 2 enemies in explosion range from the target. (AOE Logic),
                                ///     Any hero in PowPow Range. (Range Logic).
                                /// </summary>
                                if ((Variables.Orbwalker.GetTarget() as Obj_AI_Hero).CountEnemyHeroesInRange(200f) >= 2 &&
                                    !GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                            }
                            break;

                        /// <summary>
                        ///     The Q Clear Enable Logics.
                        /// </summary>
                        case OrbwalkingMode.LaneClear:

                            /// <summary>
                            ///     Start if:
                            ///     It respects the ManaManager Check, (Mana check),
                            ///     The Clear Option is enabled. (Option check).
                            /// </summary>
                            if (GameObjects.Player.ManaPercent <
                                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["q"]["clear"]) ||
                                !Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
                            {
                                return;
                            }

                            /// <summary>
                            ///     Start if:
                            ///     The target is a valid minion. (Target Check),
                            /// </summary>
                            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion != null &&
                                (Variables.Orbwalker.GetTarget() as Obj_AI_Minion).IsValidTarget())
                            {
                                /// <summary>
                                ///     Enable if:
                                ///     No minion in PowPow Range but 1 or more minions in FishBones range. (Lane Range Logic).
                                ///     Or 2 or more minions in explosion range from the minion target. (Lane AoE Logic).
                                /// </summary>
                                if (Targets.Minions.Any(
                                    m =>
                                        m.IsValidTarget(Vars.Q.Range) &&
                                        !m.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                                else if (Targets.Minions
                                        .Count(m2 => m2.Distance(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) < 200f) >= 3)
                                {
                                    Vars.Q.Cast();
                                }

                                /// <summary>
                                ///     Enable if:
                                ///     No jungle minion in PowPow Range but 1 or more jungle minions in FishBones range. (Jungle Range Logic).
                                ///     Or 1 or more jungle minions in explosion range from the jungle minion target. (Jungle AOE Logic).
                                /// </summary>
                                if (Targets.JungleMinions.Any(
                                    m =>
                                        m.IsValidTarget(Vars.Q.Range) &&
                                        !m.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                                else if (Targets.JungleMinions
                                        .Count(m2 => m2.Distance(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) < 200f) >= 2)
                                {
                                    Vars.Q.Cast();
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                /// <summary>
                ///     FishBones -> PowPow.Range Logics.
                /// </summary>
                else
                {
                    switch (Variables.Orbwalker.ActiveMode)
                    {
                        /// <summary>
                        ///     The Q Clear Disable Logics.
                        /// </summary>
                        case OrbwalkingMode.LaneClear:

                            /// <summary>
                            ///     Disable if:
                            ///     Doesn't respect the ManaManager Check, (Mana check).
                            ///     The Clear Option is disabled. (Option check).
                            /// </summary>
                            if (GameObjects.Player.ManaPercent <
                                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["clear"]) ||
                                !Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
                            {
                                Vars.Q.Cast();
                            }

                            /// <summary>
                            ///     Disable if:
                            ///     The target is not a valid minion. (Target Check).
                            /// </summary>
                            if (Variables.Orbwalker.GetTarget() as Obj_AI_Minion != null &&
                                (Variables.Orbwalker.GetTarget() as Obj_AI_Minion).IsValidTarget())
                            {
                                /// <summary>
                                ///     Disable if:
                                ///     Any minion in PowPow Range. (Lane Range Logic).
                                ///     And less than 2 minions in explosion range from the minion target (Lane AoE Logic).
                                /// </summary>
                                if (!Targets.Minions.Any(
                                    m =>
                                        m.IsValidTarget(Vars.Q.Range) &&
                                        !m.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                                else if (Targets.Minions
                                        .Count(m2 => m2.Distance(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) < 250f) < 3)
                                {
                                    Vars.Q.Cast();
                                }

                                /// <summary>
                                ///     Disable if:
                                ///     Any minion in PowPow range. (Jungle Range Logic).
                                ///     And no minions in explosion range from the minion target (Jungle AoE Logic).
                                /// </summary>
                                if (!Targets.JungleMinions.Any(
                                    m =>
                                        m.IsValidTarget(Vars.Q.Range) &&
                                        !m.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                                else if (Targets.JungleMinions
                                        .Count(m2 => m2.Distance(Variables.Orbwalker.GetTarget() as Obj_AI_Minion) < 200f) < 2)
                                {
                                    Vars.Q.Cast();
                                }
                            }
                            else
                            {
                                Vars.Q.Cast();
                            }
                            break;

                        /// <summary>
                        ///     General Q disabling logic.
                        /// </summary>
                        default:

                            /// <summary>
                            ///     Disable if:
                            ///     If you are in combo mode, the combo option is disabled. (Option check).
                            /// </summary>
                            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo)
                            {
                                if (!Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                                {
                                    Vars.Q.Cast();
                                }
                            }

                            /// <summary>
                            ///     Disable if:
                            ///     If you are in mixed mode, the mixed option is disabled.. (Option check).
                            ///     or it doesn't respect the ManaManager check. (Mana check).
                            /// </summary>
                            else if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Hybrid)
                            {
                                if (GameObjects.Player.ManaPercent <
                                        ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["q"]["harass"]) ||
                                    !Vars.Menu["spells"]["q"]["harass"].GetValue<MenuSliderButton>().BValue)
                                {
                                    Vars.Q.Cast();
                                }
                            }

                            /// <summary>
                            ///     Disable if:
                            ///     The target is not a hero. (Target check),
                            /// </summary>
                            if (Variables.Orbwalker.GetTarget() as Obj_AI_Hero != null &&
                                (Variables.Orbwalker.GetTarget() as Obj_AI_Hero).IsValidTarget())
                            {
                                /// <summary>
                                ///     Disable if:
                                ///     Less than 2 enemies in explosion range from the target. (AOE Logic),
                                ///     Any hero in PowPow Range. (Range Logic).
                                /// </summary>
                                if ((Variables.Orbwalker.GetTarget() as Obj_AI_Hero).CountEnemyHeroesInRange(200f) < 2 &&
                                    GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.PowPow.Range)))
                                {
                                    Vars.Q.Cast();
                                }
                            }
                            else
                            {
                                Vars.Q.Cast();
                            }
                            break;
                    }
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.W.Range)))
                {
                    if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                    {
                        Vars.W.Cast(target.ServerPosition);
                        return;
                    }
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        t.IsValidTarget(Vars.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vars.E.Cast(target.ServerPosition);
                }
            }
        }

        /// <summary>
        ///     Called upon calling a spelaneclearlearast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellbookCastSpellEventArgs" /> instance containing the event data.</param>
        public static void Automatic(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            /// <summary>
            ///     The Q Switching Logics.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["miscellaneous"]["blockq"].GetValue<MenuBool>().Value)
            {
                if (GameObjects.Player.HasBuff("JinxQ"))
                {
                    return;
                }

                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    ///     The Q Clear Enable Logics.
                    /// </summary>
                    case OrbwalkingMode.LaneClear:

                        /// <summary>
                        ///     Block if:
                        ///     It doesn't respect the ManaManager Check, (Mana check),
                        ///     The Clear Option isn't enabled. (Option check).
                        /// </summary>
                        if (GameObjects.Player.ManaPercent < ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["manamanager"]))
                        {
                            args.Process = false;
                        }
                        break;
                }
            }
        }
    }
}