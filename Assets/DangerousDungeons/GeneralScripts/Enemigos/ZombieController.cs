using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida;
    private Animator animator;
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        vida = 10;    
    }

    private void Update()
    {
        animator.SetInteger("Vida", vida);
        if(vida<=0)
        {
            //Destroy(this.gameObject);
        }
    }
}
