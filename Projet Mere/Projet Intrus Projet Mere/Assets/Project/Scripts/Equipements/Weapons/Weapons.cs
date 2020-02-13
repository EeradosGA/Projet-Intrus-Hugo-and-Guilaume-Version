using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Weapons : MonoBehaviour
    {
        public enum WEAPON_TYPE
        {
            GUN = 0,
            SMG,
            SHOTGUN,
            SNIPER
        }

        [SerializeField]
        protected WEAPON_TYPE weaponType;
        public WEAPON_TYPE WeaponType
        {
            get
            {
                return weaponType;
            }
        }

        [SerializeField]
        protected AudioSource weaponShoot;
        [SerializeField]
        protected AudioSource reloadingSound;
        [SerializeField]
        protected AudioSource loadEmptyShoot;

        [SerializeField]
        protected ParticleSystem particleMuzzle;


        [SerializeField]
        protected ParticleSystem particleShooting;

        [SerializeField]
        protected int iDmgPerBullet;

        public int DmgPerBullet
        {
            get
            {
                return iDmgPerBullet;
            }
        }

        [SerializeField]
        protected int iMagazineSize;

        public int MagazineSize
        {
            get
            {
                return iMagazineSize;
            }
        }

        [SerializeField]
        protected int iCurrentMunition;

        public int CurrentMunition
        {
            get
            {
                return iCurrentMunition;
            }
        }

        [SerializeField]
        //To use the same munition Quantity, a weapon consume munition link to his state
        protected int iMunitionUse;

        //To use the same munition Quantity, a weapon consume munition link to his state
        public int MunitionUse
        {
            get
            {
                return iMunitionUse;
            }
        }

        [SerializeField]
        protected float fCooldownBetweenTwoBullets;

        public float CooldownBetweenTwoBullets
        {
            get
            {
                return fCooldownBetweenTwoBullets;
            }
        }

        [SerializeField]
        protected float fReloadingTime;


        public float ReloadingTime
        {
            get
            {
                return fReloadingTime;
            }
        }



        [SerializeField]
        protected float maxShootingDistance;

        [SerializeField]
        protected LayerMask collideWith;

        [SerializeField]
        protected Transform shootingPosition;




        [SerializeField]
        protected bool isAutomatic;
        public bool IsAutomatic
        {
            get
            {
                return isAutomatic;
            }
        }

        [SerializeField]
        protected bool isReloading = false;
        public bool IsReloading
        {
            get
            {
                return isReloading;
            }
        }


        [SerializeField]
        //True is the weapon is Jammed
        protected bool isJammed = false;
        public bool IsJammed
        {
            get
            {
                return isJammed;
            }
        }

        [SerializeField]
        protected float pourcentageChanceJammed = 2;
        public float PourcentageChanceJammed
        {
            get
            {
                return pourcentageChanceJammed;
            }

            set
            {
                pourcentageChanceJammed = value;
            }
        }

        [SerializeField]
        //True if the player received munition from the intruder
        protected bool isSaboted = false;
        public bool IsSaboted
        {
            get
            {
                return isSaboted;
            }

            set
            {
                isSaboted = value;
            }
        }

        protected float timerJammed = 0;
        protected float timerIsSaboted = 0;


        [SerializeField]
        protected float timerDuringJammed = 0;

        [SerializeField]
        protected float timerDuringSabotage = 30;

        [SerializeField]
        protected Light light;

        protected float timer = 0;
        int iMunitionToReload = 0;

        // Update is called once per frame
        void Update()
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

            if(isSaboted)
            {
                timerIsSaboted += Time.deltaTime;
                if(timerIsSaboted > timerDuringSabotage)
                {
                    isSaboted = false;
                    timerIsSaboted = 0;
                }
            }
        }

        public virtual void Shoot()
        {
            if (iCurrentMunition > 0)
            {
                CheckJammed();
                if (!isJammed)
                {                
                    iCurrentMunition -= 1;
                    weaponShoot.Play();
                    particleMuzzle.Play();
                    particleShooting.Play();

                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward), out hit, maxShootingDistance, collideWith))
                    {
                        if (hit.collider.GetComponent<ITakeDmg>() != null)
                            hit.collider.GetComponent<ITakeDmg>().TakeDmg(iDmgPerBullet);

                        Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                    else
                    {
                        Debug.DrawRay(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward) * 1000, Color.white);
                        Debug.Log("Did not Hit");
                    }
                }
            }
            else
            {
                loadEmptyShoot.Play();
            }
        }

        public virtual void Reloading(int pHowManyMunition)
        {
            reloadingSound.Play();
            iMunitionToReload = pHowManyMunition / iMunitionUse;
            timer = 0;
            isReloading = true;
        }


        public virtual void CheckJammed()
        {
            if (!isJammed)
            {
                if (isSaboted)
                {
                    pourcentageChanceJammed = 5;
                }
                else
                {
                    pourcentageChanceJammed = 2;
                }

                int random = Random.Range(0, 100);

                if (random >= 0 && random <= pourcentageChanceJammed)
                {
                    isJammed = true;
                    loadEmptyShoot.Play();
                    timerJammed = 0;
                }
                else
                {
                    isJammed = false;
                }
            }
        }


        public virtual void UseLight()
        {
            if (light.enabled)
                light.enabled = false;
            else
                light.enabled = true;
        }
    }
}
