using UnityEngine;

public class scripthaunt : MonoBehaviour
{
    private bool isPossessed = false;
    private Vector3 originalScale;
    private GameObject Ghost;
    private Movement ghostMovement;

    public int interactionRadius = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            }
        }
        
        else if (!isPossessed)
        {
            float distanceToGhost = Vector3.Distance(transform.position, Ghost.transform.position);

            if (distanceToGhost <= interactionRadius && !ghostMovement.IsPossessing)
            {
                // HIGHLIGHT: When ghost is near, become twice the original size.
                transform.localScale = originalScale * 2;
                Debug.Log("Ghost detected");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("why you press ra");
                    isPossessed = true;
                    ghostMovement.DisappearIntoFurniture();
                    
                    // POSSESSED: When possessed, become half the original size.
                    transform.localScale = originalScale * 0.5f;
                }
            }
            else
            {
                // NORMAL: When ghost is far away or busy, return to original size.
                transform.localScale = originalScale;
            }
        }
    
    }
}


