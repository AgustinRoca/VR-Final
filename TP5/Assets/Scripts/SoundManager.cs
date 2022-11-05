using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private float max_time;
    
    private void Start()
    {
        StartCoroutine(play());
    }



    IEnumerator play()
    {
        float randomTime = Random.Range(0.5f, max_time);
        int randomObject= Random.Range(0, objects.Length);
        Instantiate(objects[randomObject], new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(play());
    }
}
