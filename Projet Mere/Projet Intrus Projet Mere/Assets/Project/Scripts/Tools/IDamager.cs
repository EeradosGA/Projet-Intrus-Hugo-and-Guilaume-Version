using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Tools
{
    public interface IDamager : IGenericDataHolder
    {
        void Damage(IDamageable pDamageable);
    }
}