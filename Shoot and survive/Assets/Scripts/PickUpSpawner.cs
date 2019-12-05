using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] float timeBetweenPickUps = 10f;
    public GameObject[] pickUp;
    public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnPickUp());
    }

    IEnumerator SpawnPickUp()
    {
        GameObject randomEnemy = pickUp[Random.Range(0, pickUp.Length)];
        Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
        yield return new WaitForSeconds(timeBetweenPickUps);
    }
}

