using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetInteger("Velocidad", 2);
            }
            else
            {
                animator.SetInteger("Velocidad", 1);
            }
        }
        else 
        {
            animator.SetInteger("Velocidad", 0);
        }
    }
}
