using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Sniper : Weapons
    { 
        int iNumberZombieCanBePenetrate = 6;
        int numbberZombieCanBeStillPenetrated = 6;
        Transform tmpPositionShooting;

        bool shootIsShooted = false;
        // Start is called before the first frame update
        void Start()
        {
            iDmgPerBullet = 16;
            iMagazineSize = 5;
            fCooldownBetweenTwoBullets = 4;
            fReloadingTime = 4;
            iMunitionUse = 5;
            iCurrentMunition = iMagazineSize;
            isAutomatic = false;
        }

        private void Update()
        {

            //debug
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                int tmp = iMagazineSize - iCurrentMunition;
                tmp *= iMunitionUse;
                Reloading(tmp);
            }


            ///////////////////////////////
            
            if(shootIsShooted)
            {
                DrawRaycastShoot();
            }
        }

        public override void Shoot()
        {
            
            if (iCurrentMunition > 0)
            {
                iCurrentMunition -= 1;
                tmpPositionShooting = shootingPosition;
                numbberZombieCanBeStillPenetrated = iNumberZombieCanBePenetrate;
                shootIsShooted = true;               
            }
        }


        void DrawRaycastShoot()
        {           

            if (numbberZombieCanBeStillPenetrated > 0)
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(tmpPositionShooting.position, tmpPositionShooting.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collideWith))
                {
                    if (hit.collider.GetComponent<ITakeDmg>() != null)
                    {
                        hit.collider.GetComponent<ITakeDmg>().TakeDmg(iDmgPerBullet);
                        numbberZombieCanBeStillPenetrated--;
                        tmpPositionShooting = hit.transform;
                    }
                    else
                    {
                        shootIsShooted = false;
                        numbberZombieCanBeStillPenetrated = 0;
                    }

                    Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }
                else
                {
                    Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                    shootIsShooted = false;
                    numbberZombieCanBeStillPenetrated = 0;
                }
            }
            else
            {
                shootIsShooted = false;
            }

            if(numbberZombieCanBeStillPenetrated == 0)
            {
                shootIsShooted = false;
            }
        }

    }
}