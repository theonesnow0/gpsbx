#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notificationDespawning : MonoBehaviour
{
    public bool isGeneral = true;
    void Start()
    {
        if (!isGeneral) { StartCoroutine(despawn()); }
        isGeneral = false;
    }
    public IEnumerator despawn() { yield return new WaitForSeconds(3.5f); Destroy(gameObject); }
}
