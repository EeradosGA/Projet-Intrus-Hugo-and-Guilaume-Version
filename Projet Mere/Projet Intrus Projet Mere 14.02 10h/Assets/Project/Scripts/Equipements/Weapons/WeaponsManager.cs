using ProjectIntrus.Equipements.Weapons;
using ProjectIntrus.Tools;
using UnityEngine;

public class WeaponsManager : MonoSingleton<WeaponsManager>
{
    [SerializeField]
    GameObject Gun;
    [SerializeField]
    GameObject SMG;
    [SerializeField]
    GameObject Sniper;
    [SerializeField]
    GameObject Shotgun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject CreateWeapon(Weapons.WEAPON_TYPE weaponType)
    {
        switch (weaponType)
        {
            case Weapons.WEAPON_TYPE.GUN:
                return Instantiate(Gun);
                break;
            case Weapons.WEAPON_TYPE.SMG:
                return Instantiate(SMG);
                break;
            case Weapons.WEAPON_TYPE.SHOTGUN:
                return Instantiate(Shotgun);
                break;
            case Weapons.WEAPON_TYPE.SNIPER:
                return Instantiate(Sniper);
                break;
            default:
                return null;
                break;
        }

    }
}
