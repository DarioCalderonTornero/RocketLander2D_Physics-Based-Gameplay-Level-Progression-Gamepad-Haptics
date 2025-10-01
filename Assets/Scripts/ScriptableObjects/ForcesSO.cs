using UnityEngine;

[CreateAssetMenu(fileName = "ForcesSO", menuName = "ScriptableObjects/ForcesSO")]
public class ForcesSO : ScriptableObject
{
    public enum ForceType
    {
        Directional,
        radial,
        gravity,
        impulse,
        Torque
    }

    public enum ForceDirection
    {
        up,
        down,
        left,
        right
    }

    [Header("General")]
    public ForceType forceType; 
    public float duration = 0f; // Duration in seconds, 0 means infinite
    public bool applyEveryFrame = true; // If false, apply only once when entering the zone

    [Header("Direction")]
    public Vector2 direction = Vector2.up;
    public ForceDirection forceDirection;

    [Header("Radial")]
    public bool isAttractive = true; // True for attraction, false for repulsion
    public float radius = 5f;

    [Header("Gravity")]
    public float gravityScaleOverride = 0.7f; 

    [Header("Impulse")]
    public Vector2 impulseDirection = Vector2.up; 

    [Header("Torque")]
    public float torqueAmount = 100f; 
    public int torqueDirection = 1; // 1 for clockwise, -1 for counter-clockwise

    /*
    [Header("Extras")]
    public int priority = 0;          
    public LayerMask affectedLayers;
    */
}
