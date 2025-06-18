using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Skills.Phase4
{
    public class Blink: BasicPhase4Skills
    {
        public Blink (NPC nPC):base(nPC) { }
        SpriteEffects sE = SpriteEffects.None;
        public override void AI()
        {
            if (NPC.ai[0] < 30) NPC.velocity *= .66f;
            else NPC.velocity *= 0;
            NPC.ai[0]++;
            if (NPC.ai[0] > 10 && NPC.ai[0]< 50)NPC.dontTakeDamage = true;
            else NPC.dontTakeDamage = false;
            if (NPC.ai[0]== 30)
            {
                Vector2 P1 = NPC.Center;
                Vector2 P2 = Target.Center;
                Vector2 P3 = P2 + (P2 - P1).SafeNormalize(Vector2.Zero) * 700f;
                NPC.Center = P3;
            }
            if (NPC.ai[0]> 60)
            {
                NPC.rotation = (Target.Center - NPC.Center).ToRotation();
                sE = (Target.Center.X < NPC.Center.X) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            }
        }
        public override bool SwitchCondition(NPCSkills changeToSkill) => NPC.ai[0] > 120 && base.SwitchCondition(changeToSkill);
        public override bool ActivationCondition(NPCSkills activeSkill) => true;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            float a;
            if (NPC.ai[0] <= 30)
            {
                a = (30 - NPC.ai[0]) / 30f;
            }
            else if (NPC.ai[0] <= 60)
            {
                a = (NPC.ai[0] - 30) / 30f;
            }
            else a = 1;
            _ = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, Color.Yellow * 1.66f * a, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, sE, 0f);
            return false;
        }
    }
}
