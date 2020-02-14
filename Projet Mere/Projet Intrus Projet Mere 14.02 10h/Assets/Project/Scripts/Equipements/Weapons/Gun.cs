namespace ProjectIntrus.Equipements.Weapons
{
    public class Gun : Weapons
    {
        // Start is called before the first frame update
        void Start()
        {
            iDmgPerBullet = 2;
            iMagazineSize = 12;
            fCooldownBetweenTwoBullets = 0.5f;
            fReloadingTime = 1;
            iMunitionUse = 1;
            iCurrentMunition = iMagazineSize;
            isAutomatic = false;
            timerDuringJammed = 4;
            weaponType = WEAPON_TYPE.GUN;
        }

    }
}
