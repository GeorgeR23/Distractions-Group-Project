using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    void Start()
    {
        Debug.Log("CameraSwitch started");

        if (cam1 == null || cam2 == null)
        {
            Debug.LogError("One or both cameras are not assigned!");
            return;
        }
        // Ensure playercamera starts active and pccamera starts inactive
        cam1.SetActive(true);
        cam2.SetActive(false);

        Debug.Log("Cam1 active: " + cam1.activeSelf);
        Debug.Log("Cam2 active: " + cam2.activeSelf);
    }

    void Update()
    {
        switchCamera();
    }
    
    //This method checks for the F key press and toggles the active state of the two cameras.
    //It also includes debug logs to verify the state of the cameras before and after the switch.
    void switchCamera()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("F pressed - switching cameras");

            bool cam1Active = cam1.activeSelf;

            Debug.Log("Before switch:");
            Debug.Log("Cam1 active: " + cam1.activeSelf);
            Debug.Log("Cam2 active: " + cam2.activeSelf);

            cam1.SetActive(!cam1Active);
            cam2.SetActive(cam1Active);

            Debug.Log("After switch:");
            Debug.Log("Cam1 active: " + cam1.activeSelf);
            Debug.Log("Cam2 active: " + cam2.activeSelf);
        }}
}