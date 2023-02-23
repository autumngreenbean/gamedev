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
    public GameObject shipPrefab;

    public GameObject antPrefab;
    public GameObject beetlePrefab;

    public static EntityLibrary Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            GameManager.Instance.FailGame(true, "Multiple Entity Libraries detected.");

            Destroy(gameObject);
        }
        else
            Instance = this;
    }
}
