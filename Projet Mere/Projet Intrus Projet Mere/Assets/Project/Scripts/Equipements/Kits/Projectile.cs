using ProjectIntrus.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    [System.Serializable]
    public class ProjectileData : Data
    {
        [SerializeField] public int Power;
    }

    public class Projectile : ProjectIntrus.Tools.DynamicBehaviour, IDamager
    {
        [SerializeField] protected ProjectileData _data;
        [SerializeField] protected float _maxRange;
        [SerializeField] protected float _speed;
        float _remainingRange = 0;
        Vector3 _lastPos;

        public int Power
        {
            get { return _data.Power; }
        }
            

        public void Damage(IDamageable pDamageable)
        {
            if (pDamageable != null)
            {
                pDamageable.Damage(this);
            }
        }

        public Data GetData()
        {
            return _data;
        }

        // Start is called before the first frame update
        void Start()
        {
            _lastPos = this.transform.position;
            _remainingRange = _maxRange;
        }

        // Update is called once per frame
        void Update()
        {
            Move(0, 0, _speed, true);
            _remainingRange -= Vector3.Distance(_lastPos, Position);
            _lastPos = Position;
            if (_remainingRange <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Damage(other.GetComponent<IDamageable>());
            Destroy(this.gameObject);
        }
    }
}