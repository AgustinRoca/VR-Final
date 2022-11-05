using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inFrontCamara : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = camera.transform.position + camera.transform.forward * 0.05f;
    }
}
