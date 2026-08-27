#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorsEquals : MonoBehaviour
{
    public Animator defaultanim;
    public Animator clonedanim;
    void Update()
    {
        clonedanim.SetBool("isWalk", defaultanim.GetBool("isWalk"));
        clonedanim.SetBool("sillyDance", defaultanim.GetBool("sillyDance"));
        clonedanim.SetBool("falling", defaultanim.GetBool("falling"));
        clonedanim.SetBool("jumped", defaultanim.GetBool("jumped"));
        clonedanim.SetBool("running", defaultanim.GetBool("running"));
        clonedanim.SetBool("crouch", defaultanim.GetBool("crouch"));
        clonedanim.SetBool("revolverShoot", defaultanim.GetBool("revolverShoot"));
        clonedanim.SetBool("osmotrRevolver", defaultanim.GetBool("osmotrRevolver"));
        clonedanim.SetBool("reloadRevolver", defaultanim.GetBool("reloadRevolver"));
        clonedanim.SetBool("cubegunEmpty", defaultanim.GetBool("cubegunEmpty"));
        clonedanim.SetBool("cubegunReadyToRemove", defaultanim.GetBool("cubegunReadyToRemove"));
        clonedanim.SetLayerWeight(1, defaultanim.GetLayerWeight(1));
        clonedanim.SetLayerWeight(2, defaultanim.GetLayerWeight(2));
        clonedanim.SetLayerWeight(3, defaultanim.GetLayerWeight(3));
        clonedanim.SetLayerWeight(4, defaultanim.GetLayerWeight(4));
    }

}
