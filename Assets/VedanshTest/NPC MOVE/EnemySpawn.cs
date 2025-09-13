using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // needed for NavMesh

public class EnemySpawn : MonoBehaviour
{
    public GameObject spawnLocations;        // Parent containing empty GameObjects
    public List<GameObject> npcPrefabs;      // List of NPC prefabs to pick from
    public float raycastDistance = 100f;     // How far down to check
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> npcs = new List<GameObject>();

    void Start()
    {
        // Collect all spawn points (children of spawnLocations)
        foreach (Transform child in spawnLocations.transform)
        {
            spawnPoints.Add(child);
        }

        // Spawn NPCs at valid navmesh positions
        foreach (Transform point in spawnPoints)
        {
            SpawnAtGround(point.position);
        }
    }

    void SpawnAtGround(Vector3 startPos)
    {
        RaycastHit hit;

        // Cast a ray downward
        if (Physics.Raycast(startPos, Vector3.down, out hit, raycastDistance))
        {
            // Check if the hit point is on the NavMesh
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {
                // Pick a random NPC from the list
                if (npcPrefabs.Count > 0)
                {
                    GameObject randomNPC = npcPrefabs[Random.Range(0, npcPrefabs.Count)];
                    GameObject temp = Instantiate(randomNPC, navHit.position, Quaternion.identity);
                    npcs.Add(temp);
                    Debug.Log("Spawned NPC: " + randomNPC.name + " at " + navHit.position);
                }
            }
            else
            {
                Debug.LogWarning("Hit ground but not on NavMesh at: " + hit.point);
            }
        }
        else
        {
            Debug.LogWarning("No ground found below spawn point at: " + startPos);
        }
    }
}
