using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private float seperation_time;
    bool walking;
    bool is_m;
    private void Start()
    {
        walking = true;
        StartCoroutine(play());
    }


    IEnumerator play()
    {
        
        audioSource.PlayOneShot(stepClip);
        
        yield return new WaitWhile(() => audioSource.isPlaying);
        yield return new WaitForSeconds(seperation_time);
        walking = gameObject.GetComponent<Animator>().GetInteger("state") == 0;
        if (walking)
        {
            StartCoroutine(play());
        }
        
    }

}
