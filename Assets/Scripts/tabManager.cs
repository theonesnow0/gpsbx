#nullable disable

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro; 

public class tabManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform content;
    private Dictionary<int, GameObject> playerListEntries = new Dictionary<int, GameObject>();

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToList(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemovePlayerFromList(otherPlayer);
    }

    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddPlayerToList(player);
        }
    }

    private void AddPlayerToList(Player player)
    {
        GameObject playerEntry = Instantiate(playerPrefab, content);
        playerEntry.GetComponentInChildren<TextMeshProUGUI>().text = player.NickName;
        playerListEntries.Add(player.ActorNumber, playerEntry);
    }

    private void RemovePlayerFromList(Player player)
    {
        if (playerListEntries.TryGetValue(player.ActorNumber, out GameObject playerEntry))
        {
            Destroy(playerEntry);
            playerListEntries.Remove(player.ActorNumber);
        }
    }
}
