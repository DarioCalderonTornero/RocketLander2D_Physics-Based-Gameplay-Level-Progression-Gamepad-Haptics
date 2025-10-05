using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = .1f;

    private Vector2 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        Vector2 newCameraPos = Camera.main.transform.position;

        Vector2 positionDelta = newCameraPos - lastCameraPosition;

        transform.position += (Vector3)positionDelta * parallaxMultiplier;

        lastCameraPosition = newCameraPos;
    }
}
