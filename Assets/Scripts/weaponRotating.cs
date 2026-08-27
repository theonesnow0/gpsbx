#nullable disable

using System.Collections;
using UnityEngine;

public class weaponRotating : MonoBehaviour
{
    public AudioSource source;
    public float rotateSpeed = 1f;
    public BoxCollider trigger;
    public GameObject normal;
    public GameObject transparency;
    public string cubeMaterial;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        trigger = GetComponent<BoxCollider>();
        normal.SetActive(true); transparency.SetActive(false);
    }
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { trigger.enabled = false; normal.SetActive(false); transparency.SetActive(true); StartCoroutine(activate()); source.PlayOneShot(source.clip);  }
    }
    public IEnumerator activate() { yield return new WaitForSeconds(5); trigger.enabled = true; normal.SetActive(true); transparency.SetActive(false); }
}
