using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A spawner for all types of entities.
/// </summary>
public class EntitySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EntitySpawn> timedSpawns = new List<EntitySpawn>();

    //////////////////////////////////////////////////
    // Public Properties and Methods //
    //////////////////////////////////////////////////

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    public void Reset()
    {
        initialTimeInSeconds = Time.time;
        elapsedTimeInSeconds = 0;

        ResetSpawns();
    }

    //////////////////////////////////////////////////
    // Private Fields and Methods //
    //////////////////////////////////////////////////

    // References
    EntityLibrary library;

    private List<EntitySpawn> remainingSpawns;

    bool active = false; // Whether the spawner is active (enables timer)
    float initialTimeInSeconds;
    float elapsedTimeInSeconds;

    private static EntityType defaultType = EntityType.Ant;

    private void Start()
    {
        library = EntityLibrary.Instance;
        initialTimeInSeconds = Time.time;

        ResetSpawns();
        Activate();
    }

    private void Update()
    {
        if (remainingSpawns.Count < 1)
            Deactivate();

        if (active)
        {
            elapsedTimeInSeconds = Time.time - initialTimeInSeconds;

            for (int spawnIndex = remainingSpawns.Count - 1; spawnIndex >= 0; spawnIndex--) // Iterate from back so removals don't mess things up
            {
                EntitySpawn spawn = remainingSpawns[spawnIndex];

                if (spawn.delayInSeconds < elapsedTimeInSeconds)
                {
                    for (int i = 0; i < spawn.quantity; i++)
                    {
                        SpawnEntity(spawn);
                    }

                    remainingSpawns.Remove(spawn);
                }
            }
        }
    }

    private void SpawnEntity(EntitySpawn spawn)
    {
        GameObject entity = library.GetEntity(spawn.type);

        entity.transform.position = GetSpawnPosition(spawn.positionRadius);
    }

    private Vector3 GetSpawnPosition(float offsetRadius)
    {
        // Get random offset
        Vector2 spawnOffset = Random.insideUnitCircle * offsetRadius;

        // Add offset to spawner position
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, 0f, spawnOffset.y);

        return spawnPosition;
    }

    private void ResetSpawns()
    {
        remainingSpawns = timedSpawns;
    }
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
    public EntityType type = Globals.defaultEntityType;
    public int quantity = 1;
    public float delayInSeconds;
    public float positionRadius;
}
