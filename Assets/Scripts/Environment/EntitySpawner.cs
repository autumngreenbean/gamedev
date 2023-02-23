using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A spawner for all types of entities.
/// </summary>
public class EntitySpawner : MonoBehaviour
{
    // Defaults
    public static EntityType defaultType = EntityType.Ant;

    [Header("Spawner Settings")]
    public EntityType entityType = defaultType;

    public Dictionary<float, EntitySpawn> timedSpawns = new Dictionary<float, EntitySpawn>();
}

/// <summary>
/// Holds data for EntitySpawner spawn.
/// </summary>
/// <remarks>
/// This is serializable so we can create nice lists in the inspector.
/// </remarks>
[System.Serializable]
public class EntitySpawn
{
    public EntityType type;
    public int quantity = 1;
    public float delayInSeconds;
}
