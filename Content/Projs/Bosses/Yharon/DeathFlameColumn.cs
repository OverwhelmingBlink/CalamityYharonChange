using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class DeathFlameColumn : ModProjectile
    {
        public override string Texture => "CalamityYharonChange/Assets/Images/Extra/Extra_7";
        public override bool ShouldUpdatePosition() => false;
        public override void SetDefaults()
        {
            Projectile.width = 220;
            Projectile.height = 220;
            Projectile.scale = 2;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 30;
            Projectile.penetrate = -1;
        }
        public float a=0;
        public float b=0;
        public float c=0;

        public override bool? CanDamage() => Projectile.timeLeft == 29;
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.statLife -= (int)(80 + Projectile.ai[0] * 80);
        }
        public override void AI()
        {
            a = Math.Min(2 * (float)Math.Sin((double)Projectile.timeLeft * Math.PI / 30f), 1.66f);
            if(Projectile.timeLeft < 24)
            {
                b = Math.Min(2 * (float)Math.Sin((double)Projectile.timeLeft * Math.PI / 24f), 1.66f);
            }
            if (Projectile.timeLeft < 18)
            {
                c = Math.Min(2.5f * ((9f - Math.Abs(9 - Projectile.timeLeft)) / 9f), 1.5f);
            }
            if (Projectile.timeLeft == 18)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DeathFlameAura>(), 1, 0f, Projectile.owner);
            }
            if(Projectile.timeLeft == 24)
            {
                SoundEngine.PlaySound(new SoundStyle("CalamityYharonChange/Assets/Sounds/YharonNPC/cycle"), Projectile.Center);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex1 = AssetPreservation.Extra[2].Value;
            Texture2D tex2 = AssetPreservation.Extra[7].Value;
            Vector2 ori1 = tex1.Size() / 2;
            Vector2 ori2 = new Vector2(tex2.Width / 2, tex2.Height);
            Vector2 pos = Projectile.Center - Main.screenPosition;
            float h = c * 2.33f;
            Main.spriteBatch.Draw(tex1, pos, null, (Color.DarkRed with { A = 0 }) * a, 0f, ori1, 2 *a, SpriteEffects.None, 0f);
            if (Projectile.timeLeft < 24)
            {
                Main.spriteBatch.Draw(tex2, pos, null, (Color.DarkRed with { A = 0 }) * b, 0f, ori2, 2.2f * new Vector2(1f*b, h), SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(tex1, pos, null, (Color.OrangeRed with { A = 0 }) * a, 0f, ori1, a * 1.5f, SpriteEffects.None, 0f);

            if (Projectile.timeLeft < 24)
            {
                Main.spriteBatch.Draw(tex2, pos, null, (Color.OrangeRed with { A = 0 }) * b, 0f, ori2, 2.2f * new Vector2(.9f * b, h), SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(tex1, pos, null, (Color.White with { A = 0 }) * a, 0f, ori1, a , SpriteEffects.None, 0f);

            if (Projectile.timeLeft < 24)
            {
                Main.spriteBatch.Draw(tex2, pos, null, (Color.White with { A = 0 }) * b, 0f, ori2, 2.2f * new Vector2(.8f, h), SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
