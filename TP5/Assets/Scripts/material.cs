using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private Material currMaterial;
    // Start is called before the first frame update
    void Start()
    {
        currMaterial = materials[0];
    }

    public void changeMaterial(int indexMaterial)
    {
       // print(materials[indexMaterial].name);
        if (materials.Length > indexMaterial)
        {
            currMaterial = materials[indexMaterial];
            gameObject.GetComponent<MeshRenderer>().material = currMaterial;
        }
       
    }
}
