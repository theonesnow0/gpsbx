#nullable disable

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDespawn : MonoBehaviourPun
{
    public rotateScript scrpt;
    public float timeToDestroy = 5f;
    public bool isCubegnunBullet;
    public byte bulletSpeed = 50;
    public float gridSize = 1f;
    public bool needDestroy = true;
    public AudioSource globalAudioSource;
    public AudioSource audioSource;
    public GameObject globalSound;
    public Rigidbody rb;
    PhotonView view;

    public bool kickBullet;
    public bool banBullet;
    void Start()
    {
        view = GetComponent<PhotonView>();
        globalSound = GameObject.FindWithTag("globalSounds");
        globalAudioSource = globalSound.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(despawn());
        rb.linearVelocity = gameObject.transform.forward * bulletSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && view.IsMine && !isCubegnunBullet) 
        {
            PlayerController target = collision.gameObject.GetComponent<PlayerController>();
            target.photonView.RPC("TakeDamageRevolver", RpcTarget.All);
            globalAudioSource.PlayOneShot(globalAudioSource.clip);
            PhotonNetwork.Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player") && view.IsMine && !isCubegnunBullet && kickBullet)
        {
            PlayerController target = collision.gameObject.GetComponent<PlayerController>();
            target.photonView.RPC("KickPlayerRPC", RpcTarget.All, PlayerController.reasonToGive);
            globalAudioSource.PlayOneShot(globalAudioSource.clip);
            PhotonNetwork.Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Player") && view.IsMine && !isCubegnunBullet && banBullet)
        {
            PlayerController target = collision.gameObject.GetComponent<PlayerController>();
            target.photonView.RPC("BanPlayerRPC", RpcTarget.All, PlayerController.reasonToGive);
            globalAudioSource.PlayOneShot(globalAudioSource.clip);
            PhotonNetwork.Destroy(gameObject);
        }
        if (isCubegnunBullet && needDestroy)
        {
            scrpt.enabled = false;
            needDestroy = false;
            rb.linearVelocity = gameObject.transform.forward * 0;
            rb.isKinematic = true;
            Vector3 currentPosition = transform.position;

            float newX = Mathf.Round(currentPosition.x / gridSize) * gridSize;
            float newY = Mathf.Round(currentPosition.y / gridSize) * gridSize;
            float newZ = Mathf.Round(currentPosition.z / gridSize) * gridSize;

            //newX -= .5f;
            //newY -= .5f;
            //newZ -= .5f;

            transform.position = new Vector3(newX, newY, newZ);

            transform.rotation = Quaternion.Euler(0, 0, 0);
            audioSource.PlayOneShot(audioSource.clip);

        }
        if (!isCubegnunBullet && view.IsMine && !collision.collider.CompareTag("Player")) { PhotonNetwork.Destroy(gameObject); }
        if (!isCubegnunBullet && !view.IsMine && !collision.collider.CompareTag("Player")) { photonView.RPC("RequestDestroyObject", RpcTarget.All); gameObject.SetActive(false); }
    }
    public IEnumerator despawn()
    {
        yield return new WaitForSeconds(timeToDestroy);
        if (needDestroy) { PhotonNetwork.Destroy(gameObject); }
    }
    [PunRPC]
    public void RequestDestroyObject(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null && view.IsMine)
        {
            PhotonNetwork.Destroy(view.gameObject);
        }
    }
}
