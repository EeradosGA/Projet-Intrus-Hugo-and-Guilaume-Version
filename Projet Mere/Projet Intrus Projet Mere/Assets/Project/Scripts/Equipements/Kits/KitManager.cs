using ProjectIntrus.Tools;
using ProjectIntrus.Equipements.Kits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Equipements.Kits
{
    public class KitManager : MonoSingleton<KitManager>
    {
        [SerializeField] List<GameObject> _kits;

        public List<GameObject> Kits
        {
            get { return _kits; }
        }

        // Start is called before the first frame update
        void Start()
        {
            Instance._kits = _kits;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}