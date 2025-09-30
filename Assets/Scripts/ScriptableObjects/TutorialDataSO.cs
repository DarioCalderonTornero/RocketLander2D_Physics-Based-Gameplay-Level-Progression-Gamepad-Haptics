using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial_ScriptableObject", menuName = "Tutorial/TutorialData")]
public class TutorialDataSO : ScriptableObject
{
    [TextArea]
    public string tutorialText;
}
