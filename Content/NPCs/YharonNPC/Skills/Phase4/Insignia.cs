using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityYharonChange.Buffs;
using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.Projs.Bosses.Yharon;
using CalamityYharonChange.Content.Systems;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class Insignia:BasicPhase4Skills
    {
        public Insignia(NPC nPC) : base(nPC) { }
        public override void AI()
        {
            NPC.velocity *= 0f;
            NPC.ai[0]++;
            if (NPC.ai[0] < 1280) NPC.dontTakeDamage = true;
            else NPC.dontTakeDamage = false;
            Lighting.AddLight(NPC.Center, TorchID.White);
            if (NPC.ai[0] < 300) NPC.life = 1;
            else if (NPC.life < NPC.lifeMax) NPC.life += (int)(NPC.lifeMax / 300f);
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
            if (NPC.ai[0] == 1)
            {
                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<LimitArea>(), 0, 0f, Target.whoAmI, NPC.whoAmI, 16*30);
                p.timeLeft = 210;
                Target.AddBuff(ModContent.BuffType<DoomKotodama>(), 60);
                Main.NewText(
                    "这个残破扭曲的世界", 
                    Color.Orange);
                Main.NewText(
                    "一心为龙的人类只有一位",
                    Color.Orange);
                Main.NewText(
                    "一心为人的龙族也只有一条！",
                    Color.Orange);
            }
            if (NPC.ai[0] == 300)
            {
                Main.NewText(
                    "无上至尊的远古龙神啊！",
                    Color.Orange);
                Main.NewText(
                    "请赐予我再一次燃烧的权利！",
                    Color.Orange);
                Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FlareRingP4>(), 0, 0f, Target.whoAmI, 0, 2100, 30);
            }
            if (NPC.ai[0]> 300 && NPC.ai[0]< 600)
            {
                float v = (float)NPC.life / (float)NPC.lifeMax;
                for ( float i = .2f; i <= .6f; i += .2f)
                {
                    if (Math.Abs(i-v)< .02f)
                    {
                        bool flag = false;
                        foreach(Projectile p in Main.projectile)
                        {
                            if (p.type == ModContent.ProjectileType<FlareRingP4>() && p.timeLeft > 0 && p.active)
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            Projectile p = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FlareRingP4>(), 0, 0f, Target.whoAmI, 0, 1800, 20);
                            p.timeLeft = 20;
                        }
                    }
                }
            }
            if (NPC.ai[0] == 720)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, YharonNPC.YharonRoarWave, Helper.ProjDmgScaling(1200, 1200, 1200), 0f, Main.myPlayer);
            }
            if (NPC.ai[0] == 860)
            {
                if (ModNPC.Music != YharonNPC.Phase4Music)ModNPC.Music= YharonNPC.Phase4Music;
            }
            if (NPC.ai[0] == 1020)
            {
                Main.NewText(
                    "我现在什么都不缺了！",
                    Color.Orange);
                NPC.setNPCName("至尊龙神·犽戎", NPC.type,true);
                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<LimitArea>(), 0, 0f, Target.whoAmI, NPC.whoAmI,1, 16 * 90);
            }
            if (NPC.ai[0] == 1100)
            {
                Main.NewText(
                    "【你周围的空气开始猛烈燃烧】",
                    Color.Purple);
                Main.NewText(
                    "【你的传送类道具已被焚毁】",
                    Color.Purple);
                Main.NewText(
                    "【始源林海套装中的森罗之力已被三昧神火破坏】",
                    Color.Purple);
                Main.NewText(
                    "【弑神者套装中的神明之力已被滔天的龙威压制】",
                    Color.Purple);
                Main.NewText(
                    "【正在发生日食】",
                    Color.Purple);
                YharonChangeSystem.YharonFixedPos = NPC.Center;
            }
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0]> 1340 && base.SwitchCondition(changeToSkill);
        public override bool ActivationCondition(NPCSkills activeSkill) => false;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawInsignia(spriteBatch, screenPos, NPC.ai[0]);
            return false;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false; 
    }
}
