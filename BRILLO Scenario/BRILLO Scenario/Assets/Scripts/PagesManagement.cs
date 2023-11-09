using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class PagesManagement : MonoBehaviour
{
    public List<GameObject> pages;
    private int active;
    // Start is called before the first frame update
    void Start()
    {
        pages[active].SetActive(true);
        int PagesLength = pages.Count;
        for (int i = 0; i < PagesLength; i++)
        {
            pages[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        pages[active].SetActive(true);
        int PagesLength = pages.Count;
        for (int i = 0; i < PagesLength; i++)
        {
            if (i!=active)
            pages[i].SetActive(false);
        }
    }

    public void TurnOn()
    {
        int PagesLength = pages.Count;
        if (active != PagesLength)
        active = active + 1;
    }

    public void TurnBack()
    {
        if (active != 0)
            active = active - 1;
        }
}
