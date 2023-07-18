using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsMenu : MonoBehaviour
{
    [SerializeField]
    private Panel[] _panels;

    public void OpenPanel(int index)
    {
        ClosePanels();
        _panels[index].OpenPanel();
    }
    public void OpenPanel(Panel panel)
    {
        ClosePanels();
        panel.OpenPanel();
    }
    private void ClosePanels()
    {
        foreach(Panel panel in _panels)
        {
            panel.ClosePanel();
        }
    }
}
