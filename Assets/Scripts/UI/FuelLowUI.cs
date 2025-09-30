using System;
using UnityEditor.Search;
using UnityEngine;

public class FuelLowUI : MonoBehaviour
{
    [SerializeField] private GameObject container;

    public event EventHandler OnLowFuelAmount;

    private void Start()
    {
        Hide();
        Lander.Instance.OnLowFuel += Lander_OnLowFuel;
        Lander.Instance.OnHighFuel += Lander_OnHighFuel;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.LandedEventArgs e)
    {
        switch(e.landingType)
        {
            case Lander.LandingType.Success:
                Hide();
                break;
            case Lander.LandingType.TooFastLanding:
                Hide();
                break;
            case Lander.LandingType.TooSteepAngle:
                Hide();
                break;
            case Lander.LandingType.WrongLandingArea:
                Hide();
                break;
        }
    }

    private void Lander_OnLowFuel(object sender, EventArgs e)
    {
        Show();
    }

    private void Lander_OnHighFuel(object sender, EventArgs e)
    {
        Hide();
    }

    private void Hide()
    {
        if (container.gameObject.activeSelf)   
            container.gameObject.SetActive(false);
    }

    private void Show()
    {
        if (!container.gameObject.activeSelf)
            container.gameObject.SetActive(true);
    }
}
