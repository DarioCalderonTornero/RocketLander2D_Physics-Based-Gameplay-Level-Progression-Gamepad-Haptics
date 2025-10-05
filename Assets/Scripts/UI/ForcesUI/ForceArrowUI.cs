using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ForceArrowUI : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private ForcesSO forces;           // Mismo SO que usa tu zona (solo para leer dirección)
    [SerializeField] private GameObject visualPrefab;   // Debe asignarse aquí (no se usa ForcesSO.visualGameObject)

    [Header("Spawn Points (manual)")]
    [SerializeField] private List<Transform> spawnPoints = new(); // Define tú dónde spawnear

    [Header("Batch")]
    [SerializeField] private int minCount = 3;
    [SerializeField] private int maxCount = 4;

    [Header("Timing")]
    [SerializeField] private float intervalMin = 1.5f;
    [SerializeField] private float intervalMax = 3.0f;
    [SerializeField] private float visualLifetime = 2.0f;

    [Header("Options")]
    [SerializeField] private Transform visualsParent;   // opcional, para agrupar instancias
    [SerializeField] private bool runOnEnable = true;
    [SerializeField] private bool onlyIfDirectional = true;

    private readonly List<GameObject> spawned = new();
    private Coroutine loopCoroutine;

    private void Reset()
    {
        if (!visualsParent) visualsParent = transform;
    }

    private void OnEnable()
    {
        if (runOnEnable) StartLoop();
    }

    private void OnDisable()
    {
        StopLoop();
        DestroySpawned();
    }

    public void StartLoop()
    {
        if (loopCoroutine != null) return;

        if (onlyIfDirectional && (forces == null || forces.forceType != ForcesSO.ForceType.Directional))
            return;

        if (visualPrefab == null)
        {
            Debug.LogWarning($"[{name}] No visualPrefab asignado en el componente.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning($"[{name}] No hay spawnPoints definidos. Añade al menos 1 punto.");
            return;
        }

        loopCoroutine = StartCoroutine(SpawnLoop());
    }

    public void StopLoop()
    {
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
            loopCoroutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Limpia batch anterior
            DestroySpawned();

            // Spawnea un batch nuevo
            int maxPossible = Mathf.Max(1, spawnPoints.Count);
            int count = Mathf.Clamp(Random.Range(minCount, maxCount + 1), 1, maxPossible);
            SpawnBatchUniquePoints(count);

            // Espera aleatoria y repite
            float wait = Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(wait);
        }
    }

    private void SpawnBatchUniquePoints(int count)
    {
        // Elegimos puntos únicos (sin repetir) de la lista
        List<int> indices = new List<int>(spawnPoints.Count);
        for (int i = 0; i < spawnPoints.Count; i++) indices.Add(i);
        Shuffle(indices);

        float zRot = GetZRotationFromDirection();

        for (int i = 0; i < count; i++)
        {
            Transform p = spawnPoints[indices[i]];
            if (!p) continue;
            SpawnOne(p.position, zRot);
        }
    }

    private void SpawnOne(Vector2 position, float zRotation)
    {
        GameObject go = Instantiate(visualPrefab, position, Quaternion.Euler(0, 0, zRotation), visualsParent);
        spawned.Add(go);
        if (visualLifetime > 0f)
        {
            Destroy(go, visualLifetime);
        }
    }

    private void DestroySpawned()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if (spawned[i]) Destroy(spawned[i]);
        }
        spawned.Clear();
    }

    private static void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    /// <summary>
    /// Ajusta si tu visual “apunta” a otro eje. Asumo que tu prefab apunta a +X por defecto.
    /// </summary>
    private float GetZRotationFromDirection()
    {
        if (forces == null) return 0f;

        switch (forces.forceDirection)
        {
            case ForcesSO.ForceDirection.up: return 90f;
            case ForcesSO.ForceDirection.down: return -90f;
            case ForcesSO.ForceDirection.left: return 180f;
            case ForcesSO.ForceDirection.right: return 0f;
            default: return 0f;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.yellow;
        foreach (var p in spawnPoints)
        {
            if (!p) continue;
            Gizmos.DrawSphere(p.position, 0.08f);
        }
    }
#endif
}
