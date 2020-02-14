using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace ProjetIntrus.Player
{
    public class PlayerController : MonoBehaviour
    {
        bool playerIndexSet = false;
        [SerializeField] PlayerIndex playerIndex;
        GamePadState state;
        GamePadState prevState;
        float fHoldButtonThrow = 0f;
        bool weaponThrowed = false;

        PlayerStats playerStats = null;
        void Start()
        {
            playerStats = this.gameObject.GetComponent<PlayerStats>();
        }
        // Update is called once per frame
        void Update()
        {
            //assigne l'index du joueur a la manette correspondante s'il n'est pas assigné
            if (!playerIndexSet || !prevState.IsConnected)
            {
                AssignControllerToPlayer();
            }

            //update les states de la manette
            prevState = state;
            state = GamePad.GetState(playerIndex);

            //bouge la position du joueur en fonction du joystick gauche
            Movement();
            //bouge l'orientation du joueur en fonction du joytsick droit
            Orientation();
            //Interagit avec un objet avec le bouton A
            Interaction();
            //Recharge l'arme que le joueur a dans ses mains avec le bouton X
            Reload();
            //Tire avec l'arme que le joueur a dans ses mains avec la gachette RT
            Shoot();
            //Utilise le kit du joueur avec la gachette LT
            UseKit();
            //Utilise l'attaque de corps a corps avec le bouton RB ou R3
            MeleeAttack();
            //Change d'arme que le joueur possède avec le bouton LB ou Y
            ChangeWeapon();
            //Jete l'arme que le joueur a dans ses mains en maintenant le bouton LB ou Y ou avec le bouton SELECT
            ThrowWeapon();
            //Dose la competence de son kit en utilisant les fleches du pad de la manette
            DoseCompetence();
            //Active ou desactive la lampe tactique
            UseLight();

            //test vibration
            if (Input.GetKey(KeyCode.V))
                Vibration();
            else
                GamePad.SetVibration(playerIndex, 0f, 0f);
        }

        void AssignControllerToPlayer()
        {
            GamePadState testState = GamePad.GetState(playerIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", playerIndex));
                playerIndexSet = true;
            }
        }
        void Vibration()
        {
            GamePad.SetVibration(playerIndex, 0.25f, 0.25f);
        }

        void Movement()
        {
            //sert a faire bouger la position du joueur en fonction du joystick gauche
            Vector3 pos = this.transform.position;
            pos.x += prevState.ThumbSticks.Left.X / 100;
            pos.z += prevState.ThumbSticks.Left.Y / 100;
            this.transform.position = pos;
        }

        void Orientation()
        {
            //sert a faire bouger l'orientation du joueur en fonction du joytsick droit
            if (prevState.ThumbSticks.Right.X != 0 || prevState.ThumbSticks.Right.Y != 0)
            {
                Vector3 lookDirection = new Vector3(prevState.ThumbSticks.Right.X, 0, prevState.ThumbSticks.Right.Y);
                Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                float step = 10 * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
            }
        }
        void Interaction()
        {
            // Detecte si le Bouton A a été préssé
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Interact");
                if(playerStats.WeaponStocker != null)
                {
                    playerStats.PickUpWeapon(playerStats.WeaponStocker);
                }
            }
        }

        void Reload()
        {
            // Detecte si le Bouton X a été préssé
            if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Reload");
                //faire le reload
            }
        }
        void Shoot()
        {
            // Detecte si la gachette RT est préssée
            if (state.Triggers.Right >= 0.15f)
            {
                Debug.Log("Player" + playerIndex + " Shoot");
                //faire le shoot
            }
        }
        void UseKit()
        {
            // Detecte si la gachette LT est préssée
            if (prevState.Triggers.Left <= 0.15f && state.Triggers.Left >= 0.15f)
            {
                Debug.Log("Player" + playerIndex + " Use Kit");
                //faire l'utilisation de kit
            }
        }

        void MeleeAttack()
        {
            // Detecte si le Bouton RB ou R3 a été préssé
            if ((prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
                || (prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Pressed))
            {
                Debug.Log("Player" + playerIndex + " Melee Attack");
                //faire le l'attaque au corps a corps
            }
        }

        void ChangeWeapon()
        {
            // Detecte si le Bouton LB ou Y a été préssé
            if (((prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released)
                || (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released))
                && weaponThrowed == false)
            {
                Debug.Log("Player" + playerIndex + " Change Weapon");
                playerStats.TakeNextWeapon();
            }
        }
        void ThrowWeapon()
        {
            // Detecte si le Bouton LB ou Y est maintenu
            if ((state.Buttons.LeftShoulder == ButtonState.Pressed
                || state.Buttons.Y == ButtonState.Pressed)
                && fHoldButtonThrow <= 0.5f
                && weaponThrowed == false)
            {
                fHoldButtonThrow += Time.deltaTime;
            }
            // Detecte si le Bouton LB ou Y a été laché
            else if (state.Buttons.LeftShoulder == ButtonState.Released
                && state.Buttons.Y == ButtonState.Released
                && state.Buttons.Back == ButtonState.Released)
            {
                fHoldButtonThrow = 0f;
                weaponThrowed = false;
            }
            // Detecte si le Bouton SELECT a été préssé ou si le bouton LB ou Y a été maintenu assez longtemps
            if (((prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)
                || fHoldButtonThrow >= 0.5f)
                && weaponThrowed == false)
            {
                Debug.Log("Player" + playerIndex + " Throw Weapon");
                weaponThrowed = true;
                playerStats.ThrowWeapon();
            }
        }

        void DoseCompetence()
        {
            // Detecte si la fleche du haut a été préssée
            if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Up competence selected");
                //faire le dosage du haut
            }
            // Detecte si la fleche de droite a été préssée
            if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Right competence selected");
                //faire le dosage de droite
            }
            // Detecte si la fleche du bas a été préssée
            if (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Down competence selected");
                //faire le dosage du bas
            }
            // Detecte si la fleche de gauche a été préssée
            if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " Left competence selected");
                //faire l edosage de gauche
            }
        }

        void UseLight()
        {
            // Detecte si le Bouton X a été préssé
            if (prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed)
            {
                Debug.Log("Player" + playerIndex + " use light");
                //faire le use light
            }
        }
    }
}
