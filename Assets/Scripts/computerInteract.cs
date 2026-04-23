using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerInteract : MonoBehaviour
{
    public GameObject screenUI;
    public float interactDistance = 3f;

    [SerializeField] private Camera playerCamera;
    private bool isUsing = false;
    public MouseLook mouseLook;

    void Start()
    {
        playerCamera = Camera.main;
        mouseLook = Camera.main.GetComponent<MouseLook>();
    }

    void Update()
    {
        if (!isUsing)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, interactDistance))
                {
                    if (hit.transform == transform)
                    {
                        OpenComputer();
                    }
                }
            }
        }
        else
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                CloseComputer();
            }
        }
    }

    void OpenComputer()
    {
        screenUI.SetActive(true);
        mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isUsing = true;
    }

    void CloseComputer()
    {
        screenUI.SetActive(false);
        mouseLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isUsing = false;
    }
}
