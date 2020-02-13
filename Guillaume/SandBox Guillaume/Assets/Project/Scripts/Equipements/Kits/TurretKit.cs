using System.Collections;
using System.Collections.Generic;
using ProjectIntrus.Tools;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class TurretKit : Kit
    {
        [SerializeField] private GameObject _turretPrefab;
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
                    Turret turret = Instantiate(_turretPrefab).GetComponent<Turret>();
                    if (pUser.GetComponent<PrototypePlayer>() != null)
                    {
                        turret.Weapon = pUser.GetComponent<PrototypePlayer>().Weapon;
                        pUser.GetComponent<PrototypePlayer>().Weapon = null;
                        turret.Weapon.transform.SetParent(turret.transform);
                        turret.Weapon.transform.localPosition = Vector3.zero;
                    }
                    turret.transform.position = this.transform.position;
                    turret.transform.eulerAngles = this.transform.eulerAngles;
                    pUser._isUsing = true;
                    _inUse = true;
                    _isInCooldown = true;
                }
                Debug.Log("Use");
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
    }
}