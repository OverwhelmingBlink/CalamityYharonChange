using CalamityMod.Buffs.DamageOverTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class FlareRingP4 : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_0";
        public float maxRadius
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
            }
        public float maxTime
        {
            get => Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public float radius = 0;
        public float alpha = 1;
        public static bool shake = false;
        public override bool ShouldUpdatePosition() => false;
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 30;
            Projectile.hostile = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
           // Main.LocalPlayer.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing = true;
        }
        public override bool CanHitPlayer(Player target)
        {
            //return target.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing;
            return false;
        }
       /* public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing = false;
        }*/
        /*public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(targetHitbox.TopLeft() + targetHitbox.Size()/2, projHitbox.TopLeft()+projHitbox.Size()/2) <= radius;
        }*/
        public override void AI()
        {
            radius += maxRadius / maxTime;
            if (Projectile.timeLeft <= .25f * maxTime)
            {
                alpha -= 1 / (maxTime * .25f);
            }
            if(Vector2.Distance(Main.LocalPlayer.Center,Projectile.Center)<= radius && maxTime > 20)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 0; i < 360; i += 10)
            {
                Vector2 pos = Projectile.Center - Main.screenPosition + MathHelper.ToRadians(i).ToRotationVector2() * radius;
                Color c = Color.OrangeRed * alpha;
                Texture2D tex = AssetPreservation.Extra[8].Value;
                Main.spriteBatch.Draw(tex, pos, null, c, 0f, tex.Size() / 2, .3f * (.5f+radius/maxRadius), SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
