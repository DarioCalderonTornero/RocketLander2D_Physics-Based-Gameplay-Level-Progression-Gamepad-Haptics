using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ForceArrowUI : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private ForcesSO forcesSO;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float arrowSpeed;

    private float spawnInterval = 2f;   
    private float spawnTimer;

    private void Update()
    {
        switch(forcesSO.forceDirection)
        {
            case ForcesSO.ForceDirection.up:
                arrowPrefab.transform.position += Vector3.up * arrowSpeed;
                break;

            case ForcesSO.ForceDirection.down:
                arrowPrefab.transform.position += Vector3.down * arrowSpeed;
                break;

            case ForcesSO.ForceDirection.left:
                arrowPrefab.transform.position += Vector3.left * arrowSpeed;
                break;
            case ForcesSO.ForceDirection.right:
                arrowPrefab.transform.position += Vector3.right * arrowSpeed;
                break;

        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnArrow();
            spawnTimer = 0f;
        }
    }

    private void SpawnArrow()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(arrowPrefab, spawnPoints[randomIndex].position, Quaternion.identity, transform);
    }
}
