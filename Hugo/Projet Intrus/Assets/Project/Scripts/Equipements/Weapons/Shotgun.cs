using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Shotgun : Weapons
    { 
        int iLeadUsePerShoot = 25;

        [SerializeField]
        float spreadAngle = 0.3f;
        // Start is called before the first frame update
        void Start()
        {
            iDmgPerBullet = 1;
            iMagazineSize = 7;
            fCooldownBetweenTwoBullets = 2;
            fReloadingTime = 4;
            iMunitionUse = 5;
            iCurrentMunition = iMagazineSize;
            isAutomatic = false;
        }

        public override void Shoot()
        {
            if (iCurrentMunition > 0)
            {
                iCurrentMunition -= 1;

                for (int i = 0; i < iLeadUsePerShoot; i++)
                {
                    Vector3 direction = Vector3.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);

                    Debug.Log(direction);

                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(shootingPosition.position, direction, out hit, Mathf.Infinity, collideWith))
                    {
                        if (hit.collider.GetComponent<ITakeDmg>() != null)
                            hit.collider.GetComponent<ITakeDmg>().TakeDmg(iDmgPerBullet);

                        Debug.DrawRay(shootingPosition.position, direction * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                    else
                    {
                        Debug.DrawRay(shootingPosition.position, direction * 1000, Color.white);
                        Debug.Log("Did not Hit");
                    }
                }

            }
        }

    }
}
