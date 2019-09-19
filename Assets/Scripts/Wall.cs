using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    bool transparent = false;

    public void BecomeTransparent() {
        transparent = true;
    }

    void Update()
    {
        if (transparent)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else {
            GetComponent<MeshRenderer>().enabled = true;
        }
        
        transparent = false;
    }
}
