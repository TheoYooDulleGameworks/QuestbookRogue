using UnityEngine;
using UnityEngine.EventSystems;

public class TempButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.parent.parent.GetComponent<SceneController>().TransitionToSelects();
    }
}
