using ProjectIntrus.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Environement
{

    public class TriggerDoors : MonoBehaviour, IInteractable
    {
        public bool isActivated { get; set; }

        private Doors door;

        public void Action()
        {
            door.Action();
        }

        // Start is called before the first frame update
        void Start()
        {
            door = this.GetComponentInParent<Doors>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}