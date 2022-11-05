using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public Animator animator;
    private bool serve1 = false;
    private bool serve2 = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(serve1){
            StartCoroutine(serving1());
        }
        if(serve2){
            StartCoroutine(serving2());
        }
    }

    public void pullLever(int i){
        if (i == 1){
            animator.SetInteger("state1", 1);
            serve1 = true;
        } else if (i == 2){
            animator.SetInteger("state2", 1);
            serve2 = true;
        }
    }

    IEnumerator serving1()
    {
        yield return new WaitForSeconds(5f);
        serve1 = false;
        animator.SetInteger("state1", 0);
    }

    IEnumerator serving2()
    {
        yield return new WaitForSeconds(5f);
        serve2 = false;
        animator.SetInteger("state2", 0);
    }
}
