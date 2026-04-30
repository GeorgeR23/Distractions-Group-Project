using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerInteract : MonoBehaviour
{
    public GameObject screenUI;
    public float interactDistance = 3f;

    [SerializeField] private Camera PlayerCamera;
    private bool isUsing = false;

    void Start()
    {
        screenUI.SetActive(true);
        Debug.Log("ComputerInteract initialized. Camera assigned: " + PlayerCamera);
    }

    void Update()
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
                    Debug.Log("Raycast hit: " + hit.transform.name);
                    Debug.Log("Hit: " + hit.transform.name);
                    Debug.Log("This object: " + transform.name);

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

        if (screenUI == null)
        {
            Debug.LogError("screenUI is NOT assigned!");
            return;
        }

        screenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isUsing = true;
    }

    void CloseComputer()
    {
        Debug.Log("Closing computer UI");

        if (screenUI == null)
        {
            Debug.LogError("screenUI is NOT assigned!");
            return;
        }

        screenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isUsing = false;
    }
}