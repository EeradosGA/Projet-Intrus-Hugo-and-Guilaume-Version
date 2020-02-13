using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Tools
{
    public class DynamicBehaviour : MonoBehaviour
    {
        public Vector3 Position
        {
            get { return this.transform.position; }
            set { this.transform.position = value; }
        }

        public Vector3 EulerAngles
        {
            get { return this.transform.eulerAngles; }
            set { this.transform.eulerAngles = value; }
        }

        public Vector3 Scale
        {
            get { return this.transform.lossyScale; }
        }

        public Quaternion Rotation
        {
            get { return this.transform.rotation; }
            set { this.transform.rotation = value; }
        }

        public Vector3 LocalPosition
        {
            get { return this.transform.localPosition; }
            set { this.transform.localPosition = value; }
        }

        public Vector3 LocalEulerAngles
        {
            get { return this.transform.localEulerAngles; }
            set { this.transform.localEulerAngles = value; }
        }

        public Vector3 LocalScale
        {
            get { return this.transform.localScale; }
            set { this.transform.localScale = value; }
        }

        public Quaternion LocalRotation
        {
            get { return this.transform.localRotation; }
            set { this.transform.localRotation = value; }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(Vector3 pOffset, bool pDirectionIsRelative = false, bool pLookAtDirection = false)
        {
            if (pDirectionIsRelative)
            {
                Vector3 newOffset = Vector3.zero;
                newOffset += pOffset.x * this.transform.right;
                newOffset += pOffset.y * this.transform.up;
                newOffset += pOffset.z * this.transform.forward;

                if (pLookAtDirection)
                {
                    Vector3 nextPos = Position;
                    nextPos += newOffset;
                    this.transform.LookAt(nextPos);
                }

                Position += newOffset * Time.deltaTime;
            }
            else
            {
                if (pLookAtDirection)
                {
                    Vector3 nextPos = Position;
                    nextPos += pOffset;
                    this.transform.LookAt(nextPos);
                }
                Position += pOffset * Time.deltaTime;

            }
        }

        public void Move(Vector3 pOffset, Transform pRelativeObject, bool pLookAtDirection = false)
        {
            Vector3 newOffset = Vector3.zero;
            newOffset += pOffset.x * pRelativeObject.right;
            newOffset += pOffset.y * pRelativeObject.up;
            newOffset += pOffset.z * pRelativeObject.forward;

            if (pLookAtDirection)
            {
                Vector3 nextPos = Position;
                nextPos += newOffset;
                this.transform.LookAt(nextPos);
            }

            Position += newOffset * Time.deltaTime;
        }

        public void Move(float pOffsetX, float pOffsetY, float pOffsetZ, bool pDirectionIsRelative = false, bool pLookAtDirection = false)
        {
            Move(new Vector3(pOffsetX, pOffsetY, pOffsetZ), pDirectionIsRelative, pLookAtDirection);
        }

        public void Move(float pOffsetX, float pOffsetY, float pOffsetZ, Transform pRelativeObject, bool pLookAtDirection = false)
        {
            Move(new Vector3(pOffsetX, pOffsetY, pOffsetZ), pRelativeObject, pLookAtDirection);
        }
    }
}