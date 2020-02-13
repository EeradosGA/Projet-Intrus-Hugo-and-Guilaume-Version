using ProjectIntrus.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class AmmoKit : Kit
    {
        [SerializeField] List<GameObject> _stackPrefabs;
        int _ammo;

        public void UpdateAmmo(int pAmmo)
        {
            _ammo = pAmmo;
        }

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

                    _ammo = this.GetComponentInParent<PrototypePlayer>()._ammo;
                    if (_stackPrefabs[_doseId].GetComponent<AmmoStack>()._containedAmmo < _ammo)
                    {
                        _ammo -= _stackPrefabs[_doseId].GetComponent<AmmoStack>()._containedAmmo;
                        this.GetComponentInParent<PrototypePlayer>()._ammo = _ammo;

                        AmmoStack tempStack = Instantiate(_stackPrefabs[_doseId]).GetComponent<AmmoStack>();
                        tempStack.transform.position = this.transform.position;
                        tempStack.transform.eulerAngles = this.transform.eulerAngles;
                        Debug.Log("Use");
                        _isInCooldown = true;
                        pUser._isUsing = true;
                        _inUse = true;
                    }
                }
                
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