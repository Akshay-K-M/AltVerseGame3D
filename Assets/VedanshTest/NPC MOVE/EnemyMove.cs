using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Idle/Walk Settings")]
    public float idleTime = 3f;        // Seconds between picking new walk point
    public float walkRadius = 10f;     // Radius for random walk points
    public float walkSpeed = 2f;

    [Header("Run Settings")]
    public float runSpeed = 5f;
    public float runInnerRadius = 15f;
    public float runOuterRadius = 25f;

    [Header("Scared Settings")]
    public float scaredDuration = 3f;

    string currentAnimation = "";
    bool isScared = false;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();

        StartCoroutine(IdleRoutine());
    }

    IEnumerator IdleRoutine()
    {
        while (true)
        {
            if (!isScared)
            {
                playAnimation("idle");

                yield return new WaitForSeconds(idleTime);

                Vector3 dest;
                if (RandomPoint(transform.position, walkRadius, out dest))
                {
                    agent.speed = walkSpeed;
                    agent.SetDestination(dest);
                    playAnimation("walk");
                }
            }

            yield return null;
        }
    }

    public void Run()
    {
        StopAllCoroutines();
        playAnimation("run");

        Vector3 dest;
        if (RandomPoint(transform.position, Random.Range(runInnerRadius, runOuterRadius), out dest))
        {
            agent.speed = runSpeed;
            agent.SetDestination(dest);
        }

        StartCoroutine(ResumeIdle());
    }

    public void Scared()
    {
        if (!isScared)
            StartCoroutine(ScaredRoutine());
    }

    IEnumerator ScaredRoutine()
    {
        isScared = true;
        playAnimation("scared");

        yield return new WaitForSeconds(scaredDuration);

        isScared = false;
    }

    IEnumerator ResumeIdle()
    {
        yield return new WaitForSeconds(idleTime);
        StartCoroutine(IdleRoutine());
    }

    // Play animation with your style
    void playAnimation(string animation, float crossFadeTime = 0.2f)
    {
        if (animation != "shoot")
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                animator.CrossFade(animation, crossFadeTime);
            }
        }
        else
        {
            currentAnimation = animation;
            animator.CrossFadeInFixedTime(animation, 0.2f, 0, 0f);
        }
    }

    // Pick a random valid NavMesh point
    bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
