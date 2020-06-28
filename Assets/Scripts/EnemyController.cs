using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyController : MonoBehaviour
{
    public float attackDistance = 2f;
    public float movementSpeed = 0.5f;
    public float npcHP = 100;
    public float npcDamage = 4;
    public float attackRate = 0.5f;
    public Transform firePoint;
    public GameObject npcDeadPrefab;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private float nextAttackTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
    }

    private void Update()
    {
        nextAttackTime -= Time.deltaTime;

        if (agent.remainingDistance - attackDistance < 0.01f && nextAttackTime == 0)
        {
            nextAttackTime = attackRate;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, attackDistance) && hit.transform.CompareTag("Player"))
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                health?.TakeDamage(npcDamage);
            }
        }

        agent.destination = Player.instance.transform.position;
        transform.LookAt(new Vector3(Player.instance.transform.transform.position.x, transform.position.y, Player.instance.transform.position.z));
        rigidBody.velocity *= 0.99f;
    }

    private void OnDestroy()
    {
        GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
        npcDead.GetComponent<Rigidbody>().velocity = (-(Player.instance.transform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
    }
}