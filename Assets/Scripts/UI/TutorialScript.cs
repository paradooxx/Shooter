using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private bool conditionMet = false;
    [SerializeField] private RectTransform uiElement;

    private void Update()
    {
        if (!conditionMet)
        {
            Vector3 currentPosition = uiElement.anchoredPosition;
            float targetX = Mathf.PingPong(Time.time * 500f, 600) - 300;
            uiElement.anchoredPosition = new Vector2(targetX, currentPosition.y);
        }
        else if (conditionMet)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        conditionMet = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        conditionMet = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        conditionMet = true;
    }
}
