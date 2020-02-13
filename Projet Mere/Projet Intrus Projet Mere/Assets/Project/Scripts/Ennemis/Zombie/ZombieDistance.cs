using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectIntrus.Bullet;

namespace ProjectIntrus.Ennemis
{
    public class ZombieDistance : ZombieShooter
    {

        List<BulletZombie> listBulletZombie = new List<BulletZombie>();

        Vector3 directionShoot;

        float timeSinceLastShoot;
        float timeLastShoot;

        // Start is called before the first frame update
        void Start()
        {
            initSolo();

            speedAttack = 3;
        }

        // Update is called once per frame
        void Update()
        {
            RefreshDestination();

            ControlTime();
            if (_ciblePlayer)
            {
                _anim.SetBool("Attack_Shooter", true);
                Attack();
            }
        }

        public override void Attack()
        {

            if (timeSinceLastShoot >= randomTimeSpeedAttack)
            {
                float speedAttackWithPourcentRangeSpeedAttack = speedAttack * 0.15f;

                randomTimeSpeedAttack = Random.Range(speedAttack - speedAttackWithPourcentRangeSpeedAttack, speedAttack + speedAttackWithPourcentRangeSpeedAttack);
                timeLastShoot = Time.time;
                //Debug.Log("time speed attack : " + randomTimeSpeedAttack);
                directionShoot = this.transform.position - _ciblePlayer.transform.position;

                GameObject bulletEntity = Instantiate(prefabBulletZombie);
                bulletEntity.transform.position = transformSpawnBullet.position;
                if (bulletEntity != null)
                {
                    if (bulletEntity.GetComponent<BulletZombie>() == null)
                    {
                        //Debug.Log("Composent null");
                    }
                    bulletEntity.GetComponent<BulletZombie>().directionBullet = this.transform.forward;
                }
                listBulletZombie.Add(bulletEntity.GetComponent<BulletZombie>());
            }

        }

        void ControlTime()
        {
            timeSinceLastShoot = Time.time - timeLastShoot;
        }

    
    }

}
