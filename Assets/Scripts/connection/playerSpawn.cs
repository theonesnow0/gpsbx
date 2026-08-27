#nullable disable

using UnityEngine;
using Photon.Pun;

public class playerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public bool isGeneral;
    void Start()
    {
        if (isGeneral) { PhotonNetwork.Instantiate(player.name, spawn.transform.position, spawn.transform.rotation); }
    }
}