using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public GameObject Mike;
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Impacto");
        if (col.tag==Mike.tag)
        {
            Mike.GetComponent<MikeController>().vida -= 1;
        }
    }
}
