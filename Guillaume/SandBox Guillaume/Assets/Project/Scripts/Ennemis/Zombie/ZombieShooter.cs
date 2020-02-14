using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Ennemis
{

    public class ZombieShooter : ZombieCharacter
    {
        [SerializeField]
        protected GameObject prefabBulletZombie;
        protected float rangeShoot;
        [SerializeField]
        protected Transform transformSpawnBullet;

        protected float randomTimeSpeedAttack;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
