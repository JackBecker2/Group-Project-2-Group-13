using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{

    NavMeshAgent agent;
    [SerializeField] float decisionDelay = 1f;
    [SerializeField] Transform objectToChase;
    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;

    [SerializeField] private float knockbackStrength;

    enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    [SerializeField] EnemyStates currentState;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, decisionDelay);
        if (currentState == EnemyStates.Patrolling)
            agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, objectToChase.position) > 10f) {

            currentState = EnemyStates.Patrolling;

        } else {

            currentState = EnemyStates.Chasing;

        }

        if (currentState == EnemyStates.Patrolling) {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 10f) {

                currentWaypoint++;
                if(currentWaypoint == waypoints.Length) {

                    currentWaypoint = 0;
                }
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    void SetDestination() {
        if(currentState == EnemyStates.Chasing)
        agent.SetDestination(objectToChase.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if(rb != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * knockbackStrength, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
