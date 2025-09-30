using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreMultiplierText;
    [SerializeField] private SpriteRenderer backgroundSprite;

    
    private void Awake()
    {
        // Get the LandingPad component and set the score multiplier text
        LandingPad landingPad = GetComponent<LandingPad>(); 
        scoreMultiplierText.text = "x" + landingPad.GetScoreMultiplier();   
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            this.backgroundSprite.color = Color.green;
        }
    }
}
