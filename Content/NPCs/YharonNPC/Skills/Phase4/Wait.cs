using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class Wait:BasicPhase4Skills
    {
        public Wait (NPC nPC) : base (nPC) { }
        public override void AI()
        {
            NPC.ai[0]++;
            NPC.velocity *= 0;
            NPC.rotation = (Target.Center - NPC.Center).ToRotation();
        }

        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0] > 90 && base.SwitchCondition(changeToSkill);
        public override bool ActivationCondition(NPCSkills activeSkill) => true;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            _ = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, Color.Yellow * 1.66f, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, (Target.Center.X < NPC.Center.X) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
            return false;
        }
    }
}
