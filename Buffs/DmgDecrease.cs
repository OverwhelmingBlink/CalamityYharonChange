using CalamityYharonChange.Content.NPCs.YharonNPC;
using CalamityYharonChange.Content.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace CalamityYharonChange.Buffs
{
    public class DmgDecrease:ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            CalamtiyYharonPlayer CP = player.GetModPlayer<CalamtiyYharonPlayer>();
            if (CP != null)
            {
                if (CP.DmgDec <= 0) CP.DmgDec = 1;
            }
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            CalamtiyYharonPlayer CP = player.GetModPlayer<CalamtiyYharonPlayer>();
            if (CP != null) 
            {
                if (CP.DmgDec < 10) CP.DmgDec++;
            }
            time = 3600;
            return base.ReApply(player, time, buffIndex);
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            buffName = new string("伤害降低");
            CalamtiyYharonPlayer CP = Main.LocalPlayer.GetModPlayer<CalamtiyYharonPlayer>();
            if (CP != null)
            {
                string f = (CP.DmgDec * 10f).ToString("F1");
                tip = new string("你的伤害降低了\n" +
                    "最终伤害降低" + f + "%");
            }
        }
    }
}
