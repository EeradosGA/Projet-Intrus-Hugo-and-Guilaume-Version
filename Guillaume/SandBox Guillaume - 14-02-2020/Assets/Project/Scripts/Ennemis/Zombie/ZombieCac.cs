using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Ennemis
{

    public class ZombieCac : ZombieCharacter
    {
        // Start is called before the first frame update
        void Start()
        {
            initSolo();
        }

        // Update is called once per frame
        void Update()
        {
            RefreshDestination();

            if (isAttackPlayer == false)
            {
                if (_ciblePlayer != null)
                {
                    if ((this.transform.position - _ciblePlayer.transform.position).magnitude < 1.95f)
                    {
                        _anim.SetBool("Attack_Cac", true);
                        isAttackPlayer = true;
                    }
                }
            }
            else
            { 
                Attack();
                _anim.SetBool("Attack_Cac", false);
                isAttackPlayer = false;
            }

            if(_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Cac") == true)
            {
                if (AnimatorIsPlaying("Attack_Cac") == false)
                {
                    _anim.SetBool("Attack_Cac", false);
                }
               /*else
                {
                    if (_ciblePlayer != null)
                    {
                        if ((this.transform.position - _ciblePlayer.transform.position).magnitude > 4f)
                        {

                        }
                    }
                }*/
            }
        }

        bool AnimatorIsPlaying(string stateName) // Animation Playing
        {
            return /*AnimatorIsPlaying() &&*/ _anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        bool AnimatorIsPlaying() // Animator Playing 
        {
            return _anim.GetCurrentAnimatorStateInfo(0).length >
                   _anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public override void Attack()
        {
            Debug.Log("Cac is Attack");            
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.tag == "Player") isAttackPlayer = true;
        }
    }
}