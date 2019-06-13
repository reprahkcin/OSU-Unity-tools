using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public int activePanel = 0; // Default array index. Starting page or object should be in this position in the array below. 
    public List<GameObject> panels; // Populate in inspector (you can use panels, or any other thing you want to cycle through)

    void Start()
    {
        SwapPanels(activePanel); // Starts off turning on visibility for whatever is at index "activeThing" 
    }

    public void Forward() // Call with Next Button
    {
        activePanel++;
        if (activePanel > panels.Count) { activePanel = 0; } // If you're at the end of the array, it will cycle back to the beginning.
        SwapPanels(activePanel);
    }

    public void Backward() // Call with Previous Button
    {
        activePanel--;
        if (activePanel < 0) { activePanel = 0; } // If you're at the beginning of the array, you can't go back
        // if (activePanel < 0){activePanel = panels.Count;}  // This will make the array cycle both ways. 
        SwapPanels(activePanel);
    }

    public void SwapPanels(int i)
    {
        foreach (GameObject panel in panels)  // Turns all things off to start...
        {
            panel.gameObject.SetActive(false);
        }
        panels[i].gameObject.SetActive(true); // ...then turns on the one in the index passed above as int i
    }

    public void AddToPanels(GameObject panel)
    {
        panels.Add(panel);
    }

    public void ListCleanup()
    {
        panels.RemoveAll(GameObject => GameObject == null);
        foreach (GameObject item in panels)
        {
            item.name = "panel_" + panels.IndexOf(item);
        }
    }
}
