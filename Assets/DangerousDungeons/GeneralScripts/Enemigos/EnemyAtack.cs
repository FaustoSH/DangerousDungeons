using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    private GameObject Mike;
    private void Start()
    {
        Mike = GameObject.Find("Mike");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag==Mike.tag)
        {
            if(!Mike.GetComponent<MikeController>().invulnerabilidad && gameObject.GetComponentInParent<Animator>().GetBool("Ataque"))
            {
                Mike.GetComponent<MikeController>().vida -= 1;
            }

        }
    }
}
