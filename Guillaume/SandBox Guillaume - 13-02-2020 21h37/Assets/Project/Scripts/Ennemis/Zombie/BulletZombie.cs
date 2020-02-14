using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Bullet
{
    public class BulletZombie : MonoBehaviour
    {

        public Vector3 directionBullet;
        Vector3 vecVelocity;
        float speedBullet;

        // Start is called before the first frame update
        void Start()
        {
            speedBullet = 4;
        }

        // Update is called once per frame
        void Update()
        {
            MovementBullet();
        }

        void MovementBullet()
        {
            Vector3 posBulletBuffer = this.transform.position;
            posBulletBuffer += directionBullet * speedBullet * Time.deltaTime;
            this.transform.position = posBulletBuffer;
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
