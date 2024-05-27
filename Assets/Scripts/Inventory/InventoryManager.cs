
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private Dictionary<string, (Item item, int count)> itemCounts = new Dictionary<string, (Item item, int count)>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    public void Remove(Item item, bool cheatsActive)
    {
        // Find the item in the inventory
        Item existingItem = Items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            // If the item exists in the dictionary, decrement its count
            if (itemCounts.ContainsKey(existingItem.itemName))
            {
                // Update the count of the item in the dictionary
                itemCounts[existingItem.itemName] = (existingItem, itemCounts[existingItem.itemName].count - 1);

                // If the count drops to 0 or below, remove the item from the dictionary
                if (itemCounts[existingItem.itemName].count <= 0)
                {
                    itemCounts.Remove(existingItem.itemName);
                }
            }
            // Refresh the display
            ListItems();
        }
    }

    public void ListItems()
    {
        // Clear the dictionary before populating it with the current items
        itemCounts.Clear();

        foreach (var item in Items)
        {
            if (itemCounts.ContainsKey(item.itemName))
            {
                itemCounts[item.itemName] = (item, itemCounts[item.itemName].count + 1);
            }
            else
            {
                itemCounts[item.itemName] = (item, 1);
            }
        }

        // Clear previous items
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Instantiate and display items with their counts
        foreach (var kvp in itemCounts)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = $"{kvp.Key} x{kvp.Value.count}";
            itemIcon.sprite = kvp.Value.item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        StatManager.statManager.UpdateStats(itemCounts);
        SetInventoryItems();

    }


    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        int itemsCount = Items.Count;
        int inventoryItemsCount = InventoryItems.Length;

        int index = 0;
        while (index < itemsCount && index < inventoryItemsCount)
        {
            InventoryItems[index].AddItem(Items[index]);
            index++;
        }
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }
}

