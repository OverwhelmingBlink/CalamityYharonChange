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
    public class SeventhCata : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 200;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            buffName = new string("第七灵灾");
            tip = new string("灭世的言灵施于你身");
        }
    }
}
