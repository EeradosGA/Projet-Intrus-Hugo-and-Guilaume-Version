using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Interface
{
    public interface IInteractable
    {
        bool isActivated { get; set; }
        void Action();
    }
}