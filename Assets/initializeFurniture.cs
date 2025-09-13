using System.Collections.Generic;
using UnityEngine;

public class initializeFurniture : MonoBehaviour
{
    public List<GameObject> furnitureList;
    public Material material;
    public EnemySpawn enemySpawn;

    void Awake()
    {
        if (furnitureList == null || furnitureList.Count == 0)
        {
            Debug.LogWarning("Furniture Manager: The furniture list is empty. No scripts were added.");
            return;
        }
        Debug.Log("Furniture Manager: Starting to add scripts to " + furnitureList.Count + " objects.");

        foreach (GameObject furnitureObject in furnitureList)
        {
            if (furnitureObject.GetComponent<scripthaunt>() == null)    //incase the object already has scripthaunt
            {
                scripthaunt shaunt = furnitureObject.AddComponent<scripthaunt>();
                FurnitureMovement furnitureMovement = furnitureObject.AddComponent<FurnitureMovement>();
                // furnitureMovement.
                shaunt.enemySpawn = enemySpawn;
                shaunt.material = material;

                Debug.Log("Added Furniture script to: " + furnitureObject.name);
            }
            else
            {
                Debug.Log(furnitureObject.name + " already has the Furniture script. Skipped.");
            }
        }

    }
}
