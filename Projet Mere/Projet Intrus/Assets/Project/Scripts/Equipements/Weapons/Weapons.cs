using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Weapons : MonoBehaviour
    {
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


        float timer = 0;
        int iMunitionToReload = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

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

            if(isReloading)
            {
                timer += Time.deltaTime;
                if(timer > fReloadingTime)
                {
                    iCurrentMunition += iMunitionToReload;
                    isReloading = false;
                }
            }
        }

        public virtual void Shoot()
        {
            if (iCurrentMunition > 0)
            {
                iCurrentMunition -= 1;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(shootingPosition.position, shootingPosition.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, collideWith))
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

        public virtual void Reloading(int pHowManyMunition)
        {
            iMunitionToReload = pHowManyMunition / iMunitionUse;
            timer = 0;
            isReloading = true;
        }

    }
}
