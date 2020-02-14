using ProjectIntrus.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Environement
{

    public class TriggerDoors : MonoBehaviour, IInteractable
    {
        public bool isActivated { get; set; }
        public bool canUse { get; set; }


        private Doors door;

        public void Action()
        {
            if (canUse)
                door.Action();
        }

        // Start is called before the first frame update
        void Start()
        {
            door = this.GetComponentInParent<Doors>();

            if (door._typeDoor == Doors.TypeDoor.ExitDoor)
                canUse = false;
            else
                canUse = true;


        }

        // Update is called once per frame
        void Update()
        {

        }

        void IInteractable.ChangeUse(bool pCanUse)
        {
            canUse = pCanUse;
        }
    }

}