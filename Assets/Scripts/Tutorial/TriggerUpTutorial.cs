using TMPro;
using UnityEngine;

public class TriggerUpTutorial : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshPro tutorialUpText;
    public void Interact(Lander lander)
    {
        Destroy(tutorialUpText);
    }

}
