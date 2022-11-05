using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips_m;
    [SerializeField] private AudioClip[] clips_h;
    [SerializeField] private float max_time;
    [SerializeField] bool is_m;
    private void Start()
    {
        StartCoroutine(play());
    }



    IEnumerator play()
    {
        float randomTime = Random.Range(0.5f, max_time);
        int randomClip;
        if (is_m)
        {

            randomClip = Random.Range(0, clips_m.Length);
            audioSource.PlayOneShot(clips_m[randomClip]);
        }
        else
        {
            randomClip = Random.Range(0, clips_h.Length);
            audioSource.PlayOneShot(clips_h[randomClip]);
        }
        yield return new WaitWhile(() => audioSource.isPlaying);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(play());
    }

}
