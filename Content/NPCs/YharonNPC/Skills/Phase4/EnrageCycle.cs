using CalamityMod.NPCs.Yharon;
using CalamityYharonChange.Buffs;
using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.Projs.Bosses.Yharon;
using CalamityYharonChange.Content.UIs;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class EnrageCycle : BasicPhase4Skills
    {
        public EnrageCycle(NPC nPC) : base(nPC) { }
        public int BreathLimit;
        public override void AI()
        {
            if (NPC.ai[0] <= 720)
            {
                SkillTimeUI.Active = true;
                SkillTimeUI.SkillName = "无尽轮回";
                SkillTimeUI.SkillTimeMax = 720;
                SkillTimeUI.SkillTime = (int)NPC.ai[0];
            }
            else SkillTimeUI.Active = false;
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
            if (NPC.ai[0] == 60)
            {
                BreathLimit = 0;
                Main.NewText(
                    "我会守护住他所创建的一切伟业！一如既往！",
                    Color.Orange);
            }
            if (NPC.ai[0] == 720)
            {
                SoundEngine.PlaySound(Yharon.RoarSound with { Volume = 2f }, NPC.Center);
            }
            if (NPC.ai[0] > 720)
            {
                Target.AddBuff(ModContent.BuffType<DragonPressure>(), 10);
                float t = NPC.ai[0] - 720;
                if (t >= 60 && t % 30 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), Target.Center, Vector2.Zero, ModContent.ProjectileType<DeathFlameColumn>(), 50, 0f, Target.whoAmI, BreathLimit);
                    BreathLimit++;
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool ActivationCondition(NPCSkills activeSkill) => false;
        public override bool CompulsionSwitchSkill(NPCSkills activeSkill) => YharonNPC.enrageClock > 10000;
        public override bool SwitchCondition(NPCSkills changeToSkill) => YharonNPC.enrageClock > 10000;
        public override void OnSkillDeactivate(NPCSkills changeToSkill)
        {
            base.OnSkillDeactivate(changeToSkill);
        }
        public override void OnSkillActive(NPCSkills activeSkill)
        {
            base.OnSkillActive(activeSkill);
            NPC.dontTakeDamage = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawNormal(spriteBatch, screenPos, Color.Yellow);
            return false;
        }
    }
}
