using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AchievementObject : MonoBehaviour, IPointerDownHandler
{
    public Achievement achievement;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    // This method is called when the user clicks on the UI element
    public void OnPointerDown(PointerEventData eventData)
    {
        nameText.text = achievement.achievementName;
        descriptionText.text = achievement.achievementDescription;
    }
}
