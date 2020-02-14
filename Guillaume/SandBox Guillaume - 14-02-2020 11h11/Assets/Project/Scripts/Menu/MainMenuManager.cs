using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProjectIntrus.Tools;

namespace MainMenu
{

    public class MainMenuManager : MonoSingleton<MainMenuManager>
    {
        public enum MainMenu_State
        {
            MAIN_MENU,
            CHOICE_PLAYER
        }
        private MainMenu_State _mainMenu_State = MainMenu_State.MAIN_MENU;
        public MainMenu_State mainMenu_State { get { return _mainMenu_State; } }

        public enum ChoiceStyle
        {
            ONE_BY_ONE,
            BY_JOYSTICK_CONTROLLERS
        }
        [Header("CHOICE STYLE")]
        [SerializeField] private ChoiceStyle _choiceStyle = ChoiceStyle.ONE_BY_ONE;
        public ChoiceStyle choiceStyle  { get { return _choiceStyle; } }

        private GameObject _currentChoicePlayerSelectable;

        [Header("IMAGES")]
        [SerializeField] private UnityEngine.UI.Image _imageMainMenu;
        [SerializeField] private UnityEngine.UI.Image _imageChoicePlayer;

        [Header("BOUTONS MENU PRINCIPAL")]
        [SerializeField] private Button _ButtonChoicePlayer;
        [SerializeField] private Button _ButtonQuit;

        [Header("BOUTONS WEAPONS")]
        [SerializeField] private Button _ButtonWeaponPlayer1;
        [SerializeField] private Button _ButtonWeaponPlayer2;
        [SerializeField] private Button _ButtonWeaponPlayer3;
        [SerializeField] private Button _ButtonWeaponPlayer4;

        [Header("BOUTONS KIT")]
        [SerializeField] private Button _ButtonKitPlayer1;
        [SerializeField] private Button _ButtonKitPlayer2;
        [SerializeField] private Button _ButtonKitPlayer3;
        [SerializeField] private Button _ButtonKitPlayer4;

        [Header("BOUTONS VALIDATION")]
        [SerializeField] private Button _ButtonValidationPlayer1;
        [SerializeField] private Button _ButtonValidationPlayer2;
        [SerializeField] private Button _ButtonValidationPlayer3;
        [SerializeField] private Button _ButtonValidationPlayer4;

        [Header("MENU PLAYER")]
        [SerializeField] private GameObject _MenuPlayer1;
        [SerializeField] private GameObject _MenuPlayer2;
        [SerializeField] private GameObject _MenuPlayer3;
        [SerializeField] private GameObject _MenuPlayer4;

        [Header("BOUTON JOUER")]
        [SerializeField] private Button _ButtonPlay;



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


            //pour le mode By_joystick_Controller
            _ButtonWeaponPlayer1.GetComponent<SelectWeapon>()._isSelect = true;
            _ButtonWeaponPlayer2.GetComponent<SelectWeapon>()._isSelect = true;
            _ButtonWeaponPlayer3.GetComponent<SelectWeapon>()._isSelect = true;
            _ButtonWeaponPlayer4.GetComponent<SelectWeapon>()._isSelect = true;



        }

