using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabToSpawn;

    public Transform spawnLocation;

    private void Start()
    {
        //Assign spawner's location in game to the spawn transform
        if (spawnLocation == null)
        {
            spawnLocation = this.transform;
        }
    }
    public void spawn()
    {
        if (prefabToSpawn != null && spawnLocation != null)
        {
            // Instantiate the prefab as a child of spawnLocation
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnLocation.position, spawnLocation.rotation, spawnLocation);

            // Optionally, perform additional initialization on the spawned object
            // InitializeSpawnedObject(spawnedObject);
        }
        else
        {
            Debug.LogError("Prefab to spawn or spawn location not set.");
        }
    }

        private void InitializeSpawnedObject(GameObject spawnedObj)
    {
        // Perform any initialization you need on the spawned object here
    }
}
