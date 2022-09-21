namespace SkillCardSystem.Weapon
{
    public class LaserWeapon:WeaponBase
    {
        public override void Shot()
        {
            base.Shot();
        }

        public void Set(float bulletSpeed)
        {
            this.m_bulletSpeed = bulletSpeed;
        }

        public void ReLoad()
        {
            m_nowBulletCount = m_bulletCapacity;
        }
    }
}