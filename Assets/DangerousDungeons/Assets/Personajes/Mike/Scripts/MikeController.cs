using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeController : MonoBehaviour
{
    private Animator animator;
    public GameObject Espada;
    public GameObject Escudo;
    public GameObject Hacha;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();//Eliminar esta linea cuando se vaya a exportar el juego
        animator= GetComponent<Animator>();
        if (!PlayerPrefs.HasKey("Espada"))
        {
            PlayerPrefs.SetInt("Espada", 0);
        }
        if (!PlayerPrefs.HasKey("Hacha"))
        {
            PlayerPrefs.SetInt("Hacha", 0);
        }
        if (!PlayerPrefs.HasKey("Inventario"))
        {
            PlayerPrefs.SetInt("Inventario", 0);
        }else
        {
            switch (PlayerPrefs.GetInt("Inventario"))
            {
                case 0:
                    Espada.SetActive(false);
                    Escudo.SetActive(false);
                    Hacha.SetActive(false);
                break;
                case 1:
                    Hacha.SetActive(false);
                    if(PlayerPrefs.GetInt("Espada")==1)
                    {
                        Espada.SetActive(true);
                        Escudo.SetActive(true);
                    }else
                    {
                        PlayerPrefs.SetInt("Inventario", 0);
                    }
                break;
                case 2:
                    Espada.SetActive(false);
                    Escudo.SetActive(false);
                    if (PlayerPrefs.GetInt("Hacha") == 1)
                    {
                        Hacha.SetActive(true);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Inventario", 0);
                    }
                break;
                default:
                    PlayerPrefs.SetInt("Inventario", 0);
                break;
            }
        }

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

        if (Input.GetKey(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("Inventario", 0);
            Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("Inventario", 1);
            Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("Inventario", 2);
            Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
        }

        switch (PlayerPrefs.GetInt("Inventario"))
        {
            case 0:
                Espada.SetActive(false);
                Escudo.SetActive(false);
                Hacha.SetActive(false);
                break;
            case 1:
                Hacha.SetActive(false);
                if (PlayerPrefs.GetInt("Espada") == 1)
                {
                    Espada.SetActive(true);
                    Escudo.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetInt("Inventario", 0);
                }
                break;
            case 2:
                Espada.SetActive(false);
                Escudo.SetActive(false);
                if (PlayerPrefs.GetInt("Hacha") == 1)
                {
                    Hacha.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetInt("Inventario", 0);
                }
                break;
            default:
                PlayerPrefs.SetInt("Inventario", 0);
                break;
        }
    }
}
