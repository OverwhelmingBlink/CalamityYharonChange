using CalamityMod;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.DraedonsArsenal;

namespace CalamityYharonChange.Content.Projs.Bosses.Yharon
{
    public class YharonRoarWave : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
    {
        public new string LocalizationCategory => "Projectiles.Misc";
        public override int Lifetime => 60;
        public override bool UsesScreenshake => true;
        public override float GetScreenshakePower(float pulseCompletionRatio) =>
            Projectile.ai[2] == 0 ?
            CalamityUtils.Convert01To010(pulseCompletionRatio) * 8f :
            (Projectile.ai[2] == 2 ?
            0 : CalamityUtils.Convert01To010(pulseCompletionRatio) * 50f); // 震屏力度
        public override Color GetCurrentExplosionColor(float pulseCompletionRatio) => Color.OrangeRed * 2;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = Lifetime;
        }
        public override void AI()
        {
            if (MaxRadius == 0)
                MaxRadius = 1300;
            if (Projectile.ai[2] == 1)
            {
                if (Projectile.timeLeft == 50)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalamityYharonChange/Assets/Sounds/YharonNPC/enlightment"), Projectile.Center);
                }
            }
            base.AI();
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.LocalPlayer.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing = true;
        }
        public override bool CanHitPlayer(Player target)
        {
            return target.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
         {
             target.GetModPlayer<CalamtiyYharonPlayer>().canBeHitByRing = false;
         }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(targetHitbox.TopLeft() + targetHitbox.Size()/2, projHitbox.TopLeft()+projHitbox.Size()/2) <= CurrentRadius;
        }
    }
}
