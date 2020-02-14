using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{

    [RequireComponent(typeof(Button))]
    public class SelectWeapon : MonoBehaviour
    {
        //[SerializeField]
        //private Scrollbar _scrollBar_Weapon;
        //private float _stepValue;
        //public float stepValue { get { return _stepValue; }  } // valeur de step

        [SerializeField] private Sprite _spriteSMG;
        [SerializeField] private Sprite _spriteShotgun;
        [SerializeField] private Sprite _spriteSniper;

        private Button _button;

        private int _index;
        public int index { get { return _index; } }

        public enum MainMenu_WeaponSelect
        {
            SMG,
            SHOTGUN,
            SNIPER
        }
        private MainMenu_WeaponSelect _mainMenuWeaponSelect = MainMenu_WeaponSelect.SMG;
        public MainMenu_WeaponSelect mainMenuWeaponSelect { get { return _mainMenuWeaponSelect; } }

        // Start is called before the first frame update
        void Start()
        {
            ////ScrollBar
            //if(this.GetComponent<ScrollRect>().verticalScrollbar != null)
            //{
            //    _scrollBar_Weapon = this.GetComponent<ScrollRect>().verticalScrollbar;

            //    //manage verticalScrollbar
            //    _scrollBar_Weapon.value = 0f;

            //    _scrollBar_Weapon.numberOfSteps = this.GetComponent<ScrollRect>().content.GetComponentsInChildren<Image>().Length;
                
            //}
            //else { Debug.LogError(this.name + "Start --> verticalScrollbar est null"); }

            //_stepValue = 1f / (_scrollBar_Weapon.numberOfSteps - 1);


            if(this.GetComponent<Button>() != null)
            {
                _button = this.GetComponent<Button>();
            }
            _index = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(this.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                //clavier
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    if(_index == 0)
                    {
                        _index = 2;
                    }
                    else
                    {
                        _index--;
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (_index == 2)
                    {
                        _index = 0;
                    }
                    else
                    {
                        _index++;
                    }

                }

                //manette
                if(GameManager.Instance.getJoystickPlayer1().canBeUse)
                {
                    if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Up == XInputDotNetPure.ButtonState.Released
                        && GameManager.Instance.getJoystickPlayer1().state.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
                    {
                        if (_index == 0)
                        {
                            _index = 2;
                        }
                        else
                        {
                            _index--;
                        }
                    }
                    if (GameManager.Instance.getJoystickPlayer1().prevState.DPad.Down == XInputDotNetPure.ButtonState.Released
                        && GameManager.Instance.getJoystickPlayer1().state.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
                    {
                        if (_index == 2)
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

                //manage enum
                switch (_index)
                {
                    case 0:
                        _mainMenuWeaponSelect = MainMenu_WeaponSelect.SMG;
                        this.GetComponent<Image>().sprite = _spriteSMG;
                        break;
                    case 1:
                        _mainMenuWeaponSelect = MainMenu_WeaponSelect.SHOTGUN;
                        this.GetComponent<Image>().sprite = _spriteShotgun;
                        break;
                    case 2:
                        _mainMenuWeaponSelect = MainMenu_WeaponSelect.SNIPER;
                        this.GetComponent<Image>().sprite = _spriteSniper;
                        break;
                    default:
                        break;
                }
                //Debug.Log(this.name + "Start --> _index : " + _index + " WeaponSelect : " + _mainMenuWeaponSelect);
            }

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

