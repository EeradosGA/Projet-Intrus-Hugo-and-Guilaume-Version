using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class Turret : ProjectIntrus.Tools.DynamicBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected float _angularSpeed;
        private bool _isMoving = true;
        [SerializeField] protected Weapons.Weapons _weapon;
        private Transform _directionLocator;
        private Vector3 _startRotation;
        [SerializeField] private float _aimAngle;
        private bool _hasLocatedEnemy = false;
        [SerializeField] private int _ammo = 1000;

        public Weapons.Weapons Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
            //_startRotation = this.transform.eulerAngles;
            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;
            _directionLocator.gameObject.name = "DirectionLocator";
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMoving)
            {
                Move(0, 0, _speed, true);
            }
            else
            {
                if (_hasLocatedEnemy)
                {
                    if (Mathf.Abs(_startRotation.y - _directionLocator.transform.eulerAngles.y) < _aimAngle / 2)
                    {
                        Vector3 vec = _directionLocator.transform.eulerAngles;
                        if (vec.y < EulerAngles.y)
                        {
                            this.transform.Rotate(0, -_angularSpeed * Time.deltaTime, 0);
                        }
                        else if (vec.y > EulerAngles.y)
                        {
                            this.transform.Rotate(0, _angularSpeed * Time.deltaTime, 0);
                        }
                        if (Vector3.Angle(vec, EulerAngles) < _angularSpeed)
                        {
                            EulerAngles = vec;
                        }
                    }
                    else if (Mathf.Abs(_startRotation.y + 360 - _directionLocator.transform.eulerAngles.y) < _aimAngle / 2)
                    {
                        Vector3 vec = _directionLocator.transform.eulerAngles;
                        if (vec.y < EulerAngles.y)
                        {
                            this.transform.Rotate(0, -_angularSpeed * Time.deltaTime, 0);
                        }
                        else if (vec.y > EulerAngles.y)
                        {
                            this.transform.Rotate(0, _angularSpeed * Time.deltaTime, 0);
                        }
                        if (Vector3.Angle(vec, EulerAngles) < _angularSpeed)
                        {
                            EulerAngles = vec;
                        }
                    }
                    if (_weapon != null)
                    {
                        if (_weapon.CurrentMunition == 0 & !_weapon.IsReloading)
                        {
                            int tmp = _weapon.MagazineSize - _weapon.CurrentMunition;
                            tmp *= _weapon.MunitionUse;
                            if (_ammo < tmp)
                            {
                                _weapon.Reloading(_ammo);
                                _ammo = 0;
                            }
                            else
                            {
                                _weapon.Reloading(tmp);
                                _ammo -= tmp;
                            }
                        }
                        _weapon.Shoot();
                    }
                }

            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PrototypePlayer>() == null)
            {
                _isMoving = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<PrototypePlayer>() != null)
            {
                _directionLocator.LookAt(other.transform);
                Vector3 vec = _directionLocator.transform.eulerAngles;
                vec.x = vec.z = 0;
                _directionLocator.transform.eulerAngles = vec;
                if (Mathf.Abs(_startRotation.y - _directionLocator.transform.eulerAngles.y) < _aimAngle / 2)
                {
                    _hasLocatedEnemy = true;
                }
            }
        }
    }
}