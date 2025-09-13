using System.Collections.Generic;
using UnityEngine;

public class scripthaunt : MonoBehaviour
{
    private bool isPossessed = false;
    private Vector3 originalScale;
    private GameObject Ghost;
    private MyMovement ghostMovement;
    public FurnitureMovement furnitureMovement;
    public Material material;
    MeshRenderer meshRenderer;
    private Material[] originalMats;
    public List<GameObject> npcs;
    public EnemySpawn enemySpawn;
    public float scareDistance = 300f;
    float stopTime = 5f;
    float lastTime;
    public int interactionRadius = 2;
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = transform.GetComponent<MeshRenderer>();
        originalScale = transform.localScale;

        Ghost = GameObject.FindGameObjectWithTag("Player");
        if (Ghost != null)
        {
            ghostMovement = Ghost.GetComponent<MyMovement>();
        }

        if (ghostMovement == null)
        {
            Debug.LogError("Ghost with movement.cs doesnt exist");
        }
        furnitureMovement = transform.GetComponent<FurnitureMovement>();
        if (meshRenderer != null)
        {
            originalMats = meshRenderer.materials;
        }
        npcs = enemySpawn.npcs;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ghostMovement == null) return;

        if (isPossessed)
        {
            Debug.Log("wooooooo wooooooo");

            if (Input.GetKey(KeyCode.P) && Time.time >= stopTime + lastTime)
            {
                Debug.Log("Scaryyyy");
                foreach (GameObject enemy in npcs)
                {
                    Transform enemyTransform = enemy.transform;
                    float distance = Vector3.Distance(enemyTransform.position, transform.position);
                    if (distance <= scareDistance)
                    {
                        Vector3 direction = transform.position - enemyTransform.position;
                        direction.y = 0f; // ignore vertical difference
                        if (direction != Vector3.zero)
                        {
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, lookRotation, Time.deltaTime * 5f);
                        }

                        // Apply damage
                        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
                        EnemyCharacteristics enemyCharacteristics = enemy.GetComponent<EnemyCharacteristics>();
                        enemyCharacteristics.takeDamage(damage);
                        enemyMove.Scared();
                    }
                }
                lastTime = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPossessed = false;
                Debug.Log(" amongus");


                transform.localScale = originalScale;

                ghostMovement.ReappearFromFurniture();
                furnitureMovement.moveFurniture = false;
            }
            

        }

        else if (!isPossessed)
        {
            float distanceToGhost = Vector3.Distance(transform.position, Ghost.transform.position);

            if (distanceToGhost <= interactionRadius && !ghostMovement.IsPossessing)
            {
                if (meshRenderer != null)
                {
                    Material[] mats = meshRenderer.materials;

                    // Create new array with +1 slot
                    Material[] newMats = new Material[mats.Length + 1];

                    // Copy old materials
                    for (int i = 0; i < mats.Length; i++)
                    {
                        newMats[i] = mats[i];
                    }

                    // Add the new material at the end
                    newMats[mats.Length] = material;

                    // Assign back
                    meshRenderer.materials = newMats;
                }
                // HIGHLIGHT: When ghost is near, become twice the original size.
                // transform.localScale = originalScale * 2;
                Debug.Log("Ghost detected");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("why you press ra");
                    isPossessed = true;
                    ghostMovement.DisappearIntoFurniture();
                    furnitureMovement.moveFurniture = true;
                    if (meshRenderer != null)
                    {
                        meshRenderer.materials = originalMats;
                    }


                    // POSSESSED: When possessed, become half the original size.
                    // transform.localScale = originalScale * 0.5f;
                }
            }
            else
            {
                if (meshRenderer != null)
                {
                    meshRenderer.materials = originalMats;
                }
            }
        }
    
    }
}


