using CalamityYharonChange.Buffs;
using CalamityYharonChange.Content.NPCs.YharonNPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class DeathFlameAura : ModProjectile
    {
        public override string Texture => "CalamityYharonChange/Assets/Images/Extra/Extra_7";
        public override bool ShouldUpdatePosition() => false;
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 5;
            Projectile.penetrate = -1;
        }
        public float a = 0;
        public float b = 0;
        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            b++;
            foreach (NPC y in Main.npc)
            {
                if (y.active && y.life > 0 && y.type == ModContent.NPCType<YharonNPC>())
                {
                    Projectile.timeLeft++;
                }
            }
            if (a < 1) a += .01f;
            if (a >= 1)
            {
                foreach (Player t in Main.player)
                {
                    if (t.active && !t.dead)
                    {
                        if (Vector2.Distance(t.Center, Projectile.Center) <= 320f)
                        {
                            t.AddBuff(ModContent.BuffType<SeventhCata>(), 300);
                        }
                    }
                }
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = AssetPreservation.Extra[8].Value;
            float rot = MathHelper.ToRadians(b*2);
            float s = 320f / tex.Width;
            Vector2 o = tex.Size() / 2;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, (Color.OrangeRed ) * a, rot, o, s, SpriteEffects.None, 0f);
            return false;
        }
    }
}
