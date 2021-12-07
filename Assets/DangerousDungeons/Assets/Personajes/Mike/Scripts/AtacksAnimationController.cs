/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código evita que el animator se pueda quedar atascado en un estado intermedio a la hora de atacar.
                consiste en mantener el estado de atacando hasta el final del ataque. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtacksAnimationController : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Ataque", -1); //Con esto se evita que los estados de las animaciones se queden enganchados 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Ataque", -1); //Con esto se evita que los estados de las animaciones se queden enganchados 
    }

}
