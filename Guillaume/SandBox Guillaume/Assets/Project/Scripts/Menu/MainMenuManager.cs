using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{

    public class MainMenuManager : MonoBehaviour
    {


        [Header("IMAGES")]
        [SerializeField] private UnityEngine.UI.Image _imageMainMenu;
        [SerializeField] private UnityEngine.UI.Image _imageChoicePlayer;

        [Header("BOUTONS MENU PRINCIPAL")]
        [SerializeField] private Button _ButtonChoicePlayer;
        [SerializeField] private Button _ButtonQuit;

        [Header("BOUTONS CHOIX PLAYER")]
        [SerializeField] private Button _ButtonPlayer1;
        [SerializeField] private Button _ButtonPlayer2;
        [SerializeField] private Button _ButtonPlayer3;
        [SerializeField] private Button _ButtonPlayer4;

        [Header("DROP DOWN KIT")]
        [SerializeField] private Dropdown _DDKitPlayer1;
        [SerializeField] private Dropdown _DDKitPlayer2;
        [SerializeField] private Dropdown _DDKitPlayer3;
        [SerializeField] private Dropdown _DDKitPlayer4;

        [Header("BUTTONS WEAPONS")]
        [SerializeField] private Button _ButtonWeaponPlayer1;
        [SerializeField] private Button _ButtonWeaponPlayer2;
        [SerializeField] private Button _ButtonWeaponPlayer3;
        [SerializeField] private Button _ButtonWeaponPlayer4;

        [Header("MENU PLAYER")]
        [SerializeField] private GameObject _MenuPlayer1;
        [SerializeField] private GameObject _MenuPlayer2;
        [SerializeField] private GameObject _MenuPlayer3;
        [SerializeField] private GameObject _MenuPlayer4;

        [Header("SELECT WEAPON")]
        [SerializeField] private SelectWeapon _SelectWeapon_Player1;

        [Header("BOUTON JOUER")]
        [SerializeField] private Button _ButtonPlay;

        


        public enum MainMenu_State
        {
            MAIN_MENU,
            CHOICE_PLAYER
        }
        private MainMenu_State _mainMenu_State = MainMenu_State.MAIN_MENU;


        private void Start()
        {   
            ///VERIFICATIONS

            //Images
            if(_imageMainMenu == null)
            { Debug.LogError(this.name + " Start --> _imageMainMenu est null"); }
            if (_imageChoicePlayer == null)
            { Debug.LogError(this.name + " Start --> _imageChoicePlayer est null"); }

            //Menu Player
            if(_MenuPlayer1 == null)
            { Debug.LogError(this.name + " Start --> _MenuPlayer1 est null"); }
            if (_MenuPlayer2 == null)
            { Debug.LogError(this.name + " Start --> _MenuPlayer2 est null"); }
            if (_MenuPlayer3 == null)
            { Debug.LogError(this.name + " Start --> _MenuPlayer3 est null"); }
            if (_MenuPlayer4 == null)
            { Debug.LogError(this.name + " Start --> _MenuPlayer4 est null"); }

            //Select Weapon
            if(_SelectWeapon_Player1 == null)
            { Debug.LogError(this.name + " Start --> _SelectWeapon_Player1 est null"); }


            //Images
            _imageMainMenu.gameObject.SetActive(true);
            _imageChoicePlayer.gameObject.SetActive(false);

            //Menu Player
            _MenuPlayer1.SetActive(false);
            _MenuPlayer2.SetActive(false);
            _MenuPlayer3.SetActive(false);
            _MenuPlayer4.SetActive(false);

            //BUTTONS

            //ButtonChoicePlayer
            _ButtonChoicePlayer.Select();
            //ButtonPlay
            _ButtonPlay.gameObject.SetActive(false);

            //Navigation
            //_DDKitPlayer1.navigation.selectOnRight
        }

        private void Update()
        {
            

            if (_mainMenu_State == MainMenu_State.CHOICE_PLAYER)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    this.TransitionToMainMenu();

                //i faudrait adapter avce l'index du player et non le nombre
                if(GameManager.Instance.nbrJoysitckControllerConnect == 4)
                {
                    this.Player1_ActivateMenuPlayer();
                    this.Player2_ActivateMenuPlayer();
                    this.Player3_ActivateMenuPlayer();
                    this.Player4_ActivateMenuPlayer();
                }
                else if (GameManager.Instance.nbrJoysitckControllerConnect == 3)
                {
                    this.Player1_ActivateMenuPlayer();
                    this.Player2_ActivateMenuPlayer();
                    this.Player3_ActivateMenuPlayer();
                    this.Player4_DisableMenuPlayer();
                }
                else if (GameManager.Instance.nbrJoysitckControllerConnect == 2)
                {
                    this.Player1_ActivateMenuPlayer();
                    this.Player2_ActivateMenuPlayer();
                    this.Player3_DisableMenuPlayer();
                    this.Player4_DisableMenuPlayer();
                }
                else if (GameManager.Instance.nbrJoysitckControllerConnect == 1)
                {
                    this.Player1_ActivateMenuPlayer();
                    this.Player2_DisableMenuPlayer();
                    this.Player3_DisableMenuPlayer();
                    this.Player4_DisableMenuPlayer();
                }
                else if (GameManager.Instance.nbrJoysitckControllerConnect == 0)
                {
                    this.Player1_DisableMenuPlayer();
                    this.Player2_DisableMenuPlayer();
                    this.Player3_DisableMenuPlayer();
                    this.Player4_DisableMenuPlayer();
                }

                //if (GameManager.Instance.nbrJoysitckControllerConnect >= 1)
                //{
                //    _ButtonChoicePlayer
                //}

                if (_MenuPlayer1.activeSelf
                && _MenuPlayer2.activeSelf
                && _MenuPlayer3.activeSelf
                && _MenuPlayer4.activeSelf)
                {
                    _ButtonPlay.gameObject.SetActive(true);
                }
            }



            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    _SelectWeapon_Player1.ScrollUp();

            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    _SelectWeapon_Player1.ScrollDown();

            //}
        }

        public void TransitionToChoicePlayer()
        {
            _imageMainMenu.gameObject.SetActive(false);
            _imageChoicePlayer.gameObject.SetActive(true);

            _mainMenu_State = MainMenu_State.CHOICE_PLAYER;
        }

        public void TransitionToMainMenu()
        {
            _imageMainMenu.gameObject.SetActive(true);
            _imageChoicePlayer.gameObject.SetActive(false);

            _mainMenu_State = MainMenu_State.MAIN_MENU;
        }

        public void CallLoadSceneGame()
        {
            Debug.Log(this.name + " CallLoadSceneGame");
            GameManager.Instance.LoadScene(GameManager.SceneName.GAME);
        }

        public void CallApplicationQuit()
        {
            Application.Quit();
        }

        //pour l'instant des fonctions pour chaque bouton
        public void Player1_ActivateMenuPlayer()
        {
            if(_MenuPlayer1.activeSelf == false)
            {
                _MenuPlayer1.SetActive(true);
                //_DDKitPlayer1.Select();
                _ButtonWeaponPlayer1.Select();
            }

        }

        public void Player2_ActivateMenuPlayer()
        {
            if (_MenuPlayer2.activeSelf == false)
            {
                _MenuPlayer2.SetActive(true);
            }
        }

        public void Player3_ActivateMenuPlayer()
        {
            if (_MenuPlayer3.activeSelf == false)
            {
                _MenuPlayer3.SetActive(true);
            }
        }

        public void Player4_ActivateMenuPlayer()
        {
            if (_MenuPlayer4.activeSelf == false)
            {
                _MenuPlayer4.SetActive(true);
            }
        }

        public void Player1_DisableMenuPlayer()
        {
            if (_MenuPlayer1.activeSelf == true)
            {
                _MenuPlayer1.SetActive(false);
            }
        }

        public void Player2_DisableMenuPlayer()
        {
            if (_MenuPlayer2.activeSelf == true)
            {
                _MenuPlayer2.SetActive(false);
            }
        }

        public void Player3_DisableMenuPlayer()
        {
            if (_MenuPlayer3.activeSelf == true)
            {
                _MenuPlayer3.SetActive(false);
            }
        }

        public void Player4_DisableMenuPlayer()
        {
            if (_MenuPlayer4.activeSelf == true)
            {
                _MenuPlayer4.SetActive(false);
            }
        }
    }
}

