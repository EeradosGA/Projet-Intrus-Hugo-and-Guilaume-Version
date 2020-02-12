using ProjectIntrus.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class PrototypePlayer : UserBehaviour, IDamageable
    {
        public int _maxLife;
        public int _life;
        public float _healEfficiency = 1;
        [SerializeField] Kit kit;
        public bool _isDead;
        public int _ammo;

        // Start is called before the first frame update
        void Start()
        {
            _life = _maxLife;
            if (KitManager.Instance.Kits == null)
            {
                Debug.Log("MediKit is null");
            }
            kit = Instantiate(KitManager.Instance.Kits[0]).GetComponent<Kit>();
            kit.transform.SetParent(this.transform);
            kit.transform.localPosition = kit.transform.localEulerAngles = Vector3.zero;
            kit.transform.localPosition += this.transform.forward;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) & !_isUsing)
            {
                if (kit != null)
                {
                    kit.Use(this);
                }
                else
                {
                    Debug.Log("Kit is null");
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                kit.ChangeDose(0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                kit.ChangeDose(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                kit.ChangeDose(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                kit.ChangeDose(3);
            }

            if (kit != null)
            {
                kit.UpdateCooldown(_isDead);
                kit.UpdateUseStun(this);
            }
        }

        public void Damage(IDamager pDamager)
        {
            if (pDamager != null)
            {
                int power = Convert.ToInt32(pDamager.GetData().GetInfo().GetValue("Power"));
                if (power < 0)
                {
                    _life -= (int)(power * _healEfficiency);
                    _healEfficiency += (float)power / (float)_maxLife / 10f;
                    if (_healEfficiency < 0.5f)
                    {
                        _healEfficiency = 0.5f;
                    }
                }
                else
                {
                    _life -= power;
                }
                if (_life <= 0)
                {
                    _isDead = true;
                }
                if (_life > _maxLife)
                {
                    _life = _maxLife;
                }
            }
        }
    }
}