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
    [SerializeField] float damage = 10;

    private GameObject player;
    private float playerHealth;
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        resetTimeBetweenAttacks = timeBetweenAttacks;
        player = FindObjectOfType<PlayerController>().gameObject;
        playerHealth = player.GetComponent<Health>().health;
        anim = GetComponent<Animator>();
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
        if(playerHealth > 0)
        {
            anim.SetTrigger("Attacking");
            if (timeBetweenAttacks <= 0)
            {
                playerHealth -= damage;
                timeBetweenAttacks = resetTimeBetweenAttacks;
            }
        }
        print(playerHealth);
    }
}
