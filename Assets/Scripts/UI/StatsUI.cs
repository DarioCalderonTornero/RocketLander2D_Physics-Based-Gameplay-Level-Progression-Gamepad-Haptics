using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private GameObject speedLeftArrowGameObject;
    [SerializeField] private GameObject speedRightArrowGameObject;
    [SerializeField] private GameObject speedUpArrowGameObject;
    [SerializeField] private GameObject speedDownArrowGameObject;
    [SerializeField] private Image fuelBarImage;

    private void Update()
    {
        UpdateStatsText();  
    }

    private void UpdateStatsText()
    {
        speedLeftArrowGameObject.SetActive(Lander.Instance.GetSpeedX() < 0f);
        speedRightArrowGameObject.SetActive(Lander.Instance.GetSpeedX() >= 0f);
        speedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0f);
        speedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0f);

        fuelBarImage.fillAmount = Lander.Instance.GetFuelNormalized();

        statsText.text = GameManager.Instance.GetLevelNumber() + "\n" +
        GameManager.Instance.GetScore() + "\n" +
        GameManager.Instance.GetTime().ToString("F0") + " \n" +
        Lander.Instance.GetSpeedX().ToString("F1") + "\n" +
        Lander.Instance.GetSpeedY().ToString("F1");
    }
}
