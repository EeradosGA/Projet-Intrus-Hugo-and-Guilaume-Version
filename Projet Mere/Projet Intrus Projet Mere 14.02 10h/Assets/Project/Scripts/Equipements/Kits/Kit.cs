using ProjectIntrus.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    [System.Serializable]
    public class Kit : MonoBehaviour, IUsable
    {
        [SerializeField] protected float _cooldownTime;
        [SerializeField] protected float _postMortemCooldownTime;

        protected float _currentCooldown = 0;
        protected float _currentUsingTime = 0;
        [SerializeField] protected float _usingTime = 0;
        protected bool _isInCooldown = false;
        protected bool _inUse = false;
        protected int _doseId;

        public void ChangeDose(int pDose)
        {
            _doseId = pDose;
        }

        virtual public void Use(UserBehaviour pUser)
        {
            if (_isInCooldown)
            {
                Debug.Log("Is In Cooldown");
            }
            else
            {
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

        public bool UpdateCooldown(bool pUserIsDead)
        {
            if (_isInCooldown)
            {
                _currentCooldown += Time.deltaTime;
                if (pUserIsDead)
                {
                    if (_currentCooldown >= _postMortemCooldownTime)
                    {
                        _currentCooldown = 0;
                        _isInCooldown = false;
                    }
                }
                else
                {
                    if (_currentCooldown >= _cooldownTime)
                    {
                        _currentCooldown = 0;
                        _isInCooldown = false;
                    }
                }

            }

            return _isInCooldown;
        }

        public void UpdateUseStun(UserBehaviour pUser)
        {
            if (pUser._isUsing)
            {
                _currentUsingTime += Time.deltaTime;
                if (_currentUsingTime >= _usingTime)
                {
                    _currentUsingTime = 0;
                    pUser._isUsing = false;
                    _inUse = false;
                }
            }
        }
    }
}