using CalamityMod;
using CalamityYharonChange.Core.SkillsNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.NPCs.YharonNPC.Modes
{
    /// <summary>
    /// 一阶段的Mode
    /// </summary>
    public class YharonPhase4 : BaiscYharonPhase
    {
        public YharonPhase4(NPC npc) : base(npc)
        {
        }
        public override void OnEnterMode()
        {
            YharonNPC.NPC.Calamity().AITimer = 0;
        }
        public override bool ActivationCondition(NPCModes activeMode) => false;
    }
}
