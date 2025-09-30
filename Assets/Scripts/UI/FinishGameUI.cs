using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        }); 
    }

    private void Start()
    {
        finalScoreText.text = "FINAL SCORE: " + GameManager.GetTotalScore().ToString();
        mainMenuButton.Select();
    }
}
