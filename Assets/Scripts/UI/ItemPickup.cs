using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages item pickup
/// </summary>


public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
