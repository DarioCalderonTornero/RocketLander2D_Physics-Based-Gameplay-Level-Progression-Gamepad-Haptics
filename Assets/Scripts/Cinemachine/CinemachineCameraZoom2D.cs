using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    public static CinemachineCameraZoom2D Instance { get; private set; }

    public const float NORMAL_ORTOGRAPHIC_SIZE = 10f;   

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float targetOrtographicSize = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        cinemachineCamera.Lens.OrthographicSize = 
        Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrtographicSize, Time.deltaTime * GameLevel.Instance.GetZoomTimeAmount());
    }

    public void SetTargetOrthographicSize(float targetOrthographicSize )
    {
        this.targetOrtographicSize = targetOrthographicSize;
    }

    public void SetNormalOrthographicSize()
    {
        SetTargetOrthographicSize(NORMAL_ORTOGRAPHIC_SIZE);
    }
}
