using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///    Script to handle inventory items
/// </summary>
/// <remarks>
/// Some quick definitions:
///   id: easy way to reference item type
///   value: potency of item. potion w value 15 restores 15 HP for example
/// </remarks>

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
}
