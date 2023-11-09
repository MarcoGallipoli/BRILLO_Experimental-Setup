using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material RedMaterial;
    public Material GreenMaterial;

    public void SetRed()
    {
        SetMaterial(RedMaterial);
    }

    public void SetGreen()
    {
        SetMaterial (GreenMaterial);
    }

    // This method determine the color of the cube after the use of the menu
    private void SetMaterial(Material newMaterial)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();  
        renderer.material = newMaterial;    
    }
}
