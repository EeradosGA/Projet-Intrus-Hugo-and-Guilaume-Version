using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Tools
{
    public interface IDamageable
    {
        void Damage(IDamager pDamager);
    }
}