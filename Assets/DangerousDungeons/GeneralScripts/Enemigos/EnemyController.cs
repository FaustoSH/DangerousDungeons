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
    private float enfriamiento;
    private int vidaAnterior;
    private GameObject Mike;
    private NavMeshAgent navMeshAgent;
    private bool muerteNotificada;
    public AudioClip quejarse;
    void Start()
    {
        //La inicialización de la vida se hace mediante la interfaz ya que los diferentes enemigos tendrán diferentes vidas iniciales
        animator = this.gameObject.GetComponent<Animator>();
        tiempoMuerto = 0;
        vidaAnterior = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Mike = GameObject.Find("Mike");
        enfriamiento = 1.0f;
    }

    private void Update()
    {
        animator.SetInteger("Vida", vida);
        if (vida <= 0)
        {

            tiempoMuerto += 1.0f * Time.deltaTime;
            if (!muerteNotificada)
            {
                Mike.GetComponent<MikeController>().zombiesMuertos++;
                Debug.Log("ZombiesMuertos-->" + Mike.GetComponent<MikeController>().zombiesMuertos);
                muerteNotificada = true;
            }

            if (tiempoMuerto>=7)
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
                enfriamiento -= 1.0f * Time.deltaTime;
                if (enfriamiento <= 0)
                {
                    gameObject.GetComponent<AudioSource>().Pause();
                    animator.SetBool("Ataque", true);
                    enfriamiento = 1.0f;
                }
                

            }
            else
            {
                //if (gameObject.GetComponent<AudioSource>().clip != quejarse)
                //{
                //    gameObject.GetComponent<AudioSource>().clip = quejarse;
                //    gameObject.GetComponent<AudioSource>().Play();
                //}
                if(!gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    gameObject.GetComponent<AudioSource>().clip = quejarse;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                animator.SetBool("Andar", true);
                animator.SetBool("Ataque", false);
            }
               
        }
    }
}
