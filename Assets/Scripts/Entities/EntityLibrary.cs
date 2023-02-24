using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration for all entity types.
/// </summary>
/// <remarks>
/// Does not necessarily correspond to prefabs (variations).
/// </remarks>
public enum EntityType
{
    Ship,
    Ant,
    Beetle
}

public class EntityLibrary : MonoBehaviour
{
    [SerializeField]
    private GameObject shipPrefab;

    [SerializeField]
    private GameObject antPrefab;

    [SerializeField]
    private GameObject beetlePrefab;

    //////////////////////////////////////////////////
    // Public Properties and Methods //
    //////////////////////////////////////////////////

    public static EntityLibrary Instance { get; set; }

    public GameObject GetEntity(EntityType type)
    {
        return Instantiate(GetEntityPrefab(type));
    }

    //////////////////////////////////////////////////
    // Private Fields and Methods //
    //////////////////////////////////////////////////

    private GameObject defaultPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            GameManager.Instance.FailGame(true, "Multiple Entity Libraries detected.");

            Destroy(gameObject);
        }
        else
            Instance = this;

        defaultPrefab = GetEntityPrefab(Globals.defaultEntityType);
    }

    private GameObject GetEntityPrefab(EntityType type)
    {
        GameObject prefab = null;

        if (type is EntityType.Ship)
            prefab = shipPrefab;
        else if (type is EntityType.Ant)
            prefab = antPrefab;
        else if (type is EntityType.Beetle)
            prefab = beetlePrefab;
        else
        {
            GameManager.Instance.FailGame(true, "Entity type not found.");
            return null;
        }

        return prefab;
    }
}
