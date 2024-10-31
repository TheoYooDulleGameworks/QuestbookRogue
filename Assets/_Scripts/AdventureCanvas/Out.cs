using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Out : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform questSelectsScene;
    [SerializeField] private RectTransform questContentsScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!questSelectsScene.gameObject.activeSelf)
        {
            questContentsScene.gameObject.SetActive(false);
            questSelectsScene.gameObject.SetActive(true);
        }
    }
}
