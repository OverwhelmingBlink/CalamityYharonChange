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
    public class DragonPressure : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.gravity = 0f;
            player.controlDown = false;
            player.controlUp = false;
            player.controlLeft = false;
            player.controlRight = false;
            player.controlJump = false;
            player.gravControl = false;
            player.velocity *= 0;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            buffName = new string("龙神威压");
            tip = new string("龙神的君权压制得你动弹不得\n" +
                "效果：无法移动");
        }
    }
}
