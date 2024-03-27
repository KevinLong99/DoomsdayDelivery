using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueBox;
    public GameObject OrderBox;
    public GameObject TutorialBox;
    public GameObject DeliveryResultBox;

    private bool isFirstFrame = true;

    void Update()
    {
    }

    public void DialogueBoxOn() => DialogueBox.SetActive(true);
    public void DialogueBoxOff() => DialogueBox.SetActive(false);

    public void OrderBoxOn() => OrderBox.SetActive(true);
    public void OrderBoxOff() => OrderBox.SetActive(false);

    public void TutorialBoxOn() => TutorialBox.SetActive(true);
    public void TutorialBoxOff() => TutorialBox.SetActive(false);

    public void DeliveryResultBoxOn() => DeliveryResultBox.SetActive(true);
    public void DeliveryResultBoxOff() => DeliveryResultBox.SetActive(false);
}