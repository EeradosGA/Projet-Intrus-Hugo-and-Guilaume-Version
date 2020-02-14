using ProjectIntrus.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    [System.Serializable]
    public class MedicKit : Kit
    {
        [SerializeField] List<GameObject> projectilesPrefabs;
        [SerializeField] int _healAmount = 100;

        [SerializeField] int _refillAmount;
        [SerializeField] float _refillTime;
        [SerializeField] float _currentRefillTime;

        private void Update()
        {
            _currentRefillTime += Time.deltaTime;
            if (_currentRefillTime >= _refillTime)
            {
                _healAmount += _refillAmount;
                _currentRefillTime -= _refillTime;
                if (_healAmount > 100)
                {
                    _healAmount = 100;
                }
            }
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
                    if (_healAmount >= -projectilesPrefabs[_doseId].GetComponent<Projectile>().Power)
                    {
                        _healAmount += projectilesPrefabs[_doseId].GetComponent<Projectile>().Power;
                        Projectile tempProjectile = Instantiate(projectilesPrefabs[_doseId]).GetComponent<Projectile>();
                        tempProjectile.transform.position = this.transform.position;
                        tempProjectile.transform.eulerAngles = this.transform.eulerAngles;
                        _isInCooldown = true;
                        pUser._isUsing = true;
                        _inUse = true;
                    }
                }
                Debug.Log("UseMedic");
            }
        }
    }
}