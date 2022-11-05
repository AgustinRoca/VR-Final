using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    Color offColor = new Color(39f/255f, 45f/255f, 44f/255f);
    Color onColor = new Color(54f/255f, 105f/255f, 214f/255f);
    Renderer renderer;
    AudioSource audioSource;
    bool on = true;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        audioSource = transform.parent.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void switchOnOff(){
        
        audioSource.mute = !audioSource.mute;
        if(on){
            renderer.materials[1].SetColor("_Color", offColor);
            on = false;
            
        } else {
            renderer.materials[1].SetColor("_Color", onColor);
            on = true;
        }
    }
}
