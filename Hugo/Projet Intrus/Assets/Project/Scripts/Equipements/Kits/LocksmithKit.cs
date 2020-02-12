using ProjectIntrus.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class LocksmithKit : Kit
    {
        GameObject unlockedDoor;
        public override void Use(UserBehaviour pUser)
        {
            if (_isInCooldown)
            {
                Debug.Log("Is In Cooldown");
            }
            else
            {
                if (pUser)
                {
                    pUser._isUsing = true;
                    _inUse = true;
                }
                Debug.Log("Use");
                _isInCooldown = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (_inUse)
            {
                //Take door
            }
            else if(unlockedDoor != null)
            {
                //UnlockDoor
                //Remove door
            }
            else
            {
                //Remove door
            }
                
        }
    }
}