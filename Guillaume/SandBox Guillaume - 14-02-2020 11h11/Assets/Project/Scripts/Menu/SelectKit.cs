using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{

    [RequireComponent(typeof(Button))]
    public class SelectKit : MonoBehaviour
    {
        //[SerializeField]
        //private Scrollbar _scrollBar_Weapon;
        //private float _stepValue;
        //public float stepValue { get { return _stepValue; }  } // valeur de step

        [SerializeField] private Sprite _spriteSoin;
        [SerializeField] private Sprite _spriteMunitions;
        [SerializeField] private Sprite _spriteOuverturePorte;
        [SerializeField] private Sprite _spriteTourelle;


        private Button _button;

        private int _index;
        public int index { get { return _index; } }

        public enum MainMenu_KitSelect
        {
            HEAL,
            AMMO,
            DOOR,
            TURRET
        }
        private MainMenu_KitSelect _mainMenuKitSelect = MainMenu_KitSelect.HEAL;
        public MainMenu_KitSelect mainMenuKitSelect { get { return _mainMenuKitSelect; } }

        //test pour joysitck

        private XInputDotNetPure.PlayerIndex _playerIndexLink;
        public XInputDotNetPure.PlayerIndex playerIndexLink { get { return _playerIndexLink; } set { _playerIndexLink = value; } }

        public bool _isSelect = false;
        // Start is called before the first frame update
        void Start()
        {
            if(this.GetComponent<Button>() != null)
            {
                _button = this.GetComponent<Button>();
            }
            _index = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (this.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                //clavier
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (_index == 0)
                    {
                        _index = 3;
                    }
                    else
                    {
                        _index--;
                    }

                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (_index == 3)
                    {
                        _index = 0;
                    }
                    else
                    {
                        _index++;
                    }

                }
            }

            switch (MainMenuManager.Instance.choiceStyle)
            {
                case MainMenuManager.ChoiceStyle.ONE_BY_ONE:
                    #region ONE_BY_ONE
                    if (this.gameObject == EventSystem.current.currentSelectedGameObject)
                    {

                        //manette
                        if (GameManager.Instance.getJoystickPlayer1().canBeUse)
                        {
                            if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Up == XInputDotNetPure.ButtonState.Released
                                && GameManager.Instance.getJoystickPlayer1().state.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
                            {
                                if (_index == 0)
                                {
                                    _index = 3;
                                }
                                else
                                {
                                    _index--;
                                }
                            }
                            if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Down == XInputDotNetPure.ButtonState.Released
                                && GameManager.Instance.getJoystickPlayer1().state.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
                            {
                                if (_index == 3)
                                {
                                    _index = 0;
                                }
                                else
                                {
                                    _index++;
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError(this.name + " GameManager.Instance.getJoystickPlayer1() can't be use");
                        }

                    }
                    #endregion ONE_BY_ONE
                    break;
                case MainMenuManager.ChoiceStyle.BY_JOYSTICK_CONTROLLERS:
                    #region BY_JOYSTICK_CONTROLLERS

                    //Debug.LogError("YOLOOOOOOO");
                    if (_isSelect == true)
                    {
                        //je dois gérer les boutons en fonction des joysticks
                        for (int i = 0; i < GameManager.Instance.joystickPlayerTab.Length; i++)
                        {
                            Debug.Log(this.name + " Update --> playerIndex : " + GameManager.Instance.joystickPlayerTab[i].playerIndex + " & SelectWeapon PlayerLink : " + this._playerIndexLink);
                            //je dois vérifier l'index des joueurs pour agir sur les boutons correspondant
                            if (GameManager.Instance.joystickPlayerTab[i].playerIndex == this._playerIndexLink)
                            {
                                if (GameManager.Instance.joystickPlayerTab[i].canBeUse)
                                {
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Down == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_index == 3)
                                        {
                                            _index = 0;
                                        }
                                        else
                                        {
                                            _index++;
                                        }
                                    }
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.DPad.Up == XInputDotNetPure.ButtonState.Released && GameManager.Instance.joystickPlayerTab[i].state.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_index == 0)
                                        {
                                            _index = 3;
                                        }
                                        else
                                        {
                                            _index--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion BY_JOYSTICK_CONTROLLERS
                    break;
                default:
                    break;
            }

            

            //manage enum
            switch (_index)
            {
                case 0:
                    _mainMenuKitSelect = MainMenu_KitSelect.HEAL;
                    this.GetComponent<Image>().sprite = _spriteSoin;
                    break;
                case 1:
                    _mainMenuKitSelect = MainMenu_KitSelect.AMMO;
                    this.GetComponent<Image>().sprite = _spriteMunitions;
                    break;
                case 2:
                    _mainMenuKitSelect = MainMenu_KitSelect.DOOR;
                    this.GetComponent<Image>().sprite = _spriteOuverturePorte;
                    break;
                case 3:
                    _mainMenuKitSelect = MainMenu_KitSelect.TURRET;
                    this.GetComponent<Image>().sprite = _spriteTourelle;
                    break;
                default:
                    break;
            }
            //Debug.Log(this.name + "Start --> _index : " + _index + " WeaponSelect : " + _mainMenuWeaponSelect);
            

        } 

        //public void ScrollUp()
        //{
        //    if (_scrollBar_Weapon != null && _scrollBar_Weapon.value >= _stepValue)
        //    {
        //        _scrollBar_Weapon.value -= _stepValue;
        //        _index -= 1;
        //    }
        //}

        //public void ScrollDown()
        //{
        //    if (_scrollBar_Weapon != null && _scrollBar_Weapon.value <= 1 - _stepValue)
        //    {
        //        _scrollBar_Weapon.value += _stepValue;
        //        _index += 1;

        //    }
        //}
    }
}

