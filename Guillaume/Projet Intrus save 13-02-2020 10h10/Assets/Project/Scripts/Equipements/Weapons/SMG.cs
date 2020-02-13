using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class SMG : Weapons
    {

        // Start is called before the first frame update
        void Start()
        {
            iDmgPerBullet = 2;
            iMagazineSize = 20;
            fCooldownBetweenTwoBullets = 0.2f;
            fReloadingTime = 1;
            iMunitionUse = 1;
            iCurrentMunition = iMagazineSize;
            isAutomatic = true;
            weaponShoot = this.GetComponent<AudioSource>();
        }
    }
}
