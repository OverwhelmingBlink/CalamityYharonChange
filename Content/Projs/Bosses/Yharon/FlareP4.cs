using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class FlareP4 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.scale = 2;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 720;
            Projectile.penetrate = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.DarkOrange with { A = 0 }, 0f, tex.Size() / 2, 2f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White with { A = 0 }, 0f, tex.Size() / 2, 1.75f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
