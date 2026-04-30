using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private RectTransform fakeCursor;
    [SerializeField] private Canvas canvas;

    [Header("Camera")]
    [SerializeField] private GameObject pcCamera;

    private bool lastCameraState;
    private Vector2 storedMousePos;

    void Start()
    {
        lastCameraState = pcCamera != null && pcCamera.activeInHierarchy;

        ApplyCursorState(lastCameraState);

        if (fakeCursor != null)
            fakeCursor.gameObject.SetActive(false);
    }

    void Update()
    {
        bool pcCameraActive = pcCamera != null && pcCamera.activeInHierarchy;

        // Only run logic if camera state changes
        if (pcCameraActive != lastCameraState)
        {
            ApplyCursorState(pcCameraActive);
            lastCameraState = pcCameraActive;
        }

        if (!pcCameraActive)
            return;

        bool panelOpen = panel.activeSelf;
        bool uiMode = panelOpen;

        fakeCursor.gameObject.SetActive(uiMode);
        Cursor.visible = !uiMode;

        if (!uiMode || Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        localPoint.x = Mathf.Clamp(localPoint.x, canvasRect.rect.xMin, canvasRect.rect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, canvasRect.rect.yMin, canvasRect.rect.yMax);

        fakeCursor.localPosition = localPoint;

        // store position for switching back
        storedMousePos = mousePos;
    }

    void ApplyCursorState(bool cameraActive)
    {
        if (!cameraActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }

        Cursor.lockState = CursorLockMode.None;

        // restore last known position (prevents "jump")
        Mouse.current.WarpCursorPosition(storedMousePos);
    }
}