using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.Projs.Bosses.Yharon;
using CalamityYharonChange.Content.Systems;
using CalamityYharonChange.Content.UIs;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class SteppingExplosion : BasicPhase4Skills
    {
        public int SteppingDir = 0;
        public SteppingExplosion(NPC nPC) : base(nPC) { }
        public override void AI()
        {
            if (NPC.ai[0] <= 300)
            {
                SkillTimeUI.Active = true;
                SkillTimeUI.SkillName = "百京核爆";
                SkillTimeUI.SkillTimeMax = 300;
                SkillTimeUI.SkillTime = (int)NPC.ai[0];
            }
            else SkillTimeUI.Active = false;
            NPC.velocity *= 0f;
            NPC.ai[0]++;
            if (NPC.ai[0] == 1)
            {
                SteppingDir = Main.rand.Next(0, 8);
            }
            float StartingDir = MathHelper.ToRadians(SteppingDir * 45f);
            float StartingDir2 = MathHelper.ToRadians(SteppingDir * 45f + 180f);
            //0,1380,2300
            if (NPC.ai[0] == 1)
            {
                Vector2 P11 = new Vector2(-460, -2880).RotatedBy(StartingDir)/2;
                Vector2 P12 = new Vector2(1380, -2880).RotatedBy(StartingDir) / 2;
                Vector2 V11 = new Vector2(0,1).RotatedBy(StartingDir);
                Vector2 V12 = new Vector2(0,1).RotatedBy(StartingDir);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P11, V11, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P12, V12, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
            }
            if (NPC.ai[0] == 180)
            {
                Vector2 P21 = new Vector2(460, -2880).RotatedBy(StartingDir) / 2;
                Vector2 P22 = new Vector2(2300, -2880).RotatedBy(StartingDir) / 2;
                Vector2 V2 = new Vector2(0, 1).RotatedBy(StartingDir);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P21, V2, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P22, V2, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
            }
            if (NPC.ai[0] == 360)
            {
                Vector2 P31 = new Vector2(-1380, -2880).RotatedBy(StartingDir) / 2;
                Vector2 P32 = new Vector2(-2300, -2880).RotatedBy(StartingDir) / 2;
                Vector2 V31 = new Vector2(0, 1).RotatedBy(StartingDir);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P31, V31, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P32, V31, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Vector2 P33 = new Vector2(-460, -2880).RotatedBy(StartingDir2);
                Vector2 P34 = new Vector2(1380, -2880).RotatedBy(StartingDir2);
                Vector2 V32 = new Vector2(0, 1).RotatedBy(StartingDir2);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P33, V32, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P34, V32, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
            }
            if (NPC.ai[0] == 420)
            {
                foreach (Projectile p in Main.projectile)
                {
                    if (p.active && p.timeLeft > 0 && p.type == ModContent.ProjectileType<LimitArea>())
                    {
                        NPC.Center = p.Center;
                    }
                }
            }
            if (NPC.ai[0] == 540)
            {
                Vector2 P41 = new Vector2(460, -2880).RotatedBy(StartingDir2) / 2;
                Vector2 P42 = new Vector2(2300, -2880).RotatedBy(StartingDir2) / 2;
                Vector2 V4 = new Vector2(0, 1).RotatedBy(StartingDir2);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P41, V4, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P42, V4, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
            }
            if (NPC.ai[0] == 720)
            {
                Vector2 P51 = new Vector2(-1380, -2880).RotatedBy(StartingDir2) / 2;
                Vector2 P52 = new Vector2(-2300, -2880).RotatedBy(StartingDir2) / 2;
                Vector2 V5 = new Vector2(0, 1).RotatedBy(StartingDir2);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P51, V5, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + P52, V5, ModContent.ProjectileType<SteppingBoomProj>(), 55555, 0f, Target.whoAmI, 0, 30, 1);
            }
            if (NPC.ai[0]>= 360)
            {
                float t = NPC.ai[0] - 360;
                NPC.Center = YharonChangeSystem.YharonFixedPos;
                if(t == 60)
                {
                    float r = Main.rand.NextFloat(0f, MathHelper.TwoPi);
                    for (float i = 0f; i < 360f; i += 120f)
                    {
                        Vector2 V = (MathHelper.ToRadians(i) + r).ToRotationVector2() * 4f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, V, ModContent.ProjectileType<FlareP4>(), Helper.ProjDmgScaling(1200,1200,1200), 0f, Target.whoAmI);
                    }
                }
                if(t >= 60 && (t-60)%80 == 0 && NPC.ai[0]<= 720)
                {
                    float r = Main.rand.NextFloat(0f, MathHelper.TwoPi);
                    for (float i = 0f; i < 360f; i += 40f)
                    {
                        Vector2 V = (MathHelper.ToRadians(i) + r).ToRotationVector2() * 12f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, V, YharonNPC.FlareBomb, Helper.ProjDmgScaling(600, 600, 600), 0f, Target.whoAmI,-1);
                    }
                }
            }
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0] > 1140 && base.SwitchCondition(changeToSkill);
        public override bool ActivationCondition(NPCSkills activeSkill)
        {
            bool a = true;
            foreach(Projectile p in Main.projectile)
            {
                if (p.active && p.hostile && p.type == ModContent.ProjectileType<SteppingBoomProj>() && p.timeLeft > 0)
                {
                    a = false;
                }
            }
            return a;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawStepping(spriteBatch, screenPos, NPC.ai[0]);
            return false;
        }
    }
}
