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
    public class DeathCycle : BasicPhase4Skills
    {
        public DeathCycle(NPC nPC) : base(nPC) { }
        public int BreathLimit;
        public override void AI()
        {
            if (NPC.ai[0] <= 300)
            {
                SkillTimeUI.Active = true;
                SkillTimeUI.SkillName = "死亡轮回";
                SkillTimeUI.SkillTimeMax = 300;
                SkillTimeUI.SkillTime = (int)NPC.ai[0];
            }
            else SkillTimeUI.Active = false;
            NPC.ai[0]++;
            NPC.velocity *= 0;
            NPC.rotation = 0;
            if (NPC.ai[0] == 1)
            {
                BreathLimit = YharonNPC.BreathCount;
                if (Vector2.Distance(NPC.Center,Target.Center) > 800)
                {
                    NPC.Center = Target.Center + Utils.SafeNormalize(NPC.Center - Target.Center, Vector2.Zero) * 800f;
                }
            }
            if (NPC.ai[0] > 300)
            {
                float t = NPC.ai[0] - 300;
                if(t == 1)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), Target.Center, Vector2.Zero, ModContent.ProjectileType<DeathFlameColumn>(), 50, 0f, Target.whoAmI);
                }
                if (t >= 90 && t < 60 + BreathLimit * 60 && t % 60 == 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), Target.Center, Vector2.Zero, ModContent.ProjectileType<DeathFlameColumn>(), 50, 0f, Target.whoAmI);
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => BreathLimit > 0 && NPC.ai[0] > (810 + 60 * BreathLimit) && base.SwitchCondition(changeToSkill);
        public override void OnSkillDeactivate(NPCSkills changeToSkill)
        {
            YharonNPC.BreathCount++;
            base.OnSkillDeactivate(changeToSkill);
        }
        public override bool ActivationCondition(NPCSkills activeSkill) => true;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawNormal(spriteBatch, screenPos, Color.Yellow);
            return false;
        }
    }
}
