using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityYharonChange.Buffs;
using CalamityYharonChange.Content.Buffs;
using CalamityYharonChange.Content.NPCs.YharonNPC;
using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.Projs.Bosses.Yharon;
using CalamityYharonChange.Content.Systems;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange
{
    public class CalamtiyYharonPlayer : ModPlayer
    {
        /// <summary>
        /// 炼狱龙炎
        /// </summary>
        public bool hellDragonFire;
        /// <summary>
        /// 撕裂buff
        /// </summary>
        public bool flyWind;
        public int PlayerFly;
        public int DmgDec=0;
        public bool canBeHitByRing = true;
        public override void ResetEffects()
        {
            hellDragonFire = false;
            flyWind = false;
        }
        public override void UpdateDead()
        {
            hellDragonFire = false;
            flyWind = false;
            DmgDec = 0;
        }
        public override void PreUpdate()
        {
            if (YharonChangeSystem.YharonBoss != -1)
            {
                if(YharonNPC.Mode != 4)
                {
                    if (Math.Abs(Player.Center.X - YharonChangeSystem.YharonFixedPos.X) > 960 || Math.Abs(Player.Center.Y - YharonChangeSystem.YharonFixedPos.Y) > 4000)
                    {
                        Player.AddBuff(ModContent.BuffType<HellDragonfire>(), 20);
                    }
                }
                else
                {
                    foreach (Projectile p in Main.projectile)
                    {
                        if (p is not null && p.active && p.timeLeft > 0)
                        {
                            if (p.type == ModContent.ProjectileType<LimitArea>())
                            {
                                float d = p.ai[1];
                                if (d > 0)
                                {
                                    if (Vector2.Distance(Player.Center, p.Center) > d)
                                    {
                                        Player.AddBuff(ModContent.BuffType<HellDragonfire>(), 20);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (hellDragonFire)
            {
                damageSource = PlayerDeathReason.ByCustomReason(NetworkText.FromLiteral(CalamityUtils.GetText("Status.Death.Dragonfire" + Main.rand.Next(1, 5)).Format(base.Player.name)));
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (hellDragonFire && drawInfo.shadow == 0f)
            {
                Dragonfire.DrawEffects(drawInfo);
            }
        }
        public override void UpdateBadLifeRegen()
        {
            ApplyDoTDebuff(hellDragonFire, 200);
            ApplyDoTDebuff(flyWind, 100);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (DmgDec > 0)
            {
                modifiers.FinalDamage *= (10 - DmgDec) / 10f;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (DmgDec > 0)
            {
                modifiers.FinalDamage *= (10 - DmgDec) / 10f;
            }
        }
        public override void PreUpdateMovement()
        {
            if(PlayerFly > 0)
            {
                PlayerFly--;
                Player.velocity.Y = -90f;
            }
            if (YharonChangeSystem.YharonBoss != -1)
            {
                if (YharonNPC.Mode != 4)
                {
                    if (Math.Abs(Player.Center.X - YharonChangeSystem.YharonFixedPos.X) > 960 || Math.Abs(Player.Center.Y - YharonChangeSystem.YharonFixedPos.Y) > 4000)
                    {
                        Player.velocity += (YharonChangeSystem.YharonFixedPos - Player.Center).SafeNormalize(default);
                    }
                }
                else
                {
                    foreach (Projectile p in Main.projectile)
                    {
                        if (p is not null && p.active && p.timeLeft > 0)
                        {
                            if (p.type == ModContent.ProjectileType<LimitArea>())
                            {
                                float d = p.ai[1];
                                if (d > 1000f)
                                {
                                    if (Vector2.Distance(Player.Center, YharonChangeSystem.YharonFixedPos) > d)
                                    {
                                        Player.Center = (-YharonChangeSystem.YharonFixedPos + Player.Center).SafeNormalize(default) * d + YharonChangeSystem.YharonFixedPos;
                                        Player.AddBuff(ModContent.BuffType<DmgDecrease>(),3600);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            base.PreUpdateMovement();
        }
        void ApplyDoTDebuff(bool hasDebuff, int negativeLifeRegenToApply, bool immuneCondition = false)
        {
            if (!(!hasDebuff || immuneCondition))
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0f;
                Player.lifeRegen -= negativeLifeRegenToApply;
            }
        }
        public override void ModifyScreenPosition()
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active &&npc.life > 0 && npc.type == ModContent.NPCType<YharonNPC>())
                {
                    if (YharonNPC.Mode == 4 && YharonNPC.Killed)
                    {
                        float t = npc.ai[0];
                        if (t < 30) Main.screenPosition = Vector2.Lerp(Main.screenPosition, npc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), t / 30f);
                        if (t >= 30 && t < 150) Main.screenPosition = npc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                        if (t >= 150 && t < 180)
                        {
                            float tt = t - 150;
                            Main.screenPosition = Vector2.Lerp(Main.screenPosition, npc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2),(30 - tt) / 30f);
                        }
                    }
                    if(YharonNPC.Mode == 4)
                    {
                        foreach (Projectile p in Main.projectile)
                        {
                            if (p.type == ModContent.ProjectileType<FlareRingP4>() && p.timeLeft > 0)
                            {
                                if (p.ai[2]> 20)
                                {
                                    float max = p.ai[2];
                                    float t = p.timeLeft;
                                    float value = max / 2 - Math.Abs(-t + max / 2);
                                    Main.screenPosition += Main.rand.NextVector2CircularEdge(value, value) * 3;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
