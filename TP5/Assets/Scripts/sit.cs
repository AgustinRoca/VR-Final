using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sit : MonoBehaviour
{
    [SerializeField]private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0f;
        anim.Play("sit", 0, 1f);

    }

}
