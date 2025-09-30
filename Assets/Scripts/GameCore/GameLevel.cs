using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public static GameLevel Instance { get; private set; }

    [SerializeField] private int levelNumber;
    [SerializeField] private Transform startLanderPositionTransform;
    [SerializeField] private Transform cameraStartTargetTransform;
    [SerializeField] private float zoomOutOrtographicSize;
    [SerializeField] private float zoomTimeAmount;

    private void Awake()
    {
        Instance = this;    
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    // Returns the starting position for the lander at the beginning of the level
    public Vector3 GetStartLanderPosition()
    {
        return startLanderPositionTransform.position;
    }

    // Returns the transform for the camera to focus on at the start of the level
    public Transform GetCameraStartTargetTransform()
    {
        return cameraStartTargetTransform;
    }

    // Returns the orthographic size for zoomed out camera view
    public float GetZoomOutOrtographicSize()
    {
        return zoomOutOrtographicSize;
    }

    // Returns the zoom time amount for camera zooming
    public float GetZoomTimeAmount()
    {
        return zoomTimeAmount;
    }
}
