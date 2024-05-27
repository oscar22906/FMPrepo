using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create New Weapon")]
public class Weapon : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public WeaponType weaponType;
    [Header("Stats")]
    public float maxDamage;
    public float minDamage;

    public enum WeaponType
    {
        Axe,
        Sword,
        Spear
    }
}
