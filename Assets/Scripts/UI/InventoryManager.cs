using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages inventory list
/// </summary>
/// <remarks>
/// Creates singleton instance that will accept addition and removal of items
/// Also lists out items
/// Having issues in the ListItems function, need a little help with that
/// </remarks>

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

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

    public void ListItems()
    {
        // Clean contents before open
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            /// I am having an issue with the code below, it throws issues when compiling. need a little help
            // var itemName = obj.transform.Find("Item/ItemName").GetComponent<Text>();
            // var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponent<Image>();

            // itemName.text = item.itemName;
            // itemIcon.sprite = item.icon;
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
        } else {
            foreach (Transform item in ItemContent) 
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
                
        }
    }
}

