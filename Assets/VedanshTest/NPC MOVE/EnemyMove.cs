using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Idle/Walk Settings")]
    public float idleTime = 3f;        // Seconds before picking new walk point
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
    bool isRunning = false;
    float scaredEndTime;
    float nextActionTime;

    [Header("Animation States")]
    public string IdleString;
    public string WalkString;
    public string RunString;
    public string ScaredString;
    public Transform target;

    void Start()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();

        playAnimation(IdleString);
        nextActionTime = Time.time + idleTime;
    }

    void LateUpdate()
    {
        if (isScared && Time.time < scaredEndTime && !isRunning)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    void Update()
    {
        // Handle scared state
        if (isScared)
        {
            if (Time.time > scaredEndTime && !isRunning)
            {
                Run();
            }
            else if (isRunning && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isRunning = false;
                isScared = false;
                playAnimation(IdleString);
                nextActionTime = Time.time + idleTime;
            }
            return;
        }

        // If time to take an action and not moving → pick a walk destination
        // if (Time.time > nextActionTime && !agent.hasPath)
        // {
        //     Vector3 dest;
        //     if (RandomPoint(transform.position, walkRadius, out dest))
        //     {
        //         agent.speed = walkSpeed;
        //         playAnimation(WalkString);
        //         agent.SetDestination(dest);
        //     }

        //     nextActionTime = Time.time + idleTime;
        // }
        if (Time.time > nextActionTime && !agent.hasPath)
        {
            Vector3 dest;
            if (RandomPoint(transform.position, walkRadius, out dest))
            {
                agent.speed = walkSpeed;
                playAnimation(WalkString);
                agent.SetDestination(dest);
            }

            nextActionTime = Time.time + idleTime + Random.Range(0f, 5f); // stagger queries
        }

        // If reached destination → go idle
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            playAnimation(IdleString);
        }
    }

    public void Run()
    {
        isRunning = true;
        Vector3 dest;
        if (RandomPoint(transform.position, Random.Range(runInnerRadius, runOuterRadius), out dest))
        {
            agent.speed = runSpeed;
            agent.SetDestination(dest);
            playAnimation(RunString);
        }
    }

    public void Scared()
    {
        isScared = true;
        scaredEndTime = Time.time + scaredDuration;
        playAnimation(ScaredString);
        agent.ResetPath(); // stop moving while scared
    }

    // Play animation with your style
    void playAnimation(string animation, float crossFadeTime = 0.1f)
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
