using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida;
    private Animator animator;
    private float tiempoMuerto;
    private int vidaAnterior;
    private GameObject Mike;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        //La inicialización de la vida se hace mediante la interfaz ya que los diferentes enemigos tendrán diferentes vidas iniciales
        animator = this.gameObject.GetComponent<Animator>();
        tiempoMuerto = 0;
        vidaAnterior = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Mike = GameObject.Find("Mike");
    }

    private void Update()
    {
        animator.SetInteger("Vida", vida);
        if (vida <= 0)
        {
            tiempoMuerto += 1.0f * Time.deltaTime;
            if(tiempoMuerto>=7)
            {
                //Después de 7 segundos muerto se elimina al enemigo para que no gaste memoria
                Destroy(this.gameObject);
            }
        }else if(vida<vidaAnterior)
        {
            animator.SetTrigger("Golpe");
            vidaAnterior = vida;
        }else
        {
            navMeshAgent.destination = Mike.transform.position;
            if(navMeshAgent.remainingDistance<=1.1 && navMeshAgent.pathStatus==NavMeshPathStatus.PathComplete && navMeshAgent.remainingDistance!=Mathf.Infinity)
            {
                transform.LookAt(Mike.transform.position);
                animator.SetBool("Andar", false);
                animator.SetBool("Ataque", true);
            }
            else
            {
                animator.SetBool("Andar", true);
                animator.SetBool("Ataque", false);
            }
               
        }
    }
}
