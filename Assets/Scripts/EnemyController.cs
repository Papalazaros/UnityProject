using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyController : MonoBehaviour, IGazeReceiver
{
    public float npcHP = 100;
    public float npcDamage = 4;
    public float attackRate = 0.5f;
    public Transform firePoint;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private float nextAttackTime;
    private FloatingHealthBar floatingHealthBar;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        floatingHealthBar = GetComponent<FloatingHealthBar>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    private void Update()
    {
        nextAttackTime -= Time.deltaTime;

        if (agent.remainingDistance - agent.stoppingDistance < 0.01f && nextAttackTime <= 0)
        {
            nextAttackTime = attackRate;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, agent.stoppingDistance) && hit.transform.CompareTag("Player"))
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                health?.TakeDamage(npcDamage);
            }
        }

        agent.destination = Player.instance.transform.position;
        transform.LookAt(new Vector3(Player.instance.transform.transform.position.x, transform.position.y, Player.instance.transform.position.z));
        rigidBody.velocity *= 0.99f;
    }

    public void GazingUpon()
    {
        if (floatingHealthBar)
        {
            floatingHealthBar.gameObject.SetActive(true);
        }
    }

    public void NotGazingUpon()
    {
        if (floatingHealthBar)
        {
            floatingHealthBar.gameObject.SetActive(false);
        }
    }
}