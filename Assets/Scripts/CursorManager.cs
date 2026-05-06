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
        HandleCameraStateChange();
        if (!IsPcCameraActive())
            return;

        UpdateCursorVisibility();
        UpdateFakeCursorPosition();
    }
    // This method checks for changes in the active state of the PC camera and applies the appropriate cursor state when a change is detected.
    private void HandleCameraStateChange()
    {
        bool pcCameraActive = IsPcCameraActive();
        if (pcCameraActive != lastCameraState)
        {
            ApplyCursorState(pcCameraActive);
            lastCameraState = pcCameraActive;
        }
    }
    // This method checks if the PC camera is currently active in the scene. It returns true if the camera is assigned and active, and false otherwise.
    private bool IsPcCameraActive()
    {
        return pcCamera != null && pcCamera.activeInHierarchy;
    }
    // This method updates the visibility of the fake cursor and the system cursor based on whether the UI panel is open or not.
    private void UpdateCursorVisibility()
    {
        bool panelOpen = panel.activeSelf;
        bool uiMode = panelOpen;

        fakeCursor.gameObject.SetActive(uiMode);
        Cursor.visible = !uiMode;
    }
    // This method updates the position of the fake cursor to match the mouse position, while ensuring it stays within the bounds of the canvas.
    private void UpdateFakeCursorPosition()
    {
        if (!IsUiMode() || Mouse.current == null)
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
        storedMousePos = mousePos;
    }
    // This method checks if the UI panel is currently active, which indicates whether the player is in UI mode or not.
    private bool IsUiMode()
    {
        return panel.activeSelf;
    }
    //This method applies the appropriate cursor state based on whether the PC camera is active or not. 
    //If the PC camera is inactive, it locks and hides the cursor. 
    //If the PC camera is active, it unlocks the cursor and restores its last known position 
    // to prevent any "jumping" effect when switching back to UI mode.
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