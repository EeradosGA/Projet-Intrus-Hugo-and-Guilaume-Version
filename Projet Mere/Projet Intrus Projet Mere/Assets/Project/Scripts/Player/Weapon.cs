using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetIntrus.Player
{
    public class Weapon : MonoBehaviour
    {
        public float damages;
        public int Amos;
        public float AttackSpeed;
        public bool _CurrentlyUsed = false;
        public bool _IsAutomatic = false;

        public WeaponType _type;

        private void Start()
        {
            
        }

        private void setStats(bool _IsAuto, float _AttSpeed, string _WeaponName)
        {
            _IsAutomatic = _IsAuto;
            AttackSpeed = _AttSpeed;
            name = _WeaponName;
        }

        public Weapon(int _Type)
        {
            if (_Type == (int)WeaponType.HANDPISTOL)
            {
                setStats(true, 0.5f, "HANDPISTOL");
            }
            else if (_Type == (int)WeaponType.SHOTGUN)
            {
                setStats(false, 1.0f, "SHOTGUN");
            }
            else if (_Type == (int)WeaponType.ASSAULTRIFLE)
            {
                setStats(true, 0.2f, "UZI");
            }
            else if (_Type == (int)WeaponType.SNIPER)
            {
                setStats(false, 2.0f, "SNIPER");
            }
        }

        public Weapon(int _Type, bool _IsUsed)
        {
            _CurrentlyUsed = _IsUsed;

            if (_Type == (int)WeaponType.HANDPISTOL)
            {
                setStats(true, 0.5f, "HANDPISTOL");
            }
            else if (_Type == (int)WeaponType.SHOTGUN)
            {
                setStats(false, 1.0f, "SHOTGUN");
            }
            else if (_Type == (int)WeaponType.ASSAULTRIFLE)
            {
                setStats(true, 0.2f, "UZI");
            }
            else if (_Type == (int)WeaponType.SNIPER)
            {
                setStats(false, 2.0f, "SNIPER");
            }
        }

        public enum WeaponType
        {
            HANDPISTOL = 0,
            SHOTGUN,
            ASSAULTRIFLE,
            SNIPER
        }


        public void Shoot()
        {

        }

        public void Reload()
        {

        }
    }
}