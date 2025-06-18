using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Boss;
using CalamityYharonChange.Content.NPCs.Dusts;
using CalamityYharonChange.Content.NPCs.YharonNPC.Skills.General;
using CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase1;
using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.Projs.Bosses.Yharon;
using CalamityYharonChange.Content.UIs;
using CalamityYharonChange.Core.NPCAI;
using CalamityYharonChange.Core.SkillsNPC;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class FinalAtk : BasicPhase4Skills
    {
        public FinalAtk(NPC nPC) : base(nPC) { }
        public int SteppingDir = 0;
        public override void AI()
        {
            NPC.dontTakeDamage = true;
            SkillTimeUI.Active = false;
            NPC.ai[0]++;
            NPC.velocity *= 0;
            NPC.rotation = 0;
            foreach (Projectile p in Main.projectile)
            {
                if (p.active && p.timeLeft > 0 && p.type == ModContent.ProjectileType<LimitArea>())
                {
                    NPC.Center = p.Center;
                }
            }
            if (NPC.ai[0] == 1)
            {
                foreach (Projectile p in Main.projectile)
                {
                    if (p.active && p.timeLeft > 0 && p.type != ModContent.ProjectileType<LimitArea>() && p.hostile)
                    {
                        p.Kill();
                    }
                }
            }
            if (NPC.ai[0] > 180)
            {
                float t = NPC.ai[0] - 180;
                if (t > 60 && t % 30 == 0)
                {
                    float r = Main.rand.NextFloat(0f, MathHelper.TwoPi);
                    for (float i = 0f; i < 360f; i += 40f)
                    {
                        Vector2 V = (MathHelper.ToRadians(i) + r).ToRotationVector2() * 14f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, V, YharonNPC.FlareBomb, 300, 0f, Target.whoAmI, -1);
                    }
                }
                if (t == 1)
                {
                    SteppingDir = Main.rand.Next(0, 8);
                }
                float StartingDir = MathHelper.ToRadians(SteppingDir * 45f);
                float StartingDir2 = MathHelper.ToRadians(SteppingDir * 45f + 180f);
                if (t == 30)
                {
                    new ShootRing2().ApplyExtraAI(YharonNPC.extraAIs, NPC);
                    Vector2 P11 = new Vector2(-460, -2880).RotatedBy(StartingDir) / 2;
                    Vector2 P12 = new Vector2(1380, -2880).RotatedBy(StartingDir) / 2;
                    Vector2 V11 = new Vector2(0, 1).RotatedBy(StartingDir);
                    Vector2 V12 = new Vector2(0, 1).RotatedBy(StartingDir);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P11, V11, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P12, V12, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                }
                if (t == 180)
                {
                    Vector2 P21 = new Vector2(460, -2880).RotatedBy(StartingDir) / 2;
                    Vector2 P22 = new Vector2(2300, -2880).RotatedBy(StartingDir) / 2;
                    Vector2 V2 = new Vector2(0, 1).RotatedBy(StartingDir);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P21, V2, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P22, V2, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                }
                if (t >= 720)
                {
                    NPC.life = 0;
                    NPC.checkDead();
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool ActivationCondition(NPCSkills activeSkill) => false;
        public override void OnSkillActive(NPCSkills activeSkill)
        {
            base.OnSkillActive(activeSkill);
            NPC.dontTakeDamage = true;
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0] > 900 && base.SwitchCondition(changeToSkill);
        public override bool CompulsionSwitchSkill(NPCSkills activeSkill) => NPC.life <= 10;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawNormal(spriteBatch, screenPos, Color.Yellow);
            return false;
        }
    }
    public class ShootRing2 : ExtraAI
    {
        private int timer;
        public int counts = 4;
        private int distance;
        public override void AI()
        {
            timer++;
            FireBurstRing.width = 400;
            if (timer > 120)
            {
                if ((timer - 120) % 120 == 0)
                {
                    Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, Vector2.Zero, ModContent.ProjectileType<FireBurstRing>(), npc.GetProjectileDamage(YharonNPC.YharonNormalBoomProj), 0f, Main.myPlayer, 0, distance);
                    distance += FireBurstRing.width;
                }
                if ((timer-120) >= 120 * counts)
                {
                    Remove();
                }
            }
        }
    }
}
