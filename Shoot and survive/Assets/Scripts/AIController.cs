using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{    
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float maxDistanceToTarget = 5f;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float resetTimeBetweenAttacks;
    [SerializeField] int damage = 10;

    private GameObject player;
    private float playerHealth;
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        resetTimeBetweenAttacks = timeBetweenAttacks;
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        print(distance);
        if (distance > maxDistanceToTarget)
        {
            agent.destination = player.transform.position;
        }
        else if(distance <= maxDistanceToTarget)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timeBetweenAttacks -= Time.deltaTime;
        if(Health.playerHealthStatic > 0)
        {
            anim.SetTrigger("Attacking");
            if (timeBetweenAttacks <= 0)
            {
                Health.playerHealthStatic -= damage;
                timeBetweenAttacks = resetTimeBetweenAttacks;
            }
        }
        print(Health.playerHealthStatic);
    }
}
