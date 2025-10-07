using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ForceArrowUI : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private ForcesSO forcesSO;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowLifetime = 2f; // Tiempo que vive cada flecha

    private float spawnInterval = 0.7f; // Intervalo entre flechas (ajusta para más/menos flechas simultáneas)
    private float spawnTimer;

    private void SpawnArrow()
    {
        Vector3 direction = Vector3.up;
        Quaternion rotation = Quaternion.identity;

        switch (forcesSO.forceDirection)
        {
            case ForcesSO.ForceDirection.up:
                direction = Vector3.up;
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case ForcesSO.ForceDirection.down:
                direction = Vector3.down;
                rotation = Quaternion.Euler(0, 0, -90);
                break;
            case ForcesSO.ForceDirection.left:
                direction = Vector3.left;
                rotation = Quaternion.Euler(0, 0, 180);
                break;
            case ForcesSO.ForceDirection.right:
                direction = Vector3.right;
                rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, rotation, transform);

        ArrowForce mover = arrow.GetComponent<ArrowForce>();
        if (mover != null)
        {
            mover.Init(direction, arrowSpeed);
        }

        Destroy(arrow, arrowLifetime); // Destruye la flecha tras X segundos
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnArrow();
            spawnTimer = 0f;
        }
    }
}