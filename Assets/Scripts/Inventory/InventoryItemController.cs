using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button removeButton;
    private GameObject player;
    private GameObject dropPosition;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dropPosition = GameObject.FindGameObjectWithTag("DropPosition");
    }

    public void DropItem()
    {
        if (item != null)
        {
            if (player != null)
            {
                item.prefab.layer = LayerMask.NameToLayer("Interactable");
                GameObject droppedItem = Instantiate(item.prefab, dropPosition.transform.position, player.transform.rotation);
                if (droppedItem.GetComponent<Rigidbody>() != null)
                {
                    Vector3 direction = player.transform.forward;
                    droppedItem.GetComponent<Rigidbody>().AddForce(direction * 300);
                }
            }
        }
        RemoveItem();
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        InventoryManager.Instance.ListItems();

        Destroy(gameObject);
    }


    public void AddItem(Item newItem)
    {
        item = newItem;
    }
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
                // manipulate health
                Debug.Log("Used Potion");
                RemoveItem();
                break;
            case Item.ItemType.Material:
                // manipulate material balance
                Debug.Log("Collected Material");
                break;
            case Item.ItemType.Weapon:
                // Equip weapon
                print("How did this get in there");
                RemoveItem();
                break;
            case Item.ItemType.Collectable:
                // nothing
                Debug.Log("Used Collectable");
                RemoveItem();
                break;
            default:
                print("Invalid item type");
                break;
        }
    }
}
