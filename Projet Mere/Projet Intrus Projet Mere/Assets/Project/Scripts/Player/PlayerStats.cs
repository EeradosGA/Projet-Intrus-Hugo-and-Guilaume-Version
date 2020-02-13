using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetIntrus.Player
{
    public class PlayerStats : MonoBehaviour
    {

        public float HealthPoints = 100;
        public float Damages = 8;
        public float Speed = 10;
        public float _Strengh = 10;

        private GameObject[] CurrentWeapons;

        public Material GhostMaterial;
        public bool _IsDead = false;

        public bool _IsRadiated = false;
        public float RadiatedTimer;

        public bool _IsSlowed = false;
        public float SlowTimer;

        public bool _HasShooted = false;

        public GameObject WeaponStocker = null;

        public bool _PreparingToThrow = false;
        public float ThrowTimer = 0;

        private int WeponNbr = 0;

        // Start is called before the first frame update
        void Start()
        {
            CurrentWeapons = new GameObject[2];
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (WeaponStocker != null)
                {
                    PickUpWeapon(WeaponStocker);
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.position += transform.forward / 5;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position -= transform.forward / 5;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += transform.right / 5;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position -= transform.right / 5;
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                TakeNextWeapon();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("KeyPressed");
                _PreparingToThrow = true;
                ThrowTimer = Time.time;
            }
            else if(Input.GetMouseButtonUp(1))
            {
                _PreparingToThrow = false;
            }

            if(_PreparingToThrow == true && Time.time - ThrowTimer > 0.2f)
            {
                ThrowWeapon();
            }

        }



        public void Die()
        {
            this.GetComponent<MeshRenderer>().material = GhostMaterial;
            for (int a = CurrentWeapons.Length; a >= 0; a--)
            {
                Destroy(CurrentWeapons[a].gameObject);
            }
        }

        public void Shoot()
        {
            if (_IsDead == false)
            {
                for (int a = 0; a < CurrentWeapons.Length; a++)
                {
                    if (CurrentWeapons[a] != null && CurrentWeapons[a].GetComponent< Weapon >()._CurrentlyUsed == true)
                    {
                        CurrentWeapons[a].GetComponent<Weapon>().Shoot();
                    }
                }
            }
        }

        public void GetHitted(float _DamagesTaken)
        {
            if (_IsDead == false)
            {
                if (HealthPoints > _DamagesTaken)
                {
                    HealthPoints -= _DamagesTaken;
                }
                else
                {
                    Die();
                }
            }
        }

        public void PickUpWeapon(GameObject _newWeapon)
        {
            for(int a = 0; a < CurrentWeapons.Length; a++)
            {
                if(CurrentWeapons[a] == null)
                {
                    WeponNbr = a;
                    CurrentWeapons[a] = _newWeapon;
                    CurrentWeapons[a].GetComponent<Rigidbody>().isKinematic = true;
                    CurrentWeapons[a].GetComponent<Weapon>()._CurrentlyUsed = true;

                    WeaponStocker.transform.parent = this.transform;
                    WeaponStocker.transform.localPosition = new Vector3(0,1,1);

                    for (int c = 0; c < CurrentWeapons.Length; c++)
                    {
                        if(CurrentWeapons[c] != null && c != a)
                        {
                            CurrentWeapons[c].GetComponent<Weapon>()._CurrentlyUsed = false;
                            CurrentWeapons[c].SetActive(false);
                        }
                    }
                    WeaponStocker = null;
                    break;
                }
                if (a == CurrentWeapons.Length - 1)
                {
                    for (int b = 0; b < CurrentWeapons.Length; b++)
                    {
                        if (CurrentWeapons[b] != null && CurrentWeapons[b].GetComponent<Weapon>()._CurrentlyUsed == true)
                        {
                            WeponNbr = b;
                            ThrowWeapon();
                            CurrentWeapons[b] = _newWeapon;
                            CurrentWeapons[b].GetComponent<Rigidbody>().isKinematic = false;
                            CurrentWeapons[b].GetComponent<Weapon>()._CurrentlyUsed = true;


                            WeaponStocker = null;
                            break;
                        }
                    }
                }
            }
        }

        public void TakeNextWeapon()
        {
            if (_IsDead == false)
            {
                    if (WeponNbr == 0)
                    {
                        WeponNbr = 1;
                        if (CurrentWeapons[0] != null)
                        {
                            CurrentWeapons[0].GetComponent<Weapon>()._CurrentlyUsed = false;
                            CurrentWeapons[0].SetActive(false);
                        }

                        if (CurrentWeapons[1] != null)
                        {
                            CurrentWeapons[1].GetComponent<Weapon>()._CurrentlyUsed = true;
                            CurrentWeapons[1].SetActive(true);
                        }
                    }
                    else if (WeponNbr == 1)
                    {
                        WeponNbr = 0;
                        if (CurrentWeapons[1] != null)
                        {
                            CurrentWeapons[1].GetComponent<Weapon>()._CurrentlyUsed = false;
                            CurrentWeapons[1].SetActive(false);
                        }

                        if (CurrentWeapons[0] != null)
                        {
                            CurrentWeapons[0].GetComponent<Weapon>()._CurrentlyUsed = true;
                            CurrentWeapons[0].SetActive(true);
                        }
                    }
            }
        }

        public void ThrowWeapon()
        {
            if (_IsDead == false)
            {
                for (int a = 0; a < CurrentWeapons.Length; a++)
                {
                    if (CurrentWeapons[a] != null && CurrentWeapons[a].GetComponent<Weapon>()._CurrentlyUsed == true)
                    {
                        CurrentWeapons[a].GetComponent<Weapon>()._CurrentlyUsed = false;
                        CurrentWeapons[a].GetComponent<Rigidbody>().isKinematic = false;
                        CurrentWeapons[a].GetComponent<Rigidbody>().AddForce(this.transform.forward * _Strengh);
                        CurrentWeapons[a].GetComponent<Rigidbody>().AddForce(this.transform.up * _Strengh / 5);
                        CurrentWeapons[a].transform.parent = null;
                        CurrentWeapons[a].transform.Rotate(new Vector3(0, 90, 0));
                        CurrentWeapons[a] = null;
                    }
                }
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            Weapon CollidingWeapon;
            if (other.TryGetComponent<Weapon>(out CollidingWeapon) && other.gameObject != CurrentWeapons[WeponNbr])
            {
                WeaponStocker = other.gameObject;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject == WeaponStocker)
            {
                WeaponStocker = null;
            }
        }
    }
}