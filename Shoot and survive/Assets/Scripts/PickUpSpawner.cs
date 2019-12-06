using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenPickUps = 10f;
    [SerializeField] private int maxItemsActive;
    private bool isSpawning = false;
    public GameObject[] pickUp;
    public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnPickUp());
    }

    private void Update()
    {
        if(maxItemsActive <= this.transform.childCount)
        {
            return;
        }

        if (!isSpawning)
        {
            StartCoroutine(SpawnPickUp());
        }
    }

    IEnumerator SpawnPickUp()
    {
        isSpawning = true;

        float randomTime = Random.Range((timeBetweenPickUps / 2), timeBetweenPickUps);
        GameObject randomItem = pickUp[Random.Range(0, pickUp.Length)];
        Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(randomItem, randomSpot.position, randomItem.transform.rotation);

        yield return new WaitForSeconds(randomTime);

        isSpawning = false;
    }
}
