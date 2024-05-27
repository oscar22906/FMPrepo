using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public void Pickup()
    {
        if (Item.itemType != Item.ItemType.Weapon)
        {
            print($"Item is a {Item.itemType}");
            InventoryManager.Instance.Add(Item);
        }
        else
        {
            print("Equipped weapon");
            Equipment.equipment.EquipWeapon(Item);
        }
        Destroy(gameObject);
        
    }
}
