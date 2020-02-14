using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Sniper : Weapons
    { 

        int iNumberZombieCanBePenetrate = 6;
        int numbberZombieCanBeStillPenetrated = 6;
        Transform tmpPositionShooting;

        bool shootIsShooted = false;


        int iMunitionToReload = 0;


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
            weaponShoot = this.GetComponent<AudioSource>();
            timerDuringJammed = 8;
            weaponType = WEAPON_TYPE.SNIPER;
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
            /////////////////////////////////////
            if (isReloading)
            {
                timer += Time.deltaTime;
                if (timer > fReloadingTime)
                {
                    iCurrentMunition += iMunitionToReload;
                    isReloading = false;
                }
            }

            if (isJammed)
            {
                timerJammed += Time.deltaTime;
                if (timerJammed > fReloadingTime)
                {
                    isJammed = false;
                }
            }

            if (isSaboted)
            {
                timerIsSaboted += Time.deltaTime;
                if (timerIsSaboted > timerDuringSabotage)
                {
                    isSaboted = false;
                    timerIsSaboted = 0;
                }
            }

            if (shootIsShooted)
            {
                DrawRaycastShoot();
            }


            if (!canShoot)
            {
                timerShooting += Time.deltaTime;
                if (timerShooting >= fCooldownBetweenTwoBullets)
                {
                    timerShooting = 0;
                    canShoot = true;
                }
            }
        }

        public override void Shoot()
        {
            if (iCurrentMunition > 0)
            {
                if (canShoot)
                {
                    weaponShoot.Play();
                    iCurrentMunition -= 1;
                    tmpPositionShooting = shootingPosition;
                    particleShooting.transform.position = tmpPositionShooting.position;
                    particleShooting.Play();
                    numbberZombieCanBeStillPenetrated = iNumberZombieCanBePenetrate;
                    shootIsShooted = true;
                    canShoot = false;
                }
            }
            else
            {
                loadEmptyShoot.Play();
            }
        }


        void DrawRaycastShoot()
        {           

            if (numbberZombieCanBeStillPenetrated > 0)
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(tmpPositionShooting.position, tmpPositionShooting.TransformDirection(Vector3.forward), out hit, maxShootingDistance, collideWith))
                {
                    

                    if (hit.collider.GetComponent<ITakeDamage>() != null)
                    {
                        hit.collider.GetComponent<ITakeDamage>().TakeDamage(iDmgPerBullet);
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

        }

    }
}