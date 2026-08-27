#nullable disable

using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using Firebase;
// using Firebase.Database;
using System.Threading.Tasks;

public class connectToServer : MonoBehaviourPunCallbacks
{
    //text in map selector

    public Image mapSelectorImage;
    public TextMeshProUGUI mapSelectorName;
    public TextMeshProUGUI mapSelectorDesc;
    public TextMeshProUGUI mapOnlineText;
    public byte mapOnlineTestPlace;
    public byte mapOnlineCityCrash;
    public byte mapOnlineCubeGun;
    public byte mapOnlineTemniyLes;

    //testPlace
    public Image testPlaceImage;
    public TextMeshProUGUI testPlaceName;
    public TextMeshProUGUI testPlaceDesc;

    //cityCrash
    public Image cityCrashImage;
    public TextMeshProUGUI cityCrashName;
    public TextMeshProUGUI cityCrashDesc;

    //cubeGun
    public Image cubeGunImage;
    public TextMeshProUGUI cubeGunName;
    public TextMeshProUGUI cubeGunDesc;

    //cubeGun
    public Image temniyLesImage;
    public TextMeshProUGUI temniyLesName;
    public TextMeshProUGUI temniyLesDesc;

    //other

    public static short skinID = 1;
    public static string reason;
    public static string type;
    public static bool notFirstTimeConnected;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI connectText;
    public TextMeshProUGUI createText;
    public TextMeshProUGUI nickInfo;

    public Animator UIanim;
    public Animator CameraAnim;
    public Animator PlayerAnim;
    public Animator mapSelect;

    public GameObject loading;
    public GameObject loadingMask;
    public GameObject loginButtons;
    public GameObject settingsGUI;
    public GameObject settingsFade;
    public GameObject skinMenu;
    public GameObject menuButtons;
    public GameObject mapSelectMenu;
    public GameObject showPrivateServersButton;
    public GameObject privateServersButtons;

    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField nickInput;

    public Slider sensitivityPlus;

    public bool movingToSkin;
    public bool movingFromSkin;
    public int gameID;
    public bool publicGameConnect;
    public string targetRoomName;

    public GameObject quitGUI;
    public GameObject aboutGameGUI;
    public GameObject banGUI;
    public TextMeshProUGUI banReason;
    public GameObject kickGUI;
    public TextMeshProUGUI kickReason;
    public GameObject correctKeyEnteredGUI;
    public GameObject usedKeyEnteredGUI;
    public bool kicked;

