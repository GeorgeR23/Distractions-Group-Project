using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerInteract : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject[] panels;

    [Header("Settings")]
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private float interactDistance = 3f;

    private bool isUsing = false;
    private int currentPanel = 0;

    void Start()
    {
        // Turn all panels off at start
        foreach (GameObject panel in panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        Debug.Log("ComputerInteract initialized. Camera assigned: " + PlayerCamera);
    }

    void Update()
    {
        IsPlayerLookingAtMonitor();
    }

    private void IsPlayerLookingAtMonitor()
    {
        if (Camera.main != PlayerCamera)
            return;

        if (!isUsing)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Left click detected");

                Ray ray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 1f);

                if (Physics.Raycast(ray, out hit, interactDistance))
                {
                    if (hit.transform == transform || hit.transform.IsChildOf(transform))
                    {
                        Debug.Log("Hit this computer. Opening...");
                        OpenComputer();
                    }
                }
            }
        }
        else
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Debug.Log("Right click detected. Closing computer...");
                CloseComputer();
            }
        }
    }

    void OpenComputer()
    {
        Debug.Log("Opening computer UI");

        if (panels.Length == 0)
        {
            Debug.LogError("No panels assigned!");
            return;
        }

        ShowPanel(0); // open first panel by default

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isUsing = true;
    }

    void CloseComputer()
    {
        Debug.Log("Closing computer UI");

        foreach (GameObject panel in panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isUsing = false;
    }

    // Call this from buttons
    public void ShowPanel(int panelIndex)
    {
        if (panelIndex < 0 || panelIndex >= panels.Length)
        {
            Debug.LogWarning("Invalid panel index");
            return;
        }

        // turn all off
        foreach (GameObject panel in panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        // turn selected on
        panels[panelIndex].SetActive(true);
        currentPanel = panelIndex;

        Debug.Log("Switched to panel " + panelIndex);
    }
}