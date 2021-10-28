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
        Debug.Log(animator.GetInteger("Velocidad")+"-"+animator.GetInteger("Direccion"));
        Vector3 position;
        Quaternion quaternion;
        float giro=0;
        position = new Vector3(0, 0, 0);
        quaternion = transform.rotation;
        if (Input.GetKey(KeyCode.W))
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                position.z = 4 * Time.deltaTime;
                animator.SetInteger("Velocidad", 2);
                if (Input.GetKey(KeyCode.D))
                {
                    giro = (180) * Time.deltaTime;
                    animator.SetInteger("Direccion", 2);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    giro = (-180) * Time.deltaTime;
                    animator.SetInteger("Direccion", 1);
                }
                else
                {
                    animator.SetInteger("Direccion", 0);
                }
            }
            else
            {
                position.z = 2 * Time.deltaTime;
                animator.SetInteger("Velocidad", 1);
                if (Input.GetKey(KeyCode.D))
                {
                    giro = (90) * Time.deltaTime;
                    animator.SetInteger("Direccion", 2);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    giro = (-90) * Time.deltaTime;
                    animator.SetInteger("Direccion", 1);
                    
                }
                else
                {
                    animator.SetInteger("Direccion", 0);
                }
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            animator.SetInteger("Velocidad", 0);
            animator.SetInteger("Direccion", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("Velocidad", 0);
            animator.SetInteger("Direccion", 2);
        }
        else if (Input.GetKey(KeyCode.S))
        {
                animator.SetInteger("Velocidad", 0);
                animator.SetInteger("Direccion", 3);
        }else
        {
            animator.SetInteger("Velocidad", 0);
            animator.SetInteger("Direccion", 0);
        }
        transform.position += quaternion * position;
        quaternion *= Quaternion.Euler(0, giro, 0);
        transform.rotation = quaternion;

    }
}
