using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using SharpDX;

namespace NabbTracker
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class Drawings
    {
        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Initialize()
        {
            Drawing.OnDraw += delegate
            {
                foreach (var pg in GameObjects.Heroes.Where(
                    e =>
                        e.IsHPBarRendered &&
                        (e.IsMe && Vars.Menu["me"].GetValue<MenuBool>().Value ||
                            e.IsEnemy && Vars.Menu["enemies"].GetValue<MenuBool>().Value ||
                            e.IsAlly && !e.IsMe && Vars.Menu["allies"].GetValue<MenuBool>().Value)))
                {
                    for (var Spell = 0; Spell < Vars.SpellSlots.Count(); Spell++)
                    {
                        Vars.SpellX = (int)pg.HPBarPosition.X + Vars.SpellXAdjustment(pg) + Spell*25;
                        Vars.SpellY = (int)pg.HPBarPosition.Y + Vars.SpellYAdjustment(pg);

                        Vars.DisplayTextFont.DrawText(
                            null,
                            pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time > 0
                                ? string.Format("{0:0}", pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time + 1)
                                : Vars.SpellSlots[Spell].ToString(),

                            Vars.SpellX,
                            Vars.SpellY,

                            pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).Level < 1
                                ? Color.Gray
                                : pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).SData.ManaCostArray.MaxOrDefault(value => value) > pg.Mana
                                    ? Color.Cyan
                                    : pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time > 0 &&
                                      pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time <= 4
                                        ? (Vars.Menu["miscellaneous"]["colorblind"].GetValue<MenuBool>().Value 
                                            ? Color.FromBgra(0xFFF6F9C8)
                                            : Color.Yellow)
                                        : pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).CooldownExpires - Game.Time > 4
                                            ? Color.Red
                                            : (Vars.Menu["miscellaneous"]["colorblind"].GetValue<MenuBool>().Value 
                                                ? Color.FromBgra(0xFFB1AF27)
                                                : Color.LightGreen)
                        );

                        for (var DrawSpellLevel = 0; DrawSpellLevel <= pg.Spellbook.GetSpell(Vars.SpellSlots[Spell]).Level - 1; DrawSpellLevel++)
                        {
                            Vars.SpellLevelX = Vars.SpellX + DrawSpellLevel*3 - 4;
                            Vars.SpellLevelY = Vars.SpellY + 4;

                            Vars.DisplayTextFont.DrawText(null, ".", Vars.SpellLevelX, Vars.SpellLevelY, Color.White);
                        }
                    }

                    for (var SummonerSpell = 0; SummonerSpell < Vars.SummonerSpellSlots.Count(); SummonerSpell++)
                    {
                        Vars.SummonerSpellX = (int)pg.HPBarPosition.X + Vars.SummonerSpellXAdjustment(pg) + SummonerSpell*88;
                        Vars.SummonerSpellY = (int)pg.HPBarPosition.Y + Vars.SummonerSpellYAdjustment(pg);

                        switch (pg.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).Name.ToLower())
                        {
                            case "summonerflash":
                                Vars.GetSummonerSpellName = "Flash";
                                break;
                            case "summonerdot":
                                Vars.GetSummonerSpellName = "Ignite";
                                break;
                            case "summonerheal":
                                Vars.GetSummonerSpellName = "Heal";
                                break;
                            case "summonerteleport":
                                Vars.GetSummonerSpellName = "Teleport";
                                break;
                            case "summonerexhaust":
                                Vars.GetSummonerSpellName = "Exhaust";
                                break;
                            case "summonerhaste":
                                Vars.GetSummonerSpellName = "Ghost";
                                break;
                            case "summonerbarrier":
                                Vars.GetSummonerSpellName = "Barrier";
                                break;
                            case "summonerboost":
                                Vars.GetSummonerSpellName = "Cleanse";
                                break;
                            case "summonermana":
                                Vars.GetSummonerSpellName = "Clarity";
                                break;
                            case "summonerclairvoyance":
                                Vars.GetSummonerSpellName = "Clairvoyance";
                                break;
                            case "summonerodingarrison":
                                Vars.GetSummonerSpellName = "Garrison";
                                break;
                            case "summonersnowball":
                                Vars.GetSummonerSpellName = "Mark";
                                break;
                            default:
                                Vars.GetSummonerSpellName = "Smite";
                                break;
                        }

                        Vars.DisplayTextFont.DrawText(
                            null,
                            pg.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires - Game.Time > 0
                                ? Vars.GetSummonerSpellName + ":" + string.Format("{0:0}", pg.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires - Game.Time + 1)
                                : Vars.GetSummonerSpellName + ": UP ", Vars.SummonerSpellX, Vars.SummonerSpellY,
 
                            pg.Spellbook.GetSpell(Vars.SummonerSpellSlots[SummonerSpell]).CooldownExpires - Game.Time > 0
                                ? Color.Red 
                                : Color.Yellow);
                    }
                }
            };
        }
    }
}