        private void Update()
        {
            
            if (_mainMenu_State == MainMenu_State.CHOICE_PLAYER)
            {
                //Retour au menu
                if (Input.GetKeyDown(KeyCode.Escape)
                    ||
                    (GameManager.Instance.getJoystickPlayer1().prevState.Buttons.B == XInputDotNetPure.ButtonState.Released
                        && GameManager.Instance.getJoystickPlayer1().state.Buttons.B == XInputDotNetPure.ButtonState.Pressed ))
                    this.TransitionToMainMenu();

                //activer le suivant quand le premier est validé
                if(_choiceStyle == ChoiceStyle.ONE_BY_ONE)
                {
                    #region ONE BY ONE
                    this.Player1_ActivateMenuPlayer_One_By_One();

                    if(_ButtonValidationPlayer1.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        if (_ButtonKitPlayer1.GetComponent<SelectKit>() != null)
                            GameManager.Instance.tabPlayerData[0].mainMenuKitSelect = _ButtonKitPlayer1.GetComponent<SelectKit>().mainMenuKitSelect;
                        else
                            Debug.LogError(this.name + " Update --> _ButtonKitPlayer1.GetComponent<SelectKit>() est null");

                        if(_ButtonWeaponPlayer1.GetComponent<SelectWeapon>() != null)
                            GameManager.Instance.tabPlayerData[0].mainMenuWeaponSelect = _ButtonWeaponPlayer1.GetComponent<SelectWeapon>().mainMenuWeaponSelect;
                        else
                            Debug.LogError(this.name + " Update --> _ButtonKitPlayer1.GetComponent<SelectWeapon>() est null");

                        //sélecionne le bouton de sélection d'arme du 2ème Player
                        this.Player2_ActivateMenuPlayer_One_By_One();
                    }
                    if (_ButtonValidationPlayer2.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[1].mainMenuKitSelect = _ButtonKitPlayer2.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[1].mainMenuWeaponSelect = _ButtonWeaponPlayer2.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        //sélecionne le bouton de sélection d'arme du 3ème Player
                        this.Player3_ActivateMenuPlayer_One_By_One();
                    }
                    if (_ButtonValidationPlayer3.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[2].mainMenuKitSelect = _ButtonKitPlayer3.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[2].mainMenuWeaponSelect = _ButtonWeaponPlayer3.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        //sélecionne le bouton de sélection d'arme du 4ème Player
                        this.Player4_ActivateMenuPlayer_One_By_One();
                    }
                    if (_ButtonValidationPlayer4.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[3].mainMenuKitSelect = _ButtonKitPlayer4.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[3].mainMenuWeaponSelect = _ButtonWeaponPlayer4.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        _ButtonPlay.gameObject.SetActive(true);
                        _ButtonPlay.Select();
                    }
                    #endregion ONE BY ONE
                }
                else if(_choiceStyle == ChoiceStyle.BY_JOYSTICK_CONTROLLERS)
                {
                    #region BY JOYSTICK CONTROLLERS
                    //Debug.LogError(this.name + " Update --> joystickPlayerTab.Length : " + GameManager.Instance.joystickPlayerTab.Length);
                    for(int i = 0; i < GameManager.Instance.joystickPlayerTab.Length; i++)
                    {
                        //je dois vérifier l'index des joueurs pour activer le menu correspondant et les link
                        if(GameManager.Instance.joystickPlayerTab[i].canBeUse)
                        {
                            Debug.Log(this.name + " Update --> Player num : " + i + " canBeUse & playerIndex : " + GameManager.Instance.joystickPlayerTab[i].playerIndex);

                            switch (GameManager.Instance.joystickPlayerTab[i].playerIndex)
                            {
                                case XInputDotNetPure.PlayerIndex.One:
                                    Debug.Log(this.name + " Update --> XInputDotNetPure.PlayerIndex.One");
                                    Player1_ActivateMenuPlayer_By_Joystick_Controller();

                                    //je link le select weapon aux 3 boutons
                                    _ButtonWeaponPlayer1.GetComponent<SelectWeapon>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonKitPlayer1.GetComponent<SelectKit>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonValidationPlayer1.GetComponent<ButtonValidation>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;

                                    //ManageMove
                                    #region MANAGE MOVE
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Right == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonWeaponPlayer1.GetComponent<SelectWeapon>()._isSelect == true)
                                        {
                                            _ButtonWeaponPlayer1.GetComponent<SelectWeapon>()._isSelect = false;
                                            _ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonValidationPlayer1.GetComponent<ButtonValidation>()._isSelect = true;
                                        }
                                    }
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Left == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonValidationPlayer1.GetComponent<ButtonValidation>()._isSelect == true)
                                        {
                                            _ButtonValidationPlayer1.GetComponent<ButtonValidation>()._isSelect = false;
                                            _ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer1.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonWeaponPlayer1.GetComponent<SelectWeapon>()._isSelect = true;
                                        }
                                    }
                                    #endregion MANAGE MOVE


                                    break;
                                case XInputDotNetPure.PlayerIndex.Two:
                                    Debug.Log(this.name + " Update --> XInputDotNetPure.PlayerIndex.Two");
                                    Player2_ActivateMenuPlayer_By_Joystick_Controller();


                                    //je link le select weapon aux 3 boutons
                                    _ButtonWeaponPlayer2.GetComponent<SelectWeapon>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonKitPlayer2.GetComponent<SelectKit>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonValidationPlayer2.GetComponent<ButtonValidation>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;

                                    //ManageMove
                                    #region MANAGE MOVE
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Right == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonWeaponPlayer2.GetComponent<SelectWeapon>()._isSelect == true)
                                        {
                                            _ButtonWeaponPlayer2.GetComponent<SelectWeapon>()._isSelect = false;
                                            _ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonValidationPlayer2.GetComponent<ButtonValidation>()._isSelect = true;
                                        }
                                    }
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Left == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonValidationPlayer2.GetComponent<ButtonValidation>()._isSelect == true)
                                        {
                                            _ButtonValidationPlayer2.GetComponent<ButtonValidation>()._isSelect = false;
                                            _ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer2.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonWeaponPlayer2.GetComponent<SelectWeapon>()._isSelect = true;
                                        }
                                    }
                                    #endregion MANAGE MOVE

                                    break;
                                case XInputDotNetPure.PlayerIndex.Three:
                                    Debug.Log(this.name + " Update --> XInputDotNetPure.PlayerIndex.Three");
                                    Player3_ActivateMenuPlayer_By_Joystick_Controller();

                                    //je link le select weapon aux 3 boutons
                                    _ButtonWeaponPlayer3.GetComponent<SelectWeapon>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonKitPlayer3.GetComponent<SelectKit>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonValidationPlayer3.GetComponent<ButtonValidation>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;

                                    //ManageMove
                                    #region MANAGE MOVE
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Right == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonWeaponPlayer3.GetComponent<SelectWeapon>()._isSelect == true)
                                        {
                                            _ButtonWeaponPlayer3.GetComponent<SelectWeapon>()._isSelect = false;
                                            _ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonValidationPlayer3.GetComponent<ButtonValidation>()._isSelect = true;
                                        }
                                    }
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Left == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonValidationPlayer3.GetComponent<ButtonValidation>()._isSelect == true)
                                        {
                                            _ButtonValidationPlayer3.GetComponent<ButtonValidation>()._isSelect = false;
                                            _ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer3.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonWeaponPlayer3.GetComponent<SelectWeapon>()._isSelect = true;
                                        }
                                    }
                                    #endregion MANAGE MOVE

                                    break;
                                case XInputDotNetPure.PlayerIndex.Four:
                                    Debug.Log(this.name + " Update --> XInputDotNetPure.PlayerIndex.Four");
                                    Player4_ActivateMenuPlayer_By_Joystick_Controller();


                                    //je link le select weapon aux 3 boutons
                                    _ButtonWeaponPlayer4.GetComponent<SelectWeapon>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonKitPlayer4.GetComponent<SelectKit>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;
                                    _ButtonValidationPlayer4.GetComponent<ButtonValidation>().playerIndexLink = GameManager.Instance.joystickPlayerTab[i].playerIndex;

                                    //ManageMove
                                    #region MANAGE MOVE
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Right == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonWeaponPlayer4.GetComponent<SelectWeapon>()._isSelect == true)
                                        {
                                            _ButtonWeaponPlayer4.GetComponent<SelectWeapon>()._isSelect = false;
                                            _ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonValidationPlayer4.GetComponent<ButtonValidation>()._isSelect = true;
                                        }
                                    }
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Left == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_ButtonValidationPlayer4.GetComponent<ButtonValidation>()._isSelect == true)
                                        {
                                            _ButtonValidationPlayer4.GetComponent<ButtonValidation>()._isSelect = false;
                                            _ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect = true;
                                        }
                                        else if (_ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect)
                                        {
                                            _ButtonKitPlayer4.GetComponent<SelectKit>()._isSelect = false;
                                            _ButtonWeaponPlayer4.GetComponent<SelectWeapon>()._isSelect = true;
                                        }
                                    }
                                    #endregion MANAGE MOVE

                                    break;
                                default:
                                    break;
                            }
                        }
                    }


                    if (_ButtonValidationPlayer1.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK
                    && _ButtonValidationPlayer2.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK
                    && _ButtonValidationPlayer3.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK
                    && _ButtonValidationPlayer4.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        _ButtonPlay.gameObject.SetActive(true);
                        _ButtonPlay.Select();
                    }
                    #endregion BY JOYSTICK CONTROLLERS
                }
            }
        }

        public void TransitionToChoicePlayer()
        {
            _imageMainMenu.gameObject.SetActive(false);
            _imageChoicePlayer.gameObject.SetActive(true);

            if(_currentChoicePlayerSelectable != null)
            {
                if(_currentChoicePlayerSelectable.GetComponent<Selectable>() != null)
                {
                    _currentChoicePlayerSelectable.GetComponent<Selectable>().Select();
                }
                else
                {
                    Debug.LogError(this.name + " TransitionToChoicePlayer --> _currentChoicePlayerSelectable.GetComponent<Selectable>() est null");
                }
            }

            _mainMenu_State = MainMenu_State.CHOICE_PLAYER;
        }

        public void TransitionToMainMenu()
        {
            _imageMainMenu.gameObject.SetActive(true);
            _imageChoicePlayer.gameObject.SetActive(false);

            //save current selectable
            _currentChoicePlayerSelectable = EventSystem.current.currentSelectedGameObject;

            _ButtonChoicePlayer.Select();

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
        #region ACTIVATE & DISABLE
        public void Player1_ActivateMenuPlayer_One_By_One()
        {
            if(_MenuPlayer1.activeSelf == false)
            {
                _MenuPlayer1.SetActive(true);
                _ButtonWeaponPlayer1.Select();
            }

        }

        public void Player2_ActivateMenuPlayer_One_By_One()
        {
            if (_MenuPlayer2.activeSelf == false)
            {
                _MenuPlayer2.SetActive(true);
                _ButtonWeaponPlayer2.Select();

            }
        }

        public void Player3_ActivateMenuPlayer_One_By_One()
        {
            if (_MenuPlayer3.activeSelf == false)
            {
                _MenuPlayer3.SetActive(true);
                _ButtonWeaponPlayer3.Select();
            }
        }

        public void Player4_ActivateMenuPlayer_One_By_One()
        {
            if (_MenuPlayer4.activeSelf == false)
            {
                _MenuPlayer4.SetActive(true);
                _ButtonWeaponPlayer4.Select();
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


        public void Player1_ActivateMenuPlayer_By_Joystick_Controller()
        {
            if (_MenuPlayer1.activeSelf == false)
            {
                _MenuPlayer1.SetActive(true);
                //_ButtonWeaponPlayer1.Select();
            }
        }

        public void Player2_ActivateMenuPlayer_By_Joystick_Controller()
        {
            if (_MenuPlayer2.activeSelf == false)
            {
                _MenuPlayer2.SetActive(true);
                //_ButtonWeaponPlayer2.Select();
            }
        }

        public void Player3_ActivateMenuPlayer_By_Joystick_Controller()
        {
            if (_MenuPlayer3.activeSelf == false)
            {
                _MenuPlayer3.SetActive(true);
                //_ButtonWeaponPlayer3.Select();
            }
        }

        public void Player4_ActivateMenuPlayer_By_Joystick_Controller()
        {
            if (_MenuPlayer4.activeSelf == false)
            {
                _MenuPlayer4.SetActive(true);
                //_ButtonWeaponPlayer4.Select();
            }
        }

        #endregion ACTIVATE & DISABLE
    }
}

