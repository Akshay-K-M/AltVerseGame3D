using UnityEngine;

public class EnemyCharacteristics : MonoBehaviour
{
    public int maxSanity;
    public int currentSanity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentSanity);
    }

    public void takeDamage(int damage)
    {
        currentSanity -= damage;
        // image.fillAmount = currentSanity / (float)maxSanity;
        if (currentSanity <= 0)
        {
            Die();
            Debug.Log("Died");
        }
    }
    public void Die()
    {
        // transform.GetComponent<EnemyMove>().enabled = false;
        // transform.GetComponent<NavMeshAgent>().enabled = false;
        // ragdoll.ActivateRagDoll();
        // playerController.enemyKillReward();
    }

}
