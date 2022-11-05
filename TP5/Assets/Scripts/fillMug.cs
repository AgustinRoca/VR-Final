using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillMug : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject faucet;
    [SerializeField] private float maxCastDistance;
    private int layerMask;
    GameObject mug;

    private void Start()
    {
        maxCastDistance = 0.2f;
        layerMask = (1<<7);
    }
    public void fill()
    {
        StartCoroutine(fillE());
    }

    // Update is called once per frame
    IEnumerator fillE()
    {
        RaycastHit hit;
        audioSource.PlayOneShot(clip);

        yield return new WaitForSeconds(5);

        bool mugPresent = Physics.Raycast(faucet.transform.position, new Vector3(0,-1, 0), out hit, maxCastDistance, layerMask);
        //print("is mug present?");
        if (mugPresent)
        {
            //print("mug present");
            mug = hit.collider.gameObject;
            mug.GetComponent<fillThisMug>().fill();
        }
    }

    
}