    // FirebaseDatabase db;
    private string currentVersion;
    public GameObject updateGUI;
    string newVersionLink;
    public TextMeshProUGUI whatsNewText;
    public TextMeshProUGUI newVersionText;
    void Start()
    {
        currentVersion = Application.version;
        string fileName = "dev.txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        bool keyN1Entered = false;
        bool badKeyEntered = false;

        if (System.IO.File.Exists(filePath))
        {
            string fileContent = System.IO.File.ReadAllText(filePath);
            Debug.Log("File content: " + fileContent);
            if (fileContent == "oasf-t7ta-s6i7-uaf7") { keyN1Entered = true; }
            if (fileContent != "" && !keyN1Entered) { badKeyEntered = true; }
        }
        if (!System.IO.File.Exists(filePath))
        {
            string fileContent = "";
            System.IO.File.WriteAllText(filePath, fileContent);
            Debug.Log("File created and written to: " + filePath);
        }

        string unbanKeyN1HasUsed = PlayerPrefs.GetString("N1");
        if (unbanKeyN1HasUsed == "") { unbanKeyN1HasUsed = "no"; }

        if (keyN1Entered && unbanKeyN1HasUsed == "no") { PlayerPrefs.SetString("N1", "yes"); correctKeyEnteredGUI.SetActive(true); PlayerPrefs.SetString("type", ""); }
        if (keyN1Entered && unbanKeyN1HasUsed == "yes") { usedKeyEnteredGUI.SetActive(true); }
        if (badKeyEntered) { usedKeyEnteredGUI.SetActive(true); }

        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.GameVersion = Application.version;
        reason = PlayerPrefs.GetString("reason");
        string kickedText = PlayerPrefs.GetString("kicked");
        if (kickedText == "yes") { kicked = true; }
        if (kicked) { kickGUI.SetActive(true); }
        kickReason.text = "�������: " + reason + "\n" + "� ��������� ��� ������� �� ��� ��� �� �������\n" + "��������� � ����� �������� �� ������� ����� � ����� ������� �������.";
        banReason.text = "�� ���� �������� � ����� ����.\r\n�������: " + reason + ".\r\n��������� � ����� �������� ���� �� ������� ����� � ����� ������� �������.";
        type = PlayerPrefs.GetString("type");
        //if (type != "ban") PhotonNetwork.ConnectUsingSettings();
        if (type == "ban") { banGUI.SetActive(true); }
        skinID = (short)PlayerPrefs.GetInt("skinID");
        if (PlayerPrefs.GetInt("skinID") == 0) { skinID = 1; }
        nickInput.text = PlayerPrefs.GetString("nickname");
        if (PlayerPrefs.GetString("nickname") == "") { nickInput.text = "��������"; }
        PlayerPrefs.SetString("nickname", nickInput.text);
        PhotonNetwork.NickName = nickInput.text;
        Application.targetFrameRate = 60;
        if (notFirstTimeConnected) { loadingText.text = "��������..."; }

        if (PlayerPrefs.GetFloat("sensitivityPlus") == 0) { sensitivityPlus.value = 2.5f; }

        // CheckForUpdate();
    }
    void Update()
    {
        if (gameID == 0) { mapSelect.SetBool("selected", false); privateServersButtons.SetActive(false); showPrivateServersButton.SetActive(true); }
        if (gameID != 0) { mapSelect.SetBool("selected", true); }
        if (notFirstTimeConnected) { loadingText.text = "��������..."; }
        if (gameID == 1) { mapSelectorImage.sprite = testPlaceImage.sprite; mapSelectorName.text = testPlaceName.text; mapSelectorDesc.text = testPlaceDesc.text; mapOnlineText.text = mapOnlineTestPlace.ToString(); }
        if (gameID == 2) { mapSelectorImage.sprite = cityCrashImage.sprite; mapSelectorName.text = cityCrashName.text; mapSelectorDesc.text = cityCrashDesc.text; mapOnlineText.text = mapOnlineCityCrash.ToString(); }
        if (gameID == 3) { mapSelectorImage.sprite = cubeGunImage.sprite; mapSelectorName.text = cubeGunName.text; mapSelectorDesc.text = cubeGunDesc.text; mapOnlineText.text = mapOnlineCubeGun.ToString(); }
        if (gameID == 4) { mapSelectorImage.sprite = temniyLesImage.sprite; mapSelectorName.text = temniyLesName.text; mapSelectorDesc.text = temniyLesDesc.text; mapOnlineText.text = mapOnlineTemniyLes.ToString(); }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            Debug.Log($"Room: {room.Name}, Players: {room.PlayerCount}/{room.MaxPlayers}");
            if (room.Name == "1public") mapOnlineTestPlace = (byte)room.PlayerCount;
            if (room.Name == "2public") mapOnlineCityCrash = (byte)room.PlayerCount;
            if (room.Name == "3public") mapOnlineCubeGun = (byte)room.PlayerCount;
            if (room.Name == "4public") mapOnlineTemniyLes = (byte)room.PlayerCount;
        }
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby, requesting room list...");
    }
    public void saveSensitivity() { PlayerPrefs.SetFloat("sensitivityPlus", sensitivityPlus.value); }
    public override void OnConnectedToMaster()
    {
        if (type != "ban")
        {
            loading.SetActive(false);
            loadingMask.SetActive(false);
            loginButtons.SetActive(true);
            PhotonNetwork.JoinLobby();
        }
    }
    public void createRoom()
    {
        if (createInput.text != "")
        {
            notFirstTimeConnected = true;
            loadingMask.SetActive(true);
            loading.SetActive(true);
            PlayerPrefs.SetString("nickname", nickInput.text);
            PhotonNetwork.NickName = nickInput.text;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 0;
            PhotonNetwork.CreateRoom(gameID.ToString() + createInput.text, roomOptions);
        }
        else { connectText.text = "���� �� ������ ���� ������"; StartCoroutine(joinTextToNormal()); }
    }
    public void joinPublicRoom()
    {
        notFirstTimeConnected = true;
        loadingMask.SetActive(true);
        loading.SetActive(true);
        publicGameConnect = true;
        PlayerPrefs.SetString("nickname", nickInput.text);
        PhotonNetwork.NickName = nickInput.text;
        PhotonNetwork.JoinRoom(gameID.ToString() + "public");
    }
    public void joinPrivateRoom()
    {
        if (createInput.text != "")
        {
            notFirstTimeConnected = true;
            loadingMask.SetActive(true);
            loading.SetActive(true);
            PlayerPrefs.SetString("nickname", nickInput.text);
            PhotonNetwork.NickName = nickInput.text;
            PhotonNetwork.JoinRoom(gameID.ToString() + createInput.text);
        }
        else { connectText.text = "���� �� ������ ���� ������"; StartCoroutine(joinTextToNormal()); }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (publicGameConnect)
        {
            notFirstTimeConnected = true;
            PlayerPrefs.SetString("nickname", nickInput.text);
            PhotonNetwork.NickName = nickInput.text;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 0;
            PhotonNetwork.CreateRoom(gameID.ToString() + "public", roomOptions);
        }
        if (!publicGameConnect) { connectText.text = "�� ������� ������������"; StartCoroutine(joinTextToNormal()); loadingMask.SetActive(false); loading.SetActive(false); }
        publicGameConnect = false;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createText.text = "�� ������� ������� �������";
        StartCoroutine(createTextToNormal());
        loadingMask.SetActive(false);
        loading.SetActive(false);
    }
    public override void OnJoinedRoom()
    {
        if (gameID == 1) { PhotonNetwork.LoadLevel("TestPlace1"); }
        if (gameID == 2) { PhotonNetwork.LoadLevel("CityCrash"); }
        if (gameID == 3) { PhotonNetwork.LoadLevel("CubeGun"); }
        if (gameID == 4) { PhotonNetwork.LoadLevel("TemniyLes"); }
    }
    public void skinMenuOpen()
    {
        loginButtons.SetActive(false);
        skinMenu.SetActive(true);
        movingToSkin = true;
        StartCoroutine(skinCameraMoveTo());
        UIanim.SetBool("opened", true);
        CameraAnim.SetBool("opened", true);
        PlayerAnim.SetBool("opened", true);
    }
    public IEnumerator skinCameraMoveTo() { yield return new WaitForSeconds(.1f); movingToSkin = false; }
    public void skinMenuClose()
    {
        PlayerPrefs.SetInt("skinID", skinID);
        loginButtons.SetActive(true);
        skinMenu.SetActive(false);
        movingFromSkin = true;
        StartCoroutine(skinCameraMoveFrom());
        UIanim.SetBool("opened", false);
        CameraAnim.SetBool("opened", false);
        PlayerAnim.SetBool("opened", false);
    }
    public void checkNickname()
    {
        if (nickInput.text.Length > 20) { nickInfo.text = "������� �� ����� ���� ������ 20 ��������"; nickInput.text = PlayerPrefs.GetString("nickname"); StartCoroutine(nicknameInfoBackToNormal()); }
        if (nickInput.text.Contains("<size=")) { nickInfo.text = "� �������� �� ������ ���� ������� ��������"; nickInput.text = PlayerPrefs.GetString("nickname"); StartCoroutine(nicknameInfoBackToNormal()); }
        if (nickInput.text.Contains("<color=")) { nickInfo.text = "� �������� �� ������ ���� ������� �����"; nickInput.text = PlayerPrefs.GetString("nickname"); StartCoroutine(nicknameInfoBackToNormal()); }
        if (nickInput.text.Contains(" ")) { nickInfo.text = "� �������� �� ������ ���� ��������"; nickInput.text = PlayerPrefs.GetString("nickname"); StartCoroutine(nicknameInfoBackToNormal()); }
        if (nickInput.text == "") { nickInfo.text = "������� �� ������ ���� ������"; nickInput.text = PlayerPrefs.GetString("nickname"); StartCoroutine(nicknameInfoBackToNormal()); }
        if (nickInput.text != "" && nickInput.text.Length <= 20 && !nickInput.text.Contains("<size=") && !nickInput.text.Contains("<color=") && !nickInput.text.Contains(" ")) { PlayerPrefs.SetString("nickname", nickInput.text); }
    }
    public void playButton() { mapSelectMenu.SetActive(true); menuButtons.SetActive(false); }
    public void closeMapSelect() { mapSelectMenu.SetActive(false); menuButtons.SetActive(true); }
    public void selectMapTestMap() { gameID = 1; }
    public void selectMapCityCrash() { gameID = 2; }
    public void selectMapCubeGun() { gameID = 3; }
    public void selectMapTemniyLes() { gameID = 4; }
    public void clearSelectedMap() { gameID = 0; }
    public void showPrivateServerButtons() { privateServersButtons.SetActive(true); showPrivateServersButton.SetActive(false); }
    public IEnumerator skinCameraMoveFrom() { yield return new WaitForSeconds(.1f); movingFromSkin = false; }
    public IEnumerator createTextToNormal() { yield return new WaitForSeconds(3); createText.text = "�������"; }
    public IEnumerator joinTextToNormal() { yield return new WaitForSeconds(3); connectText.text = "��������������"; }
    public void skinButtonCreeper() { skinID = 1; }
    public void skinButtonVasyaPolice() { skinID = 2; }
    public void skinButtonGreyTiger() { skinID = 3; }
    public void skinButtonRedRobot() { skinID = 4; }
    public void skinButtonMichal() { skinID = 5; }
    public void skinButtonNosok() { skinID = 6; }
    public void skinButtonCayborg() { skinID = 7; }
    public void skinButtonMrTemchik() { skinID = 8; }
    public void skinButtonPoliceNatalie() { skinID = 9; }
    public void skinButtonSetSkinID(int ID) { skinID = (short)ID; }
    public void exitGameButton() { quitGUI.SetActive(true); }
    public void exitGameButtonYes() { Application.Quit(); }
    public void exitGameButtonNo() { quitGUI.SetActive(false); }
    public void kickWindowClose() { kickGUI.SetActive(false); PlayerPrefs.SetString("reason", ""); PlayerPrefs.SetString("kicked", ""); }
    public void settingsOpen() { settingsGUI.SetActive(true); settingsFade.SetActive(true); }
    public void settingsClose() { settingsGUI.SetActive(false); settingsFade.SetActive(false); }
    public void aboutGameButtonOpen() { aboutGameGUI.SetActive(true); settingsFade.SetActive(true); }
    public void aboutGameButtonClose() { aboutGameGUI.SetActive(false); settingsFade.SetActive(false); }
    public void GUIButtonClose(GameObject GUI) { GUI.SetActive(false); }
    public void discordButton() { Application.OpenURL("https://discord.com/invite/Q7d4aFTs3p"); }
    public void youtubeButton() { Application.OpenURL("https://www.youtube.com/@dpakohfire"); }
    public void donationAlertsButton() { Application.OpenURL("https://www.donationalerts.com/r/dpakohfine"); }
    public void updateLinkButton() { Application.OpenURL(newVersionLink); }
    public IEnumerator nicknameInfoBackToNormal() { yield return new WaitForSeconds(3f); nickInfo.text = "�������"; }
    private async Task CheckForUpdate() {
		//��� � ������� �������� ������

        Debug.Log("������ ���������� ���������.");
        if (type != "ban") {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}