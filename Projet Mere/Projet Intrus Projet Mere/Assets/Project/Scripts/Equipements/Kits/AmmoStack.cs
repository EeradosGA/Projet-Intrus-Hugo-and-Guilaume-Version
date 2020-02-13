using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class AmmoStack : ProjectIntrus.Tools.DynamicBehaviour
    {
        public int _containedAmmo;
        [SerializeField] protected float _speed;
        private bool _isMoving = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_isMoving)
            {
                Move(0, 0, _speed, true);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PrototypePlayer>() != null)
            {
                other.gameObject.GetComponent<PrototypePlayer>()._ammo += _containedAmmo;
                Destroy(this.gameObject);
            }
            else
            {
                _isMoving = false;
            }
        }
    }
}