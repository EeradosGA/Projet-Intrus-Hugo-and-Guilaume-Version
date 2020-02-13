using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Weapons
{
    public class Shotgun : Weapons
    { 
        int iLeadUsePerShoot = 25;

        [SerializeField]
        GameObject particleShootingEffect;

        [SerializeField]
        List<ParticleSystem> listParticleShootingEffect;

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

            listParticleShootingEffect = new List<ParticleSystem>();

            for (int i = 0; i < iLeadUsePerShoot; i++)
            {
                listParticleShootingEffect.Add(Instantiate(particleShootingEffect).GetComponent<ParticleSystem>());
                listParticleShootingEffect[i].transform.SetParent(this.transform);
                listParticleShootingEffect[i].transform.position = shootingPosition.position;
            }

            timerDuringJammed = 8;

            weaponType = WEAPON_TYPE.SHOTGUN;

        }

        public override void Shoot()
        {
            if (iCurrentMunition > 0)
            {
                weaponShoot.Play();
                iCurrentMunition -= 1;

                for (int i = 0; i < iLeadUsePerShoot; i++)
                {                
                    Vector3 direction = Vector3.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);

                    Debug.Log(direction);

                    listParticleShootingEffect[i].transform.eulerAngles = direction*100;
                    listParticleShootingEffect[i].Play();

                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(shootingPosition.position, direction, out hit, maxShootingDistance, collideWith))
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
            else
            {
                loadEmptyShoot.Play();
            }
        }

    }
}
