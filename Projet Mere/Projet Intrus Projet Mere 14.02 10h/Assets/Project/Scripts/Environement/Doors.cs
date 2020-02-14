using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Environement
{
    public class Doors : MonoBehaviour
    {
        public enum TypeDoor
        {
            InternDoor,
            ExitDoor,
        }

        [SerializeField] bool needKit = false;

        private bool _isTrapped = false;
        public bool isTrapped { get { return _isTrapped; } }

        private bool _isLocked = true;
        public bool isLocked { get { return _isLocked; } }

        private bool _isOpen = false;
        public bool isOpen { get { return _isOpen; } }

        private bool _openningDoor = false;
        public bool openningDoor { get { return _openningDoor; } }

        [SerializeField] private bool isLeft = false;

        /// Variables use to open door
        private float _currentTime = 0;
        private float _timeToOpen = 5;
        private Quaternion _doorDestination;
        private Quaternion _doorStart;

        [SerializeField] private TypeDoor typeDoor = TypeDoor.InternDoor;
        public TypeDoor _typeDoor { get { return typeDoor; } }

        // Start is called before the first frame update
        void Start()
        {
            //Set door destination with size of the door
            if (isLeft)
                _doorDestination = new Quaternion(this.transform.localRotation.x, -90, this.transform.localRotation.z, this.transform.localRotation.w);
            else
                _doorDestination = new Quaternion(this.transform.localRotation.x, 90, this.transform.localRotation.z, this.transform.localRotation.w);



            _doorStart = this.transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            OpenDoor();
        }

        /// <summary>
        /// Function call by the trigger
        /// </summary>
        public void Action()
        {
            if (UnlockDoor())
            {
                return;
            }
            else
                _openningDoor = true;
        }

        /// <summary>
        /// Unlock the door
        /// </summary>
        /// <returns></returns>
        private bool UnlockDoor(bool haveKit = false)
        {
            if (_isLocked)
            {
                if (needKit && haveKit)
                {
                    _isLocked = false;
                    return true;
                }
                else if (!needKit)
                {
                    if (_isLocked)
                    {
                        _isLocked = false;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Open the door if door is unlock and the play use action of interactable object
        /// </summary>
        /// <returns></returns>
        private bool OpenDoor()
        {
            if (_openningDoor)
            {
                if (!_isOpen)
                {
                    _currentTime += Time.deltaTime / _timeToOpen;
                    if (_currentTime < 1)
                    {
                        //Debug.Log("OpenDoor");
                        this.transform.localRotation = Quaternion.Lerp(_doorStart, _doorDestination, _currentTime);
                        return false;
                    }
                    else
                    {
                        _isOpen = true;
                        return true;
                    }
                }
                else
                    return true;
            }
            else return false;
        }

    }
}