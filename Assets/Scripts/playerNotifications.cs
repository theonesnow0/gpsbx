#nullable disable

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerNotifications : MonoBehaviourPunCallbacks
{
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;

    public Vector3[] slotPositions;

    public GameObject notification;
    public TextMeshProUGUI notifText;
    public Image notifImage;

    public TextMeshProUGUI playerCountText;
    public void FixedUpdate()
    {
        GameObject[] notifications = GameObject.FindGameObjectsWithTag("notification");

        slotPositions[0] = notification.transform.position;
        slotPositions[1] = slot1.transform.position;
        slotPositions[2] = slot2.transform.position;
        slotPositions[3] = slot3.transform.position;

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        playerCountText.text = playerCount.ToString();

        for (int i = 0; i < notifications.Length; i++)
        {
            if (i < slotPositions.Length)
            {
                notifications[i].transform.position = slotPositions[i];
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        string newPlayerNickname = newPlayer.NickName;
        notifText.text = newPlayerNickname + " ������������� � ����!";
        Instantiate(notification, notification.transform);
    }
    public override void OnPlayerLeftRoom(Player leftedPlayer)
    {
        base.OnPlayerLeftRoom(leftedPlayer);
        string leftedPlayerNickname = leftedPlayer.NickName;
        notifText.text = leftedPlayerNickname + " ������� ����!";
        Instantiate(notification, notification.transform);
    }
}
