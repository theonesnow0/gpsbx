#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsPlay : MonoBehaviour
{
    public bool neeedParticle;
    public ParticleSystem dust;
    public AudioSource footsteps;
    void OnEnable() 
    { 
        footsteps.PlayOneShot(footsteps.clip); 
        if (neeedParticle) { dust.Play(); }
    }
}
