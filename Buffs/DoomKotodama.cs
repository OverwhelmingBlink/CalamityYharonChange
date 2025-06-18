using CalamityMod.CalPlayer;
using CalamityYharonChange.Content.NPCs.YharonNPC;
using CalamityYharonChange.Content.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Buffs
{
    public class DoomKotodama : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance = 0;
            if (YharonChangeSystem.YharonBoss != -1)
            {

                if (YharonNPC.Mode == 4)
                {
                    player.buffTime[buffIndex]++;
                }
            }
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            buffName = new string("末日言灵");
            tip = new string("你的防御正在土崩瓦解\n" +
                "清空伤害减免");
        }
    }
}
