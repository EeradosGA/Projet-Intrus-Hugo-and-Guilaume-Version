using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ProjectIntrus.Ennemis
{

    public class ZombieCharacter : MonoBehaviour, ITakeDamage
    {

        protected NavMeshAgent agent;
        protected Rigidbody rigidbody;
        protected float life;
        [SerializeField] protected float speed = 5f;
        [SerializeField] protected float stoppingDistance = 3f;
        protected float speedAttack;
        protected float damage;
        protected bool isAttackPlayer;
        protected Animator _anim;

        Vector3 posDestination;

        private GameObject ciblePlayer;

        public GameObject _ciblePlayer
        {
            get { return ciblePlayer; }
            set { ciblePlayer = value; }
        }

        [SerializeField] private float fieldOfView = 15f;            
        public float _fieldOfView
        {
            get { return fieldOfView; }
        }

        public virtual void Attack()
        {

        }

        public virtual void Move()
        {
        }

        public void RefreshDestination()
        { 
            if ((CheckDestinationDefault.Instance._pos - this.transform.position).magnitude > CheckDestinationDefault.Instance._maxDistance + fieldOfView / 2)
            {
                posDestination = CheckDestinationDefault.Instance._pos;
                agent.SetDestination(CheckDestinationDefault.Instance._pos);
            }
            else if(ciblePlayer == null)
            {
                for (int i = 0; i < GameManager.Instance.getListPlayer().Count; i++)
                {
                    if ((GameManager.Instance.getListPlayer()[i].transform.position - this.transform.position).magnitude < fieldOfView)
                    {
                        RaycastHit hit;

                        if(Physics.Raycast(this.transform.position, (GameManager.Instance.getListPlayer()[i].transform.position - this.transform.position), out hit))
                        {
                            if (hit.collider.gameObject.tag == "Player")
                            {
                                //Debug.Log("hit collider : " + hit.collider + " Player [" + i + "]");
                                Debug.DrawRay(this.transform.position, (GameManager.Instance.getListPlayer()[i].transform.position - this.transform.position), Color.red);
                                ciblePlayer = GameManager.Instance.getListPlayer()[i];                               
                            }
                        }
                    }
                }
            } 
            else
            {
                agent.SetDestination(ciblePlayer.transform.position);
            }
        }

        public void TakeDamage(float HowMany)
        {

        }

        public virtual void initSolo()
        {
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
            rigidbody = GetComponent<Rigidbody>();
            _anim = this.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
        }
    }

}
