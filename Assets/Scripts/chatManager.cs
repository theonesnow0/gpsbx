#nullable disable

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.EventSystems;
using Photon.Chat;
using ExitGames.Client.Photon;

public class chatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    public TMP_InputField inputField;
    public Button sendButton;
    public TextMeshProUGUI chatDisplay;
    public bool pechataet;
    public bool disconnected;

    public gameModifiersSettings gameModifiers;
    public PlayerController controller;

    private void Start()
    {
        gameModifiers = GameObject.FindWithTag("gameModifiersObject").GetComponent<gameModifiersSettings>();
        sendButton.onClick.AddListener(SendMessage);
        //inputField.onSubmit.AddListener(HandleSubmit);
        ConnectToPhotonChat();
    }
    public void Update()
    {
        if (chatClient != null) { chatClient.Service(); }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!pechataet)
            {
                pechataet = true;
                inputField.ActivateInputField();
                EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            }
            else if (inputField.text != "")
            {
                SendMessage();
                pechataet = false;
                inputField.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
    public void ConnectToPhotonChat()
    {
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, Application.version, new AuthenticationValues(PhotonNetwork.NickName));
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { PhotonNetwork.CurrentRoom.Name });
        chatDisplay.text += "��� �������: " + PhotonNetwork.NickName + "\n";
    }

    public void OnDisconnected()
    {
        chatDisplay.text += "Disconnected from chat\n";
        disconnected = true;
    }

    public void OnChatStateChange(ChatState state)
    {
        // ��������� ��������� ��������� ���� (�����������)
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < messages.Length; i++)
        {
            chatDisplay.text += "[<color=#00BFFF>" + senders[i] + "</color>]" + ": " + messages[i].ToString() + "\n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        // ��������� ������ ��������� (�����������)
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatDisplay.text += "��������� � ����\n";
        if (gameModifiers.flashlightEnabled) { chatDisplay.text += "[<color=blue>�����������</color>] � ���� ���� �������� <color=yellow>�������</color>.\n��� �� ��� ��������/���������, ������� ������ 'F', �� �������� ������� ������ ������� ������\n"; }
    }

    public void OnUnsubscribed(string[] channels)
    {
        // ��������� ������� �� ������ (�����������)
        disconnected = true;
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        // ��������� ���������� ������� (�����������)
    }

    public void OnUserSubscribed(string channel, string user)
    {
        // ��������� �������� ������������ (�����������)
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        // ��������� ������� ������������ (�����������)
    }
    public void DebugReturn(DebugLevel lvl, string msg)
    {
        // ���� ����� ��� ����� ��������
    }
    public void SendMessage()
    {
        if (!string.IsNullOrEmpty(inputField.text) && !inputField.text.Contains("<size") && !inputField.text.Contains("<color") && inputField.text.Length < 50)
        {
            chatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, inputField.text);
            inputField.text = "";
        }
    }
    public void HandleSubmit(string text)
    {
        EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
        inputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }
    public void Pechataet() { pechataet = true; }
    public void nePechataet() { pechataet = false; }
    public void unsubscribeFromChat(string[] name) { chatClient.Unsubscribe(name); }
    [PunRPC] public void ReceiveMessage(string sender, string message) { chatDisplay.text += sender + ": " + message + "\n"; }
}
