using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier;

    // Returns the score multiplier for this landing pad
    public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }
}
