
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectIntrus.Equipements.Kits;
using UnityEngine;

namespace ProjectIntrus.Tools
{

    public interface IUser : IGenericDataHolder
    {
        void Use(IUsable pUsable);
    }

    public class UserBehaviour : MonoBehaviour, IUser
    {
        public bool _isUsing = false;

        virtual public Data GetData()
        {
            throw new System.NotImplementedException();
        }

        virtual public void Use(IUsable pUsable)
        {
            if (pUsable != null)
            {
                pUsable.Use(this);
            }
        }

    }
}