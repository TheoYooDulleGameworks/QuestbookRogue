using UnityEngine;
using UnityEngine.UI;

public class CursorManager : Singleton<CursorManager>
{
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        Cursor.visible = false;

        if (Application.isPlaying)
            { Cursor.lockState = CursorLockMode.None; }
        else
            { Cursor.lockState = CursorLockMode.Confined; }
    }

    private void Update()
    {
        Vector2 cursorPos = Input.mousePosition;
        image.rectTransform.position = cursorPos;

        if (!Application.isPlaying) { return; }

        Cursor.visible = false;
    }
}