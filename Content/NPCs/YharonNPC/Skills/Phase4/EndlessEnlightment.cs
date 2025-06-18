using CalamityYharonChange.Content.Projs;
using CalamityYharonChange.Content.UIs;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class EndlessEnlightment : BasicPhase4Skills
    {
        public EndlessEnlightment(NPC npc) : base(npc) { }
        Vector2 pos;
        public override void AI()
        {
            if (NPC.ai[0] <= 300)
            {
                SkillTimeUI.Active = true;
                SkillTimeUI.SkillName = "无尽顿悟";
                SkillTimeUI.SkillTimeMax = 300;
                SkillTimeUI.SkillTime = (int)NPC.ai[0];
            }
            else SkillTimeUI.Active = false;
            NPC.ai[0]++;
            NPC.velocity *= 0;
            NPC.rotation = 0;
            if (NPC.ai[0] == 1)
            {
                if (Target.active && !Target.dead) pos = Target.Center;
                else pos = NPC.Center;
            }
            if (NPC.ai[0] == 300)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, Vector2.Zero, YharonNPC.YharonRoarWave, Helper.ProjDmgScaling(1200,1200,1200), 5f, Target.whoAmI, 0, 0, 1);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawNormal(spriteBatch,screenPos,Color.Yellow);
            return false;
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0] > 600 && base.SwitchCondition(changeToSkill);
        public override void OnSkillDeactivate(NPCSkills changeToSkill)
        {
            YharonNPC.BreathCount++;
            base.OnSkillDeactivate(changeToSkill);
        }
        public override bool ActivationCondition(NPCSkills activeSkill) => true;
    }
}
