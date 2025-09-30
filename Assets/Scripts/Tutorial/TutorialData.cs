using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialData : MonoBehaviour, IInteractable
{

    [SerializeField] private Button continueButton;
    [SerializeField] private Image backgroundSpriteImage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Animator myAnimator;
    //[SerializeField] private GameObject thisGameObject;

    [SerializeField] private TutorialDataSO tutorialDataSO;

    private bool hasEntered = false;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            DestroySelf();
            ResumeTime();
            myAnimator.SetBool("IsTutorialOn", false);
        });
    }

    public void Interact(Lander lander)
    {
        if (!hasEntered)
        {
            tutorialText.text = tutorialDataSO.tutorialText;

            myAnimator.SetBool("IsTutorialOn", true);

            Invoke(nameof(StopTime), 1f);

            continueButton.Select();

            hasEntered = true;
        }
       
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    private void DestroySelf()
    {
        //Destroy(gameObject);  
    }

}
