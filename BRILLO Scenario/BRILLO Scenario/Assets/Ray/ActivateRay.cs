using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRay : MonoBehaviour
{
    public GameObject Ray;
    public GameObject Dot;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        Ray.GetComponent<LineRenderer>().enabled = false;
        Dot.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Control()
    {
        
            if (active == false)
            {
                active = true;
                Ray.GetComponent<LineRenderer>().enabled = active;
                Dot.GetComponent<MeshRenderer>().enabled = active;
            }
            else
            {
                active = false;
                Ray.GetComponent<LineRenderer>().enabled = active;
                Dot.GetComponent<MeshRenderer>().enabled = active;
            }
        
    }
}
