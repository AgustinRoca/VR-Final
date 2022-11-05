using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillThisMug : MonoBehaviour
{
    public void fill()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    public bool isFilled(){
        return this.GetComponent<MeshRenderer>().enabled;
    }
}
