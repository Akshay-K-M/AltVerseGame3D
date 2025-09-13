using UnityEngine;

public class scripthaunt : MonoBehaviour
{
    private bool isPossessed = false;
    private Vector3 originalScale;
    private GameObject Ghost;
    private Movement ghostMovement;
    public FurnitureMovement furnitureMovement;
    public Material material;
    MeshRenderer meshRenderer;
    private Material[] originalMats;

    public int interactionRadius = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = transform.GetComponent<MeshRenderer>();
        originalScale = transform.localScale;

        Ghost = GameObject.FindGameObjectWithTag("Player");
        if (Ghost != null)
        {
            ghostMovement = Ghost.GetComponent<Movement>();
        }

        if (ghostMovement == null)
        {
            Debug.LogError("Ghost with movement.cs doesnt exist");
        }
        furnitureMovement = transform.GetComponent<FurnitureMovement>();
        originalMats = meshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ghostMovement == null) return;

        if (isPossessed)
        {
            Debug.Log("wooooooo wooooooo");

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
                // HIGHLIGHT: When ghost is near, become twice the original size.
                // transform.localScale = originalScale * 2;
                Debug.Log("Ghost detected");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("why you press ra");
                    isPossessed = true;
                    ghostMovement.DisappearIntoFurniture();
                    furnitureMovement.moveFurniture = true;
                    meshRenderer.materials = originalMats;

                    
                    // POSSESSED: When possessed, become half the original size.
                    // transform.localScale = originalScale * 0.5f;
                }
            }
            else
            {
                meshRenderer.materials = originalMats;
            }
        }
    
    }
}


