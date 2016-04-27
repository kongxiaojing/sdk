using LeagueSharp;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace NabbAlerter
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Alerter
    {
        /// <summary>
        ///     Called when the game loads itself.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();
        }

        /// <summary>
        ///     Handles the <see cref="E:ProcessSpell" /> event.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="Spell">The <see cref="GameObjectProcessSpellCastEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Vars.Menu["enable"].GetValue<MenuBool>().Value)
            {
                return;
            }

            if (sender == null)
            {
                return;
            }

            if (sender.IsMe ||
                !sender.IsEnemy ||
                !(sender is Obj_AI_Hero))
            {
                return;
            }

            /// <summary>
            ///     Check for the Not included Champions.
            /// </summary>
            if (Vars.NotIncludedChampions.Contains((sender as Obj_AI_Hero).ChampionName.ToLower()))
            {
                return;
            }

            /// <summary>
            ///     The Ultimate (R).
            /// </summary>
            if (args.Slot == SpellSlot.R &&
                Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["ultimate"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Annie can cast R multiple times to move tibbers, let's just not spam it once she already casted R.
                /// </summary>
                if ((sender as Obj_AI_Hero).ChampionName.Equals("Annie") &&
                    !args.SData.Name.Equals("InfernalGuardian"))
                {
                    return;
                }

                /// <summary>
                ///     Jhin can cast R four times, let's just not spam it.
                /// </summary>
                if ((sender as Obj_AI_Hero).ChampionName.Equals("Jhin") &&
                    !args.SData.Name.Equals("JhinR"))
                {
                    return;
                }

                /// <summary>
                ///     Zed can cast R two times, let's just not spam it.
                /// </summary>
                if ((sender as Obj_AI_Hero).ChampionName.Equals("Zed") &&
                    !args.SData.Name.Equals("ZedR"))
                {
                    return;
                }

                /// <summary>
                ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                /// </summary>
                DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                {
                    /// <summary>
                    ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                    /// </summary>
                    switch (WeightedRandom.Next(1, 4))
                    {
                        case 1:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no ulti");
                            break;

                        case 2:
                            Game.Say($"no ult {Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        case 3:
                            Game.Say($"ult {Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        default:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} ult");
                            break;
                    }
                });
            }

            /// <summary>
            ///     The First SummonerSpell.
            /// </summary>
            if (args.Slot == SpellSlot.Summoner1 &&
                Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower()) != null &&
                Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["sum1"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                /// </summary>
                DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                {
                    /// <summary>
                    ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                    /// </summary>
                    switch (WeightedRandom.Next(1, 4))
                    {
                        case 1:
                            Game.Say($"no {Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())} " +
                                     $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        case 2:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no " +
                                     $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())}");
                            break;

                        case 3:
                            Game.Say($"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())} " +
                                     $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        default:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} " +
                                     $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[4].Name.ToLower())}");
                            break;
                    }
                });
            }

            /// <summary>
            ///     The Second SummonerSpell.
            /// </summary>
            if (args.Slot == SpellSlot.Summoner2 &&
                Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower()) != null &&
                Vars.Menu[(sender as Obj_AI_Hero).ChampionName.ToLower()]["sum2"].GetValue<MenuBool>().Value)
            {
                /// <summary>
                ///     Let's delay the alert by 5-10 seconds since we're not Sean Wrona.
                /// </summary>
                DelayAction.Add(WeightedRandom.Next(5000, 10000), () =>
                {
                    /// <summary>
                    ///     Then we randomize the whole output (Structure/Names/SpellNames) to make it seem totally legit.
                    /// </summary>
                    switch (WeightedRandom.Next(1, 4))
                    {
                        case 1:
                            Game.Say($"no {Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())} " +
                                     $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        case 2:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} no " +
                                     $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())}");
                            break;

                        case 3:
                            Game.Say($"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())} " +
                                     $"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())}");
                            break;

                        default:
                            Game.Say($"{Vars.GetHumanName((sender as Obj_AI_Hero).ChampionName.ToLower())} " +
                                     $"{Vars.GetHumanSpellName((sender as Obj_AI_Hero).Spellbook.Spells[5].Name.ToLower())}");
                            break;
                    }
                });
            }
        }
    }
}