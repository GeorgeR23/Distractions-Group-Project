using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public void OpenPanel(int index)
    {
        foreach (GameObject panel in panels)
            panel.SetActive(false);

        panels[index].SetActive(true);
    }
}