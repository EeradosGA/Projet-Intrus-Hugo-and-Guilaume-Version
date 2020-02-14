using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{

    public class MainMenuManager : MonoBehaviour
    {
        public enum MainMenu_State
        {
            MAIN_MENU,
            CHOICE_PLAYER
        }
        private MainMenu_State _mainMenu_State = MainMenu_State.MAIN_MENU;

        public enum ChoiceStyle
        {
            ONE_BY_ONE,
            BY_JOYSTICK_CONTROLLERS
        }
        [Header("CHOICE STYLE")]
        [SerializeField] private ChoiceStyle _choiceStyle = ChoiceStyle.ONE_BY_ONE;

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
                    this.Player1_ActivateMenuPlayer();

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
                        this.Player2_ActivateMenuPlayer();
                    }
                    if (_ButtonValidationPlayer2.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[1].mainMenuKitSelect = _ButtonKitPlayer2.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[1].mainMenuWeaponSelect = _ButtonWeaponPlayer2.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        //sélecionne le bouton de sélection d'arme du 3ème Player
                        this.Player3_ActivateMenuPlayer();
                    }
                    if (_ButtonValidationPlayer3.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[2].mainMenuKitSelect = _ButtonKitPlayer3.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[2].mainMenuWeaponSelect = _ButtonWeaponPlayer3.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        //sélecionne le bouton de sélection d'arme du 4ème Player
                        this.Player4_ActivateMenuPlayer();
                    }
                    if (_ButtonValidationPlayer4.GetComponent<ButtonValidation>().validationSelect == ButtonValidation.ValidationSelect.CHECK)
                    {
                        //actualise les données du GameManager
                        GameManager.Instance.tabPlayerData[3].mainMenuKitSelect = _ButtonKitPlayer4.GetComponent<SelectKit>().mainMenuKitSelect;
                        GameManager.Instance.tabPlayerData[3].mainMenuWeaponSelect = _ButtonWeaponPlayer4.GetComponent<SelectWeapon>().mainMenuWeaponSelect;

                        _ButtonPlay.gameObject.SetActive(true);
                        _ButtonPlay.Select();
                    }

         
                }
                else if(_choiceStyle == ChoiceStyle.BY_JOYSTICK_CONTROLLERS)
                {
                    //il faudrait adapter avce l'index du player et non le nombre
                    if (GameManager.Instance.nbrJoysitckControllerConnect == 4)
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


                    if (_MenuPlayer1.activeSelf
                    && _MenuPlayer2.activeSelf
                    && _MenuPlayer3.activeSelf
                    && _MenuPlayer4.activeSelf)
                    {
                        _ButtonPlay.gameObject.SetActive(true);
                    }
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
        public void Player1_ActivateMenuPlayer()
        {
            if(_MenuPlayer1.activeSelf == false)
            {
                _MenuPlayer1.SetActive(true);
                _ButtonWeaponPlayer1.Select();
            }

        }

        public void Player2_ActivateMenuPlayer()
        {
            if (_MenuPlayer2.activeSelf == false)
            {
                _MenuPlayer2.SetActive(true);
                _ButtonWeaponPlayer2.Select();

            }
        }

        public void Player3_ActivateMenuPlayer()
        {
            if (_MenuPlayer3.activeSelf == false)
            {
                _MenuPlayer3.SetActive(true);
                _ButtonWeaponPlayer3.Select();
            }
        }

        public void Player4_ActivateMenuPlayer()
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
        #endregion ACTIVATE & DISABLE
    }
}

