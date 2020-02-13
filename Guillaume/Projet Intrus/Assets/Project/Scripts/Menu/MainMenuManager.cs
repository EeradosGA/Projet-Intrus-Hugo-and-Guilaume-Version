using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

namespace MainMenu
{

    public class MainMenuManager : MonoBehaviour
    {
        public struct joystickPlayer
        {
            public bool playerIndexSet;
            public PlayerIndex playerIndex;
            public GamePadState state;
            public GamePadState prevState;
        }
        private joystickPlayer[] _joystickPlayerTab = new joystickPlayer[4];

        [Header("IMAGES")]
        [SerializeField] private UnityEngine.UI.Image _imageMainMenu;
        [SerializeField] private UnityEngine.UI.Image _imageChoicePlayer;

        [Header("BOUTONS")]
        [SerializeField] private Button _ButtonPlayer1;
        [SerializeField] private Button _ButtonPlayer2;
        [SerializeField] private Button _ButtonPlayer3;
        [SerializeField] private Button _ButtonPlayer4;

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


            //ButtonPlay
            _ButtonPlay.gameObject.SetActive(false);
        }

        private void Update()
        {
            //Manage Joystick input
            for (int i = 0; i < _joystickPlayerTab.Length; ++i)
            {
                if (!_joystickPlayerTab[i].playerIndexSet || !_joystickPlayerTab[i].prevState.IsConnected)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;
                    GamePadState testState = GamePad.GetState(testPlayerIndex);
                    if (testState.IsConnected)
                    {
                        Debug.Log(this.name + " Start --> player " + i + " IsConnected");
                        _joystickPlayerTab[i].playerIndex = testPlayerIndex;
                        _joystickPlayerTab[i].state = GamePad.GetState(_joystickPlayerTab[i].playerIndex);
                    }
                }

                _joystickPlayerTab[i].prevState = _joystickPlayerTab[i].state;
                _joystickPlayerTab[i].state = GamePad.GetState(_joystickPlayerTab[i].playerIndex);
            }

            if (_mainMenu_State == MainMenu_State.CHOICE_PLAYER)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    this.TransitionToMainMenu();

                if(_MenuPlayer1.activeSelf
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
            GameManager.Instance.LoadScene(GameManager.SceneName.GAME);
        }

        public void CallApplicationQuit()
        {
            Application.Quit();
        }

        //pour l'instant des fonctions pour chaque bouton
        public void Player1_ActivateMenuPlayer()
        {
            _MenuPlayer1.SetActive(true);
        }

        public void Player2_ActivateMenuPlayer()
        {
            _MenuPlayer2.SetActive(true);
        }

        public void Player3_ActivateMenuPlayer()
        {
            _MenuPlayer3.SetActive(true);
        }

        public void Player4_ActivateMenuPlayer()
        {
            _MenuPlayer4.SetActive(true);
        }
    }
}

