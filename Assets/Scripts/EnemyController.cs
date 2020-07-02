using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyController : MonoBehaviour
{
    public float damage = 5;
    public float attackRate = 0.25f;
    public Transform firePoint;
    public FloatingHealthBar floatingHealthBar;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private float nextAttackTime;
    private Camera mainCamera;
    private IHealth health;
    private bool healthBarActivated;

    private void Awake()
    {
        health = GetComponent<IHealth>();
        agent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        mainCamera = Camera.main;
    }

    private bool OnScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void Update()
    {
        if (!healthBarActivated && health.CurrentHealth < health.MaxHealth && agent.remainingDistance <= agent.stoppingDistance * 3 && OnScreen())
        {
            healthBarActivated = true;
            floatingHealthBar.UpdatePosition(gameObject, mainCamera);
            floatingHealthBar.gameObject.SetActive(true);
        }
        else if (healthBarActivated && (health.CurrentHealth == health.MaxHealth || agent.remainingDistance > agent.stoppingDistance * 3 || !OnScreen()))
        {
            healthBarActivated = false;
            floatingHealthBar.gameObject.SetActive(false);
        }

        nextAttackTime -= Time.deltaTime;

        if (agent.remainingDistance - agent.stoppingDistance < 0.01f && nextAttackTime <= 0)
        {
            nextAttackTime = attackRate;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, agent.stoppingDistance) && hit.transform.CompareTag("Player"))
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                health?.TakeDamage(damage);
            }
        }

        agent.destination = Player.instance.transform.position;
        transform.LookAt(Player.instance.transform);
    }
}