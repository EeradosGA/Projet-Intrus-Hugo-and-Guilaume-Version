using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Interface
{
    public interface IInteractable
    {
        bool isActivated { get; set; }

        bool canUse { get; set; }
        void Action();

        void ChangeUse(bool pCanUse);
    }
}