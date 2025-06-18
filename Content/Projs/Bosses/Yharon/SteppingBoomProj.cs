using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class SteppingBoomProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.scale = 6;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
        }
        public override bool? CanDamage()
        {
            if (Projectile.ai[2] > 0) return false;
            else return Projectile.timeLeft == 350;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = 0;
        }
        public override void AI()
        {
            if (Projectile.ai[2] <= 0)
            {
                Main.projFrames[Type] = 7;
                if (Projectile.timeLeft < 270) Projectile.Kill();
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 4)
                {
                    Projectile.frameCounter = 0;
                    if (Projectile.frame < 6) Projectile.frame++;
                    else Projectile.frame = 0;
                    if (Projectile.frame == 4)
                    {
                        SoundEngine.PlaySound(new SoundStyle("CalamityYharonChange/Assets/Sounds/YharonNPC/step"), Projectile.Center);
                    }
                }
            }
            else
            {
                Main.projFrames[Type] = 12;
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 4)
                {
                    Projectile.frameCounter = 0;
                    if (Projectile.frame < 11) Projectile.frame++;
                    else Projectile.frame = 0;
                }
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            if (Projectile.ai[2] <= 0)
            {
                if (Projectile.ai[1] > 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + Projectile.Hitbox.Size()/2 + Projectile.velocity * 500f, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Main.LocalPlayer.whoAmI, Projectile.ai[0], Projectile.ai[1] - 1);
                }
            }
            else
            {
                if (Projectile.ai[1] > 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + Projectile.Hitbox.Size() / 2, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Main.LocalPlayer.whoAmI, Projectile.ai[0], Projectile.ai[1]);
                }
            }
            return base.PreKill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[2] <= 0)
            {
                if(Projectile.frame < 7 && Projectile.timeLeft > 331)
                {
                    Vector2 drawPos = Projectile.Center - Main.screenPosition;
                    Texture2D projTex = AssetPreservation.Extra[6].Value;
                    Color color = Color.Orange;
                    Rectangle rec = new Rectangle(0, Projectile.frame * 98, 98, 98);
                    Main.spriteBatch.Draw(projTex, drawPos, rec, color, 0f, rec.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0f);
                }
            }
            else
            {
                Vector2 drawPos = Projectile.Center - Main.screenPosition;
                Texture2D projTex = AssetPreservation.Extra[9].Value;
                Color color = Color.Orange;
                Rectangle rec = new Rectangle( Projectile.frame * 65,0, 65, 67);
                Main.spriteBatch.Draw(projTex, drawPos, rec, Color.White * .66f, Projectile.velocity.ToRotation() + MathHelper.PiOver2, rec.Size()/2, Projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}
