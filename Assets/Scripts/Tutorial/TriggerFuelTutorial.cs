using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerFuelTutorial : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI tutorialUpText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private Animator myAnimator;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            Destroy();
            Destroy(gameObject);
            continueButton.gameObject.SetActive(false);
        });
    }

    public void Interact(Lander lander)
    {
        myAnimator.SetBool("IsFuelTutorial", true);
        Invoke(nameof(StopGame), 1f);
        continueButton.Select();
    }

    private void StopGame()
    {
        Time.timeScale = 0.0f;
    }

    private void Destroy()
    {
        Destroy(tutorialUpText);
        Destroy(backgroundImage);
        continueButton.gameObject.SetActive(false);
    }
}
