using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{

    public class SelectWeapon : MonoBehaviour
    {
        [SerializeField]
        private Scrollbar _scrollBar_Weapon;
        private float _stepValue;
        public float stepValue { get { return _stepValue; }  } // valeur de step

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

            //ScrollBar
            if(this.GetComponent<ScrollRect>().verticalScrollbar != null)
            {
                _scrollBar_Weapon = this.GetComponent<ScrollRect>().verticalScrollbar;

                //manage verticalScrollbar
                _scrollBar_Weapon.value = 0f;

                _scrollBar_Weapon.numberOfSteps = this.GetComponent<ScrollRect>().content.GetComponentsInChildren<Image>().Length;
                
            }
            else { Debug.LogError(this.name + "Start --> verticalScrollbar est null"); }

            _stepValue = 1f / (_scrollBar_Weapon.numberOfSteps - 1);
            _index = 0;
        }

        // Update is called once per frame
        void Update()
        {

            switch (_index)
            {
                case 0:
                    _mainMenuWeaponSelect = MainMenu_WeaponSelect.SMG;
                    break;
                case 1:
                    _mainMenuWeaponSelect = MainMenu_WeaponSelect.SHOTGUN;
                    break;
                case 2:
                    _mainMenuWeaponSelect = MainMenu_WeaponSelect.SNIPER;
                    break;
                default:
                    break;
            }
            //Debug.Log(this.name + "Start --> _index : " + _index + " WeaponSelect : " + _mainMenuWeaponSelect);
        } 

        public void ScrollUp()
        {
            if (_scrollBar_Weapon != null && _scrollBar_Weapon.value >= _stepValue)
            {
                _scrollBar_Weapon.value -= _stepValue;
                _index -= 1;
            }
        }

        public void ScrollDown()
        {
            if (_scrollBar_Weapon != null && _scrollBar_Weapon.value <= 1 - _stepValue)
            {
                _scrollBar_Weapon.value += _stepValue;
                _index += 1;

            }
        }
    }
}

