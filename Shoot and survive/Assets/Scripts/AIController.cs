using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{    
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float maxDistanceToTarget = 5f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float resetTimeBetweenAttacks;
    [SerializeField] private int damage = 10;

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
        agent.stoppingDistance = maxDistanceToTarget;
        agent.speed = movementSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
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
        if(PlayerController.playerHealthStatic > 0)
        {
            anim.SetTrigger("Attacking");
            if (timeBetweenAttacks <= 0)
            {
                PlayerController.TakeDamage(damage);
                timeBetweenAttacks = resetTimeBetweenAttacks;
            }
        }
    }
}
