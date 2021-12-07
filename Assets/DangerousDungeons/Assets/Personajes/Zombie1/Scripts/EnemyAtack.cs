/*
    Autor: Fausto S�nchez Hoya
    Descripci�n: este c�digo se encarga aplicar el da�o a Mike cuando la mano del zombie impacta contra �l
 */


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
            //Se debe comprobar que el objeto impactado es en efecto Mike y que se estaba atacando
            if(!Mike.GetComponent<MikeController>().invulnerabilidad && gameObject.GetComponentInParent<Animator>().GetBool("Ataque") && gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Atacando"))
            {
                gameObject.GetComponent<AudioSource>().Play();
                Mike.GetComponent<MikeController>().vida -= 1;
            }

        }
    }
}
