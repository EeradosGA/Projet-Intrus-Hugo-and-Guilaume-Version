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
}
