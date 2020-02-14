using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{

    [RequireComponent(typeof(Button))]
    public class ButtonValidation : MonoBehaviour
    {

        [SerializeField] private Sprite _spriteCheck;
        [SerializeField] private Sprite _spriteUncheck;

        private Button _button;

        private int _index;
        public int index { get { return _index; } }

        public enum ValidationSelect
        {
            UNCHECK = 0,
            CHECK

        }
        public ValidationSelect _validationSelect = ValidationSelect.UNCHECK;
        public ValidationSelect validationSelect { get { return _validationSelect; } }

        //test pour joysitck
        private XInputDotNetPure.PlayerIndex _playerIndexLink;
        public XInputDotNetPure.PlayerIndex playerIndexLink { get { return _playerIndexLink; } set { _playerIndexLink = value; } }

        public bool _isSelect = false;

        // Start is called before the first frame update
        void Start()
        {
            if (this.GetComponent<Button>() != null)
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
                        _index = 1;
                    }
                    else
                    {
                        _index--;
                    }

                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (_index == 1)
                    {
                        _index = 0;
                    }
                    else
                    {
                        _index++;
                    }

                }
            }

            //manette
            switch (MainMenuManager.Instance.choiceStyle)
            {
                case MainMenuManager.ChoiceStyle.ONE_BY_ONE:
                    #region ONE_BY_ONE
                    if (this.gameObject == EventSystem.current.currentSelectedGameObject)
                    {

                        //manette
                        if (GameManager.Instance.getJoystickPlayer1().canBeUse)
                        {
                            //DPAD
                            if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Up == XInputDotNetPure.ButtonState.Released
                                && GameManager.Instance.getJoystickPlayer1().state.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
                            {
                                if (_index == 0)
                                {
                                    _index = 1;
                                }
                                else
                                {
                                    _index--;
                                }
                            }
                            else if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Down == XInputDotNetPure.ButtonState.Released
                                && GameManager.Instance.getJoystickPlayer1().state.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
                            {
                                if (_index == 1)
                                {
                                    _index = 0;
                                }
                                else
                                {
                                    _index++;
                                }
                            }

                            //BUTTON A
                            if (GameManager.Instance.getJoystickPlayer1().prevState.Buttons.A == XInputDotNetPure.ButtonState.Released
                                && GameManager.Instance.getJoystickPlayer1().state.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
                            {
                                if (_index == 1)
                                    _index = 0;
                                else
                                    _index = 1;
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
                                        if (_index == 1)
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
                                            _index = 1;
                                        }
                                        else
                                        {
                                            _index--;
                                        }
                                    }

                                    //BUTTON A
                                    if (GameManager.Instance.joystickPlayerTab[i].prevState.Buttons.A == XInputDotNetPure.ButtonState.Released
                                        && GameManager.Instance.joystickPlayerTab[i].state.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
                                    {
                                        if (_index == 1)
                                            _index = 0;
                                        else
                                            _index = 1;
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
                        _validationSelect = ValidationSelect.UNCHECK;
                        this.GetComponent<Image>().sprite = _spriteUncheck;
                        break;
                    case 1:
                        _validationSelect = ValidationSelect.CHECK;
                        this.GetComponent<Image>().sprite = _spriteCheck;
                        break;
                    default:
                        break;
                }
            
        }
    }
}
