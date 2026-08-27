#nullable disable

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Chat;
using UnityEngine.EventSystems;
using Photon.Realtime;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviourPun, IPointerDownHandler, IPointerUpHandler
{
    public gameModifiersSettings gameModifiers;
    public GameObject flashlight;
    public Animator flashlightActions;

    public RectTransform joystickArea;
    public GameObject clonedMobileGui;
    public GameObject clonedJoystick;
    public GameObject clonedWeaponShoot;
    public GameObject clonedJump;
    public GameObject clonedCrouch;
    public GameObject clonedRunning;

    public GameObject mobileButtons;
    public GameObject joystickObject;
    public FloatingJoystick mobileJoystick;
    public GameObject runningButton;
    public GameObject jumpButton;
    public GameObject crouchButton;
    public GameObject weaponShootButton;
    public GameObject interactButton;

    public LayerMask groundedMask;
    public PlayerController control;

    public float cubegunClearTime;

    public Slider sensitivityPlus;
    public TextMeshPro nick;
    public TextMeshProUGUI nickHealth;
    public TextMeshProUGUI healthNumber;
    PhotonView view;

    public bool inSettings;
    public bool justConnected = true;
    public bool inMenu = true;
    public bool died;
    public bool canRespawn;
    public GameObject menuSkin;
    public GameObject menuCamera;
    public GameObject menuButtons;
    public GameObject settingsPopup;
    public GameObject chatGUI;
    public GameObject tabGUI;

    public chatManager chatMan;

    public byte skinID;
    public GameObject shield;
    public Animator shieldAnim;
    public GameObject creeperProfi;
    public GameObject vasyaPolice;

    public bool isReloading;
    public GameObject signalE;
    public GameObject logicObject;
    public telikVrubanie telikLogic;
    public bool canInteract;
    public GameObject gameGUI; 
    public GameObject ammoGUI;
    public GameObject pricel;
    public Slider stamina;
    public Slider hpSlider;
    public float staminaMinus = .5f;
    public float staminaRegen = 0.025f;
    public float staminaValue = 10f;
    public float maxStaminaValue = 15f;
    public float staminaWaitingTime = 0;
    public short hp = 100;
    public bool inShield = true;
    public float shieldDuration = 3f;
    public TextMeshProUGUI StaminaText;
    public byte StaminaTextTransparency = 200;
    private byte StaminaTextMaxTransparency = 200;
    public Image StaminaBack;
    public Image StaminaFill;
    public CinemachineFreeLook camera3d;
    public GameObject cameraTPV;
    public GameObject cameraFPS;

    public GameObject vasyaskin;
    public Material keramich;
    public Material vasyachorn;
    public Material vasyakrasni;
    public Material vasyajolti;

    //public GameObject michalSkin;
    public Material MicalChorn;
    public Material MicalKrasn;
    public Material MicalTemnoKrasn;
    public Material MicalBeton;
    public Material MicalJolti;
    public Material MicalSvetloOranj;

    //public GameObject nosokSkin;
    public Material nosokWolf;

    public Material shadowCastMaterial;

    public GameObject creeperHalatHead;
    public GameObject vasyaPoliceHead;
    public GameObject greyTigerHead;
    public GameObject skeplorsikHead;
    public GameObject michalHead;
    public GameObject nosokHead;
    public GameObject cayborgHead;
    public GameObject mrTemchikHead;
    public GameObject policeNatalieHead;
    public GameObject androidHead;
    public GameObject springtrapHead;

    public Material[] creeperHalatMaterials;
    public Material[] vasyaPoliceMaterials;
    public Material[] greyTigerMaterials;
    public Material[] skeplorsikMaterials;
    public Material[] michalMaterials;
    public Material[] nosokMaterials;
    public Material[] cayborgMaterials;
    public Material[] mrTemchikMaterials;
    public Material[] policeNatalieMaterials;
    public Material[] androidMaterials;
    public Material[] springtrapMaterials;

    public float horizontal;
    public float vertical;
    public float forwardMovement;
    public float sideMovement;
    public float mouseX;
    public float mouseY;
    public float FPSSpeedDeletor = 35f;
    public float fov = 70;
    public bool recentlyRun;

    public bool FirstPS;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 75f;
    private float verticalRotation;
    private float horizontalRotation;
    public byte weaponID;

    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float moveSpeedNormal = 3;
    public float moveSpeedRun = 5;
    public float moveSpeedCrouch = 1;
    public float turnSpeed = 1000f;
    public bool walk;
    public bool crouching;
    public bool isRunningMobile;

    public bool canBoost = true;
    public float jumpForce = 7.0f;
    public float jumpBoost;
    public float jumpBoostMultiplier = 0.125f;
    public bool isGrounded;
    public float raycastDistance;
    public float checkRadius = 0.5f;
    public Rigidbody rb;

    public AudioClip trava;
    public AudioClip solidBlock;
    public AudioSource footstep;
    public Material zelen;
    public Material standBlock;

    public GameObject spawnPlace;
    public GameObject[] spawnPlaces;
    public GameObject player;
    public Vector3 boxSize = new(.5f, .5f, .5f);
    public float cantStandDistance = .5f;
    public bool cantStand;
    public bool wantStand;
    public CapsuleCollider standingTors;
    public BoxCollider standingHead;
    public CapsuleCollider crouchingTors;
    public BoxCollider crouchingHead;
    public GameObject staminaParent;
    public Animator anim;
    public Animator stamin;
    public Animator skinIdSelector;
    public Animator deathScreenAnim;

    public GameObject creeperHalatSkin;
    public GameObject vasyaPoliceSkin;
    public GameObject greyTigerSkin;
    public GameObject skeplorsikSkin;
    public GameObject michalSkin;
    public GameObject nosokSkin;
    public GameObject cayborgSkin;
    public GameObject mrTemchikSkin;
    public GameObject policeNatalieSkin;
    public GameObject androidSkin;
    public GameObject springtrapAreSkin;

    public Material green;
    public Material black;
    public Material yellow;
    public Material cloud;
    public Material blue;
    public Material bricks;
    public Material red;
    public Material wood;
    public Material cement;
    public Material embrassedMetal;

    public string cubegunMaterial = "green";
    public Renderer cubegunCube;
    public byte ammoReload;
    public byte ammoValue;
    public TextMeshProUGUI reloadValue;
    public TextMeshProUGUI shootValue;
    public bool canShoot;
    public bool shootMobile;
    public bool cubegunCanRemoveBlock;
    public GameObject revolver;
    public Transform bulletStart;
    public Transform cubegunStart;

    public GameObject revolverBullet;
    public GameObject cubegunBulletGreen;
    public GameObject cubegunBulletBlack;
    public GameObject cubegunBulletYellow;
    public GameObject cubegunBulletCloud;
    public GameObject cubegunBulletBlue;
    public GameObject cubegunBulletBricks;
    public GameObject cubegunBulletRed;
    public GameObject cubegunBulletWood;
    public GameObject cubegunBulletCement;
    public GameObject cubegunBulletEmbrassedMetal;

    public GameObject kickBullet;
    public GameObject banBullet;

    public bool adminKick;
    public bool adminBan;
    public GameObject reasonObject;
    public TMP_InputField reason;
    public static string reasonToGive;
    public LayerMask playerLayer;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        //if (!view.IsMine) { control.enabled = false; }
        if (view.IsMine)
        {
            gameModifiers = GameObject.FindWithTag("gameModifiersObject").GetComponent<gameModifiersSettings>();
            mobileButtons = GameObject.FindWithTag("mobileButtons");
            joystickObject = GameObject.FindWithTag("joystickObject");
            joystickArea = joystickObject.GetComponent<RectTransform>();
            mobileJoystick = joystickObject.GetComponent<FloatingJoystick>();
            runningButton = GameObject.FindWithTag("runningButton");
            jumpButton = GameObject.FindWithTag("jumpButton");
            crouchButton = GameObject.FindWithTag("crouchButton");
            weaponShootButton = GameObject.FindWithTag("weaponShootButton");
            interactButton = GameObject.FindWithTag("signalE");
            GameObject cameraModeObject = GameObject.FindWithTag("cameraMode");
            Button cameraModeButton = cameraModeObject.GetComponent<Button>();
            GameObject weaponThrowObject = GameObject.FindWithTag("weaponThrow");
            Button weaponThrowButton = weaponThrowObject.GetComponent<Button>();
            GameObject weaponReloadObject = GameObject.FindWithTag("weaponReload");
            Button weaponReloadButton = weaponReloadObject.GetComponent<Button>();
            GameObject weaponOsmotrObject = GameObject.FindWithTag("weaponOsmotr");
            Button weaponOsmotrButton = weaponOsmotrObject.GetComponent<Button>();
            GameObject resetObject = GameObject.FindWithTag("reset");
            Button resetButton = resetObject.GetComponent<Button>();
            GameObject danceObject = GameObject.FindWithTag("dance");
            Button danceButton = danceObject.GetComponent<Button>();

            cameraModeButton.onClick.AddListener(mobileChangeViewCamera);
            weaponThrowButton.onClick.AddListener(mobileWeaponThrow);
            weaponReloadButton.onClick.AddListener(mobileWeaponReload);
            weaponOsmotrButton.onClick.AddListener(mobileWeaponOsmotr);
            resetButton.onClick.AddListener(mobileResetCharacter);
            danceButton.onClick.AddListener(mobileDanceButton);
            interactButton.GetComponent<Button>().onClick.AddListener(changeStateOfTelik);

            foreach (EventTrigger.Entry entry in clonedWeaponShoot.GetComponent<EventTrigger>().triggers)
            {
                EventTrigger.Entry newEntry = new()
                {
                    eventID = entry.eventID,
                    callback = entry.callback
                };
                weaponShootButton.GetComponent<EventTrigger>().triggers.Add(newEntry);
            }
            foreach (EventTrigger.Entry entry in clonedJump.GetComponent<EventTrigger>().triggers)
            {
                EventTrigger.Entry newEntry = new EventTrigger.Entry();
                newEntry.eventID = entry.eventID;
                newEntry.callback = entry.callback;
                jumpButton.GetComponent<EventTrigger>().triggers.Add(newEntry);
            }
            foreach (EventTrigger.Entry entry in clonedCrouch.GetComponent<EventTrigger>().triggers)
            {
                EventTrigger.Entry newEntry = new EventTrigger.Entry();
                newEntry.eventID = entry.eventID;
                newEntry.callback = entry.callback;
                crouchButton.GetComponent<EventTrigger>().triggers.Add(newEntry);
            }
            foreach (EventTrigger.Entry entry in clonedRunning.GetComponent<EventTrigger>().triggers)
            {
                EventTrigger.Entry newEntry = new EventTrigger.Entry();
                newEntry.eventID = entry.eventID;
                newEntry.callback = entry.callback;
                runningButton.GetComponent<EventTrigger>().triggers.Add(newEntry);
            }
            clonedMobileGui.SetActive(false);
        }
        spawnPlace = GameObject.FindWithTag("playerSpawn");
        spawnPlaces = GameObject.FindGameObjectsWithTag("playerSpawn");
        if (spawnPlaces.Length != 1) { spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)]; }

        sensitivityPlus.value = PlayerPrefs.GetFloat("sensitivityPlus");
        if (PlayerPrefs.GetFloat("sensitivityPlus") == 0) { sensitivityPlus.value = 2.5f; }
        nick.text = view.Owner.NickName;
        nickHealth.text = view.Owner.NickName;
        if (view.IsMine)
        {
            staminaParent.SetActive(true);

            if (!Application.isMobilePlatform) { mobileButtons.SetActive(false); }

            chatMan = chatGUI.GetComponent<chatManager>();

            //Material[] creeperMaterials = creeperProfi.GetComponent<Renderer>().materials;
            //creeperMaterials[3] = keramich;
            //creeperProfi.GetComponent<Renderer>().materials = creeperMaterials;

            //Material[] materials = michalSkin.GetComponent<Renderer>().materials;
            //materials[1] = MicalJolti;
            //materials[4] = MicalKrasn;
            //materials[5] = MicalBeton;
            //materials[9] = MicalChorn;
            //materials[11] = MicalSvetloOranj;
            //materials[12] = MicalTemnoKrasn;
            //michalSkin.GetComponent<Renderer>().materials = materials;

            //Material[] materialsVasya = vasyaskin.GetComponent<Renderer>().materials;
            //materialsVasya[4] = vasyachorn;
            //materialsVasya[5] = vasyajolti;
            //materialsVasya[6] = vasyakrasni;
            //vasyaskin.GetComponent<Renderer>().materials = materialsVasya;

            //Material[] materialsNosok = nosokSkin.GetComponent<Renderer>().materials;
            //materialsNosok[5] = nosokWolf;
            //nosokSkin.GetComponent<Renderer>().materials = materialsNosok;

            creeperHalatMaterials = creeperHalatHead.GetComponent<Renderer>().materials;
            vasyaPoliceMaterials = vasyaPoliceHead.GetComponent<Renderer>().materials;
            greyTigerMaterials = greyTigerHead.GetComponent<Renderer>().materials;
            skeplorsikMaterials = skeplorsikHead.GetComponent<Renderer>().materials;
            michalMaterials = michalHead.GetComponent<Renderer>().materials;
            nosokMaterials = nosokHead.GetComponent<Renderer>().materials;
            cayborgMaterials = cayborgHead.GetComponent<Renderer>().materials;
            mrTemchikMaterials = mrTemchikHead.GetComponent<Renderer>().materials;
            policeNatalieMaterials = policeNatalieHead.GetComponent<Renderer>().materials;
            androidMaterials = androidHead.GetComponent<Renderer>().materials;
            springtrapMaterials = springtrapHead.GetComponent<Renderer>().materials;
        }
        if (!view.IsMine) { nick.enabled = true; }
        anim = player.GetComponent<Animator>();
        stamin = staminaParent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        verticalRotation = transform.eulerAngles.x;
        horizontalRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            skinID = (byte)connectToServer.skinID;
            skinIdSelector.SetInteger("skinID", skinID);
            if (!died) { gameGUI.SetActive(!inMenu); }
            hpSlider.value = hp;
            healthNumber.text = hp.ToString();
            shieldAnim.SetBool("off", !inShield);
            if (hp < 0) { hp = 0; }
            if (hp == 0) { died = true; inMenu = false; inSettings = false; settingsPopup.SetActive(false); }
            if (died && !inMenu && !canRespawn) { Cursor.lockState = CursorLockMode.None; ; deathScreenAnim.SetBool("died", true); weaponID = 0; FirstPS = false; rb.freezeRotation = false; anim.SetBool("isWalk", false); anim.SetBool("isWalk", false); gameGUI.SetActive(false); StartCoroutine(respawnTiming()); }
            if (!died) { rb.isKinematic = false; rb.freezeRotation = true; deathScreenAnim.SetBool("died", false); camera3d.Follow = gameObject.transform; }
            if (canRespawn) { PlayerRespawn(); camera3d.Follow = gameObject.transform; shieldDuration = 3f; }

            if (died && !inMenu && !canRespawn)
            {
                foreach (AnimatorControllerParameter parameter in anim.parameters)
                {
                    switch (parameter.type)
                    {
                        case AnimatorControllerParameterType.Bool:
                            anim.SetBool(parameter.name, false);
                            break;
                    }
                }
            }

            if (justConnected) { shieldDuration = 3f; }
            if (shieldDuration <= 0) { inShield = false; }
            if (shieldDuration > 0) { inShield = true; }

            moveSpeedCrouch = moveSpeedNormal - (moveSpeedNormal / 1.5f);
            moveSpeedRun = moveSpeedNormal + (moveSpeedNormal / 1.5f);
            jumpBoostMultiplier = jumpForce / 267;
        }
        if (!died) { tabGUI.SetActive(Input.GetKey(KeyCode.Tab)); }
        if (died) { tabGUI.SetActive(false); }
        if (!inMenu && !died)
        {
            if (view.IsMine && !chatMan.pechataet)
            {
                if (cubegunClearTime >= 1f) { cubegunCanRemoveBlock = true; anim.SetBool("cubegunReadyToRemove", true); }
                if (cubegunClearTime < 1f) { cubegunCanRemoveBlock = false; anim.SetBool("cubegunReadyToRemove", false); }

                camera3d.enabled = true;

                if (Input.GetKeyDown("q")) { FirstPS = !FirstPS; cameraViewChange(); }
                if (Input.GetKey(KeyCode.LeftControl)) { anim.SetBool("crouch", true); moveSpeed = moveSpeedCrouch; crouching = true; wantStand = false; standingHead.enabled = false; standingTors.enabled = false; crouchingHead.enabled = true; crouchingTors.enabled = true; }
                if (Input.GetKeyUp(KeyCode.LeftControl)) { wantStand = true; }
                if (Input.GetKey("v")) { weaponID = 0; }
                if (Input.GetKey("b")) { anim.SetBool("sillyDance", true); }

                if (Input.GetMouseButtonUp(1) && !Application.isMobilePlatform) { anim.SetBool("running", false); moveSpeed = moveSpeedNormal; StartCoroutine(running()); }

                if (Input.GetMouseButtonDown(0) && weaponID == 1 && canShoot && ammoValue != 0 && !isReloading && !Application.isMobilePlatform) { anim.SetBool("revolverShoot", true); StartCoroutine(revolverShoot()); canShoot = false; revolverFire(); anim.SetBool("osmotrRevolver", false); ammoValue -= 1; }
                if (Input.GetMouseButtonUp(0) && weaponID == 2 && canShoot && ammoValue != 0 && !isReloading && !cubegunCanRemoveBlock && !inMenu && !Application.isMobilePlatform) { anim.SetBool("cubegunEmpty", true); StartCoroutine(cubegunShoot()); canShoot = false; cubegunFire(); anim.SetBool("cubegunReadyToRemove", false); cubegunCanRemoveBlock = false; cubegunClearTime = 0f; }
                if (Input.GetMouseButtonUp(0) && weaponID == 2 && cubegunCanRemoveBlock && !Application.isMobilePlatform) 
                {
                    int ignoreLayer = LayerMask.NameToLayer("IgnoreThisTagLayer");
                    int layerMask = ~(1 << ignoreLayer);
                    Ray ray = new Ray(cameraFPS.transform.position, cameraFPS.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
                    {
                        if (hit.collider.CompareTag("cubegunCube"))
                        {
                            PhotonView hitPhotonView = hit.collider.GetComponent<PhotonView>();
                            if (hitPhotonView != null && hitPhotonView.IsMine) { PhotonNetwork.Destroy(hit.collider.gameObject); }
                            if (hitPhotonView != null && !hitPhotonView.IsMine) { hitPhotonView.RPC("RequestDestroyObject", hitPhotonView.Owner, hitPhotonView.ViewID); hit.collider.gameObject.SetActive(false); }
                            if (ammoValue != 0 && ammoReload != 0) { ammoReload += 1; }
                            if (ammoValue != 0 && ammoReload == 0) { ammoReload += 1; }
                            if (ammoValue == 0 && ammoReload == 0) { ammoValue = 1; }
                        }
                    }
                    anim.SetBool("cubegunReadyToRemove", false);
                    cubegunCanRemoveBlock = false;
                    cubegunClearTime = 0f;
                }

                if (Input.GetKeyDown("k")) { hp = 0; }
                if (Input.GetKeyDown("r") && weaponID == 1 && ammoValue != 6 && ammoReload != 0)
                {
                    anim.SetBool("reloadRevolver", true);
                    if (!isReloading)
                    {
                        StartCoroutine(revolverReload());
                        isReloading = true;
                    }
                }
                if (Input.GetKeyDown("f") && weaponID == 1) { anim.SetBool("osmotrRevolver", true); StartCoroutine(osmotrRevReset()); }
                if (Input.GetKeyDown("f") && gameModifiers.flashlightEnabled) { flashlightActions.SetBool("on", !flashlightActions.GetBool("on")); }
                if (gameModifiers.flashlightEnabled) { flashlight.transform.rotation = cameraFPS.transform.rotation; }

                if (isGrounded && Input.GetButtonDown("Jump"))
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    anim.SetBool("jumped", true);
                    anim.SetBool("sillyDance", false);
                    canBoost = true;
                    StartCoroutine(jboost());
                }
                if (Input.GetButtonUp("Jump")) { canBoost = false; jumpBoost = 0f; }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (telikLogic != null)
                    {
                        telikLogic.isPlaying = !telikLogic.isPlaying;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && view.IsMine && !died && !chatMan.pechataet) { inMenu = true; }
        if (!view.IsMine) { jumpBoostMultiplier = 0; }
        if (!view.IsMine) { staminaRegen = 0; }
        if (!view.IsMine) { staminaMinus = 0; }
        if (!view.IsMine) { recentlyRun = false; }
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (staminaWaitingTime < 3) { staminaWaitingTime += 0.02f; recentlyRun = false; }
            if (staminaWaitingTime >= 3) { recentlyRun = true; }

            if (adminBan || adminKick) { reasonObject.SetActive(true); reasonToGive = reason.text; }
            if (!adminBan && !adminKick) { reasonObject.SetActive(false); }
            if (gameObject.transform.position.y < -50) { hp = 0; }
            menuCamera.SetActive(justConnected);
            if (!inSettings) { menuButtons.SetActive(inMenu); menuSkin.SetActive(justConnected); }

            if (shieldDuration > 0) { shieldDuration -= .02f; }

            if (!Application.isMobilePlatform) { signalE.SetActive(canInteract); interactButton.SetActive(false); }
            if (Application.isMobilePlatform) { interactButton.SetActive(canInteract); signalE.SetActive(false); }

            reloadValue.text = ammoReload.ToString();
            shootValue.text = ammoValue.ToString();

            if (standBlock.name.Contains("zelen")) { footstep.clip = trava; }
            else { footstep.clip = solidBlock; }

            if (!inMenu && !died && !Application.isMobilePlatform) { Cursor.lockState = CursorLockMode.Locked; }
            if (inMenu && !died && !Application.isMobilePlatform) { Cursor.lockState = CursorLockMode.None; }

            if (FirstPS)
            {
                camera3d.enabled = false;
                cameraFPS.SetActive(true);
                cameraTPV.SetActive(false);

                cubegunStart.rotation = cameraFPS.transform.rotation;

                //keramich.color = new Color(1, 1, 1, 0);

                //vasyachorn.color = new Color(1, 1, 1, 0);
                //vasyajolti.color = new Color(1, 1, 1, 0);
                //vasyakrasni.color = new Color(1, 1, 1, 0);

                //MicalJolti.color = new Color(1, 1, 1, 0);
                //MicalKrasn.color = new Color(1, 1, 1, 0);
                //MicalChorn.color = new Color(1, 1, 1, 0);
                //MicalSvetloOranj.color = new Color(1, 1, 1, 0);
                //MicalTemnoKrasn.color = new Color(1, 1, 1, 0);
                //MicalBeton.color = new Color(1, 1, 1, 0);

                //nosokWolf.color = new Color(1, 1, 1, 0);

                //if (connectToServer.skinID == 1)
                //{
                //    Material[] creeperHalatmaterials = creeperHalatHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < creeperHalatmaterials.Length; i++)
                //    {
                //        creeperHalatmaterials[i] = shadowCastMaterial;
                //    }
                //    creeperHalatHead.GetComponent<Renderer>().materials = creeperHalatmaterials;
                //}

                //if (connectToServer.skinID == 2)
                //{
                //    Material[] vasyaPolicematerials = vasyaPoliceHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < vasyaPolicematerials.Length; i++)
                //    {
                //        vasyaPolicematerials[i] = shadowCastMaterial;
                //    }
                //    vasyaPoliceHead.GetComponent<Renderer>().materials = vasyaPolicematerials;
                //}

                //if (connectToServer.skinID == 3)
                //{
                //    Material[] greyTigermaterials = greyTigerHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < greyTigermaterials.Length; i++)
                //    {
                //        greyTigermaterials[i] = shadowCastMaterial;
                //    }
                //    greyTigerHead.GetComponent<Renderer>().materials = greyTigermaterials;
                //}

                //if (connectToServer.skinID == 4)
                //{
                //    Material[] skeplorsikmaterials = skeplorsikHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < skeplorsikmaterials.Length; i++)
                //    {
                //        skeplorsikmaterials[i] = shadowCastMaterial;
                //    }
                //    skeplorsikHead.GetComponent<Renderer>().materials = skeplorsikmaterials;
                //}

                //if (connectToServer.skinID == 5)
                //{
                //    Material[] michalmaterials = michalHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < michalmaterials.Length; i++)
                //    {
                //        michalmaterials[i] = shadowCastMaterial;
                //    }
                //    michalHead.GetComponent<Renderer>().materials = michalmaterials;
                //}

                //if (connectToServer.skinID == 6)
                //{
                //    Material[] nosokmaterials = nosokHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < nosokmaterials.Length; i++)
                //    {
                //        nosokmaterials[i] = shadowCastMaterial;
                //    }
                //    nosokHead.GetComponent<Renderer>().materials = nosokmaterials;
                //}

                //if (connectToServer.skinID == 7)
                //{
                //    Material[] cayborgmaterials = cayborgHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < cayborgmaterials.Length; i++)
                //    {
                //        cayborgmaterials[i] = shadowCastMaterial;
                //    }
                //    cayborgHead.GetComponent<Renderer>().materials = cayborgmaterials;
                //}

                //if (connectToServer.skinID == 8)
                //{
                //    Material[] mrtemchikmaterials = mrTemchikHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < mrtemchikmaterials.Length; i++)
                //    {
                //        mrtemchikmaterials[i] = shadowCastMaterial;
                //    }
                //    mrTemchikHead.GetComponent<Renderer>().materials = mrtemchikmaterials;
                //}

                //if (connectToServer.skinID == 9)
                //{
                //    Material[] policenataliematerials = policeNatalieHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < policenataliematerials.Length; i++)
                //    {
                //        policenataliematerials[i] = shadowCastMaterial;
                //    }
                //    policeNatalieHead.GetComponent<Renderer>().materials = policenataliematerials;
                //}

                //if (connectToServer.skinID == 10)
                //{
                //    Material[] androidmaterials = androidHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < androidmaterials.Length; i++)
                //    {
                //        androidmaterials[i] = shadowCastMaterial;
                //    }
                //    androidHead.GetComponent<Renderer>().materials = androidmaterials;
                //}

                //if (connectToServer.skinID == 11)
                //{
                //    Material[] springtrapmaterials = springtrapHead.GetComponent<Renderer>().materials;
                //    for (int i = 0; i < springtrapmaterials.Length; i++)
                //    {
                //        springtrapmaterials[i] = shadowCastMaterial;
                //    }
                //    springtrapHead.GetComponent<Renderer>().materials = springtrapmaterials;
                //}
            }
            if (!FirstPS)
            {
                camera3d.enabled = true;
                cameraFPS.SetActive(false);
                cameraTPV.SetActive(true);

                cubegunStart.rotation = cameraTPV.transform.rotation;

                //keramich.color = new Color(1, 1, 1, 1);

                //vasyachorn.color = new Color(1, 1, 1, 1);
                //vasyajolti.color = new Color(1, 1, 1, 1);
                //vasyakrasni.color = new Color(1, 1, 1, 1);

                //MicalJolti.color = new Color(1, 1, 1, 1);
                //MicalKrasn.color = new Color(1, 1, 1, 1);
                //MicalChorn.color = new Color(1, 1, 1, 1);
                //MicalSvetloOranj.color = new Color(1, 1, 1, 1);
                //MicalTemnoKrasn.color = new Color(1, 1, 1, 1);
                //MicalBeton.color = new Color(1, 1, 1, 1);

                //nosokWolf.color = new Color(1, 1, 1, 1);

                //if (connectToServer.skinID == 1) creeperHalatHead.GetComponent<Renderer>().materials = creeperHalatMaterials;
                //if (connectToServer.skinID == 2) vasyaPoliceHead.GetComponent<Renderer>().materials = vasyaPoliceMaterials;
                //if (connectToServer.skinID == 3) greyTigerHead.GetComponent<Renderer>().materials = greyTigerMaterials;
                //if (connectToServer.skinID == 4) skeplorsikHead.GetComponent<Renderer>().materials = skeplorsikMaterials;
                //if (connectToServer.skinID == 5) michalHead.GetComponent<Renderer>().materials = michalMaterials;
                //if (connectToServer.skinID == 6) nosokHead.GetComponent<Renderer>().materials = nosokMaterials;
                //if (connectToServer.skinID == 7) cayborgHead.GetComponent<Renderer>().materials = cayborgMaterials;
                //if (connectToServer.skinID == 8) mrTemchikHead.GetComponent<Renderer>().materials = mrTemchikMaterials;
                //if (connectToServer.skinID == 9) policeNatalieHead.GetComponent<Renderer>().materials = policeNatalieMaterials;
                //if (connectToServer.skinID == 10) androidHead.GetComponent<Renderer>().materials = androidMaterials;
                //if (connectToServer.skinID == 11) springtrapHead.GetComponent<Renderer>().materials = springtrapMaterials;
            }

            camera3d.m_Lens.FieldOfView = fov;
            stamina.maxValue = maxStaminaValue;
            stamina.value = staminaValue;
            StaminaText.color = new Color32(255, 255, 255, StaminaTextTransparency);
            StaminaBack.color = new Color32(175, 175, 175, StaminaTextTransparency);
            StaminaFill.color = new Color32(175, 255, 175, StaminaTextTransparency);

            if (recentlyRun && staminaValue < maxStaminaValue && recentlyRun) { staminaValue += staminaRegen; }
            if (staminaValue < maxStaminaValue && StaminaTextTransparency < StaminaTextMaxTransparency) { StaminaTextTransparency += 3; staminaWaitingTime = 0; }
            if (staminaValue > maxStaminaValue && StaminaTextTransparency != 0 && recentlyRun) { StaminaTextTransparency -= 3; }
            if (staminaValue < maxStaminaValue / 3) { stamin.SetBool("low", true); }
            if (staminaValue > maxStaminaValue / 3) { stamin.SetBool("low", false); }

            if (!inMenu && !died && !chatMan.pechataet)
            {
                if (Input.GetButton("Jump") && canBoost && !Application.isMobilePlatform)
                {
                    anim.SetBool("jumped", true);
                    jumpBoost += jumpBoostMultiplier;
                    rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
                }
                if (canBoost && Application.isMobilePlatform)
                {
                    anim.SetBool("jumped", true);
                    jumpBoost += jumpBoostMultiplier;
                    rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
                }

                if (Input.GetMouseButton(0) && cubegunClearTime < 1f && !Application.isMobilePlatform) { cubegunClearTime += .02f; }
                if (Input.GetMouseButtonUp(0) && cubegunClearTime < 1f && !Application.isMobilePlatform) { cubegunClearTime = 0; }

                if (Input.GetMouseButton(1) && staminaValue > 0.5f && !crouching && walk && !Application.isMobilePlatform) { anim.SetBool("running", true); moveSpeed = moveSpeedRun; staminaValue -= staminaMinus; staminaWaitingTime = 0; }

                if (!Application.isMobilePlatform)
                {
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");

                    forwardMovement = Input.GetAxis("Vertical") * (moveSpeed / FPSSpeedDeletor);
                    sideMovement = Input.GetAxis("Horizontal") * (moveSpeed / FPSSpeedDeletor);
                }
                if (Application.isMobilePlatform)
                {
                    horizontal = mobileJoystick.Horizontal;
                    vertical = mobileJoystick.Vertical;

                    forwardMovement = mobileJoystick.Vertical * (moveSpeed / FPSSpeedDeletor);
                    sideMovement = mobileJoystick.Horizontal * (moveSpeed / FPSSpeedDeletor);
                }
            }

            //if (!isRunningMobile && mobileJoystick.Vertical < .8f && !crouching && walk && Application.isMobilePlatform) { isRunningMobile = false; }
            if (!isRunningMobile && mobileJoystick.Vertical > .9f && staminaValue > 0.5f && !crouching && walk && Application.isMobilePlatform) { isRunningMobile = true; }
            if (mobileJoystick.Vertical == 0 && mobileJoystick.Horizontal == 0) { isRunningMobile = false; }

            if (isRunningMobile && Application.isMobilePlatform && staminaValue > 0.5f && !crouching && walk) { anim.SetBool("running", true); moveSpeed = moveSpeedRun; staminaValue -= staminaMinus; staminaWaitingTime = 0; }
            if (!isRunningMobile && Application.isMobilePlatform && staminaValue > 0.5f && !crouching && walk) { anim.SetBool("running", false); moveSpeed = moveSpeedNormal; StartCoroutine(running()); }

            if (shootMobile && Application.isMobilePlatform)
            {
                if (weaponID == 1 && canShoot && ammoValue != 0 && !isReloading) { anim.SetBool("revolverShoot", true); StartCoroutine(revolverShoot()); canShoot = false; revolverFire(); anim.SetBool("osmotrRevolver", false); ammoValue -= 1; }
                if (cubegunClearTime < 1f) { cubegunClearTime += .02f; }
            }

            isGrounded = Physics.SphereCast(transform.position, checkRadius, -Vector3.up, out RaycastHit hit, raycastDistance, groundedMask, QueryTriggerInteraction.Ignore);
            if (isGrounded) { standBlock = hit.collider.GetComponent<Renderer>().material; }
            cantStand = Physics.BoxCast(transform.position, boxSize, Vector3.up, out RaycastHit hitInfo, Quaternion.identity, cantStandDistance);

            if (!cantStand && wantStand) { anim.SetBool("crouch", false); moveSpeed = moveSpeedNormal; crouching = false; wantStand = false; standingHead.enabled = true; standingTors.enabled = true; crouchingHead.enabled = false; crouchingTors.enabled = false; }

            if (isGrounded) { anim.SetBool("jumped", false); }
            anim.SetBool("falling", !isGrounded);

            if (staminaValue < 0.5f) { anim.SetBool("running", false); moveSpeed = moveSpeedNormal; StartCoroutine(running()); }
            if (crouching) { anim.SetBool("running", false); moveSpeed = moveSpeedCrouch; StartCoroutine(running()); }

            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 direction = cameraForward * vertical + cameraTransform.right * horizontal;
            direction.Normalize();
            if (!FirstPS && !inMenu && !died)
            {
                pricel.SetActive(false);

                if (!Application.isMobilePlatform)
                {
                    camera3d.m_XAxis.m_MaxSpeed = mouseSensitivity * 2.5f * sensitivityPlus.value;
                    camera3d.m_YAxis.m_MaxSpeed = mouseSensitivity / 3 / 10 / 2 * (sensitivityPlus.value);
                }
                if (Application.isMobilePlatform)
                {
                    if (Input.touchCount > 0)
                    {
                        for (int i = 0; i < Input.touchCount; i++)
                        {
                            Touch touch = Input.GetTouch(i);
                            if (!IsTouchOverUI(touch))
                            {
                                if (touch.phase == TouchPhase.Moved)
                                {
                                    camera3d.m_XAxis.Value += touch.deltaPosition.x * 2.5f * sensitivityPlus.value;
                                    camera3d.m_YAxis.Value -= touch.deltaPosition.y / 3 / 10 / 2 * (sensitivityPlus.value);
                                }
                            }
                        }
                    }
                }
                transform.Translate(direction * (moveSpeed / FPSSpeedDeletor), Space.World);
            }

            if (direction != Vector3.zero && !FirstPS && !died)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
                anim.SetBool("sillyDance", false);
                walk = true;
            }
            if (FirstPS && !inMenu && !died && !chatMan.pechataet)
            {
                pricel.SetActive(true);

                Vector3 movement = new Vector3(sideMovement, 0, forwardMovement);
                movement = transform.rotation * movement;
                transform.position += movement;

                if (!Application.isMobilePlatform)
                {
                    mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityPlus.value;
                    mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity / 1.2f * (sensitivityPlus.value / 1.2f);
                }
                if (Application.isMobilePlatform)
                {
                    if (Input.touchCount > 0)
                    {
                        for (int i = 0; i < Input.touchCount; i++)
                        {
                            Touch touch = Input.GetTouch(i);
                            if (!IsTouchOverUI(touch))
                            {
                                if (touch.phase == TouchPhase.Moved)
                                {
                                    mouseX = touch.deltaPosition.x * sensitivityPlus.value;
                                    mouseY = -touch.deltaPosition.y * (sensitivityPlus.value / 1.2f);
                                }
                                else
                                {
                                    mouseX = 0;
                                    mouseY = 0;
                                }
                            }
                        }
                    }
                    if (Input.touchCount == 0)
                    {
                        mouseX = 0;
                        mouseY = 0;
                    }
                }

                horizontalRotation += mouseX;
                verticalRotation += mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

                cameraFPS.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
                transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
                anim.SetBool("sillyDance", false);
                walk = true;
            }
            if (inMenu || chatMan.pechataet) { sideMovement = 0; forwardMovement = 0; vertical = 0; horizontal = 0; camera3d.m_XAxis.m_MaxSpeed = 0; camera3d.m_YAxis.m_MaxSpeed = 0; }
            if (direction == Vector3.zero) { anim.SetBool("running", false); staminaRegen = 0.035f; moveSpeed = moveSpeedNormal; walk = false; StartCoroutine(running()); }
            if (direction != Vector3.zero) { staminaRegen = 0.025f; walk = true; }

            anim.SetBool("isWalk", direction != Vector3.zero);
            if (inMenu) { anim.SetBool("isWalk", false); anim.SetBool("running", false); }

            if (ammoValue == 0 && ammoReload != 0 && weaponID == 1)
            {
                anim.SetBool("reloadRevolver", true);
                if (!isReloading)
                {
                    StartCoroutine(revolverReload());
                    isReloading = true;
                }
            }
        }
        if (weaponID == 0)
        {
            ammoGUI.SetActive(false);
            revolver.SetActive(false);
            float layerWeight = Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10); anim.SetLayerWeight(1, layerWeight);
            float revolverShootLayer = Mathf.Lerp(anim.GetLayerWeight(2), 0f, Time.deltaTime * 10); anim.SetLayerWeight(2, revolverShootLayer);
            float cubegunLayerWeight = Mathf.Lerp(anim.GetLayerWeight(3), 0f, Time.deltaTime * 10); anim.SetLayerWeight(3, cubegunLayerWeight);
            float cubegunActionsLayerWeight = Mathf.Lerp(anim.GetLayerWeight(4), 0f, Time.deltaTime * 10); anim.SetLayerWeight(4, cubegunActionsLayerWeight);
        }
        if (weaponID == 1)
        {
            ammoGUI.SetActive(true);
            revolver.SetActive(true); 
            float layerWeight = Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10); anim.SetLayerWeight(1, layerWeight);
            float revolverShootLayer = Mathf.Lerp(anim.GetLayerWeight(2), 1f, Time.deltaTime * 10); anim.SetLayerWeight(2, revolverShootLayer);
            float cubegunLayerWeight = Mathf.Lerp(anim.GetLayerWeight(3), 0f, Time.deltaTime * 10); anim.SetLayerWeight(3, cubegunLayerWeight);
            float cubegunActionsLayerWeight = Mathf.Lerp(anim.GetLayerWeight(4), 0f, Time.deltaTime * 10); anim.SetLayerWeight(4, cubegunActionsLayerWeight);
        }
        if (weaponID == 2)
        {
            ammoGUI.SetActive(true);
            float cubegunLayerWeight = Mathf.Lerp(anim.GetLayerWeight(3), 1f, Time.deltaTime * 10); anim.SetLayerWeight(3, cubegunLayerWeight);
            float cubegunActionsLayerWeight = Mathf.Lerp(anim.GetLayerWeight(4), 1f, Time.deltaTime * 10); anim.SetLayerWeight(4, cubegunActionsLayerWeight);
            float layerWeight = Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10); anim.SetLayerWeight(1, layerWeight);
            float revolverShootLayer = Mathf.Lerp(anim.GetLayerWeight(2), 0f, Time.deltaTime * 10); anim.SetLayerWeight(2, revolverShootLayer);
            if (ammoValue != 0) { anim.SetBool("cubegunEmpty", false); canShoot = true; }

            if (cubegunMaterial == "green") { cubegunCube.material = green; }
            if (cubegunMaterial == "black") { cubegunCube.material = black; }
            if (cubegunMaterial == "yellow") { cubegunCube.material = yellow; }
            if (cubegunMaterial == "cloud") { cubegunCube.material = cloud; }
            if (cubegunMaterial == "blue") { cubegunCube.material = blue; }
            if (cubegunMaterial == "bricks") { cubegunCube.material = bricks; }
            if (cubegunMaterial == "red") { cubegunCube.material = red; }
            if (cubegunMaterial == "wood") { cubegunCube.material = wood; }
            if (cubegunMaterial == "cement") { cubegunCube.material = cement; }
            if (cubegunMaterial == "embrassedMetal") { cubegunCube.material = embrassedMetal; }
        }
        if (chatMan.disconnected && view.IsMine) { PhotonNetwork.LeaveRoom(); SceneManager.LoadScene("testMenu"); }
    }
    private void LateUpdate()
    {
        foreach (var nicks in GameObject.FindGameObjectsWithTag("nickname"))
        {
            if (view.IsMine)
            {
                if (FirstPS) { nicks.transform.rotation = cameraFPS.transform.rotation; }
                if (!FirstPS) { nicks.transform.rotation = cameraTPV.transform.rotation; }
            }
        }
    }
    public void cameraViewChange()
    {
        if (FirstPS)
        {
            camera3d.enabled = false;
            cameraFPS.SetActive(true);
            cameraTPV.SetActive(false);

            cubegunStart.rotation = cameraFPS.transform.rotation;

            //keramich.color = new Color(1, 1, 1, 0);

            //vasyachorn.color = new Color(1, 1, 1, 0);
            //vasyajolti.color = new Color(1, 1, 1, 0);
            //vasyakrasni.color = new Color(1, 1, 1, 0);

            //MicalJolti.color = new Color(1, 1, 1, 0);
            //MicalKrasn.color = new Color(1, 1, 1, 0);
            //MicalChorn.color = new Color(1, 1, 1, 0);
            //MicalSvetloOranj.color = new Color(1, 1, 1, 0);
            //MicalTemnoKrasn.color = new Color(1, 1, 1, 0);
            //MicalBeton.color = new Color(1, 1, 1, 0);

            //nosokWolf.color = new Color(1, 1, 1, 0);

            if (connectToServer.skinID == 1)
            {
                Material[] creeperHalatmaterials = creeperHalatHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < creeperHalatmaterials.Length; i++)
                {
                    creeperHalatmaterials[i] = shadowCastMaterial;
                }
                creeperHalatHead.GetComponent<Renderer>().materials = creeperHalatmaterials;
            }

            if (connectToServer.skinID == 2)
            {
                Material[] vasyaPolicematerials = vasyaPoliceHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < vasyaPolicematerials.Length; i++)
                {
                    vasyaPolicematerials[i] = shadowCastMaterial;
                }
                vasyaPoliceHead.GetComponent<Renderer>().materials = vasyaPolicematerials;
            }

            if (connectToServer.skinID == 3)
            {
                Material[] greyTigermaterials = greyTigerHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < greyTigermaterials.Length; i++)
                {
                    greyTigermaterials[i] = shadowCastMaterial;
                }
                greyTigerHead.GetComponent<Renderer>().materials = greyTigermaterials;
            }

            if (connectToServer.skinID == 4)
            {
                Material[] skeplorsikmaterials = skeplorsikHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < skeplorsikmaterials.Length; i++)
                {
                    skeplorsikmaterials[i] = shadowCastMaterial;
                }
                skeplorsikHead.GetComponent<Renderer>().materials = skeplorsikmaterials;
            }

            if (connectToServer.skinID == 5)
            {
                Material[] michalmaterials = michalHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < michalmaterials.Length; i++)
                {
                    michalmaterials[i] = shadowCastMaterial;
                }
                michalHead.GetComponent<Renderer>().materials = michalmaterials;
            }

            if (connectToServer.skinID == 6)
            {
                Material[] nosokmaterials = nosokHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < nosokmaterials.Length; i++)
                {
                    nosokmaterials[i] = shadowCastMaterial;
                }
                nosokHead.GetComponent<Renderer>().materials = nosokmaterials;
            }

            if (connectToServer.skinID == 7)
            {
                Material[] cayborgmaterials = cayborgHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < cayborgmaterials.Length; i++)
                {
                    cayborgmaterials[i] = shadowCastMaterial;
                }
                cayborgHead.GetComponent<Renderer>().materials = cayborgmaterials;
            }

            if (connectToServer.skinID == 8)
            {
                Material[] mrtemchikmaterials = mrTemchikHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < mrtemchikmaterials.Length; i++)
                {
                    mrtemchikmaterials[i] = shadowCastMaterial;
                }
                mrTemchikHead.GetComponent<Renderer>().materials = mrtemchikmaterials;
            }

            if (connectToServer.skinID == 9)
            {
                Material[] policenataliematerials = policeNatalieHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < policenataliematerials.Length; i++)
                {
                    policenataliematerials[i] = shadowCastMaterial;
                }
                policeNatalieHead.GetComponent<Renderer>().materials = policenataliematerials;
            }

            if (connectToServer.skinID == 10)
            {
                Material[] androidmaterials = androidHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < androidmaterials.Length; i++)
                {
                    androidmaterials[i] = shadowCastMaterial;
                }
                androidHead.GetComponent<Renderer>().materials = androidmaterials;
            }

            if (connectToServer.skinID == 11)
            {
                Material[] springtrapmaterials = springtrapHead.GetComponent<Renderer>().materials;
                for (int i = 0; i < springtrapmaterials.Length; i++)
                {
                    springtrapmaterials[i] = shadowCastMaterial;
                }
                springtrapHead.GetComponent<Renderer>().materials = springtrapmaterials;
            }
        }
        if (!FirstPS)
        {
            camera3d.enabled = true;
            cameraFPS.SetActive(false);
            cameraTPV.SetActive(true);

            cubegunStart.rotation = cameraTPV.transform.rotation;

            //keramich.color = new Color(1, 1, 1, 1);

            //vasyachorn.color = new Color(1, 1, 1, 1);
            //vasyajolti.color = new Color(1, 1, 1, 1);
            //vasyakrasni.color = new Color(1, 1, 1, 1);

            //MicalJolti.color = new Color(1, 1, 1, 1);
            //MicalKrasn.color = new Color(1, 1, 1, 1);
            //MicalChorn.color = new Color(1, 1, 1, 1);
            //MicalSvetloOranj.color = new Color(1, 1, 1, 1);
            //MicalTemnoKrasn.color = new Color(1, 1, 1, 1);
            //MicalBeton.color = new Color(1, 1, 1, 1);

            //nosokWolf.color = new Color(1, 1, 1, 1);

            if (connectToServer.skinID == 1) creeperHalatHead.GetComponent<Renderer>().materials = creeperHalatMaterials;
            if (connectToServer.skinID == 2) vasyaPoliceHead.GetComponent<Renderer>().materials = vasyaPoliceMaterials;
            if (connectToServer.skinID == 3) greyTigerHead.GetComponent<Renderer>().materials = greyTigerMaterials;
            if (connectToServer.skinID == 4) skeplorsikHead.GetComponent<Renderer>().materials = skeplorsikMaterials;
            if (connectToServer.skinID == 5) michalHead.GetComponent<Renderer>().materials = michalMaterials;
            if (connectToServer.skinID == 6) nosokHead.GetComponent<Renderer>().materials = nosokMaterials;
            if (connectToServer.skinID == 7) cayborgHead.GetComponent<Renderer>().materials = cayborgMaterials;
            if (connectToServer.skinID == 8) mrTemchikHead.GetComponent<Renderer>().materials = mrTemchikMaterials;
            if (connectToServer.skinID == 9) policeNatalieHead.GetComponent<Renderer>().materials = policeNatalieMaterials;
            if (connectToServer.skinID == 10) androidHead.GetComponent<Renderer>().materials = androidMaterials;
            if (connectToServer.skinID == 11) springtrapHead.GetComponent<Renderer>().materials = springtrapMaterials;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("revolverSpawn")) 
        { 
            weaponID = 1;
            canShoot = true;
            ammoValue = 6;
            ammoReload = 18;
        }
        if (other.CompareTag("cubegunSpawn"))
        {
            weaponID = 2;
            canShoot = true;
            ammoValue = 1;
            ammoReload = 29;
            cubegunMaterial = other.GetComponent<weaponRotating>().cubeMaterial;
        }
        if (other.CompareTag("logic"))
        {
            canInteract = true;
            logicObject = other.gameObject;
            telikLogic = logicObject.GetComponent<telikVrubanie>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
        logicObject = null;
        telikLogic = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.CompareTag("revolverBullet") && !inShield) { hp -= 20; }
    }
    [PunRPC] public void TakeDamageRevolver() { if (!inShield) { hp -= 20; } }
    [PunRPC] public void KickPlayerRPC(string reason) { if (view.IsMine) { PlayerPrefs.SetString("kicked", "yes"); PlayerPrefs.SetString("reason", reason); } chatMan.unsubscribeFromChat(new string[] { PhotonNetwork.CurrentRoom.Name }); }
    [PunRPC] public void BanPlayerRPC(string reason) { if (view.IsMine) { PlayerPrefs.SetString("reason", reason); PlayerPrefs.SetString("type", "ban"); } chatMan.unsubscribeFromChat(new string[] { PhotonNetwork.CurrentRoom.Name }); }
    //[PunRPC] public void ReceiveMessage(string sender, string message) { chatMan.chatDisplay.text += sender + ": " + message + "\n"; }
    public IEnumerator jboost() { yield return new WaitForSeconds(.5f); canBoost = false; jumpBoost = 0f; }
    public IEnumerator running() { if (view.IsMine) { yield return null; } }
    public IEnumerator revolverShoot() { yield return new WaitForSeconds(.1f); anim.SetBool("revolverShoot", false); yield return new WaitForSeconds(.2f); canShoot = true; }
    public IEnumerator cubegunShoot() { ammoValue -= 1; yield return new WaitForSeconds(.02f); if (ammoReload != 0) { anim.SetBool("cubegunEmpty", false); yield return new WaitForSeconds(.15f); ammoReload -= 1; ammoValue = 1; canShoot = true; } }
    public IEnumerator osmotrRevReset() { yield return new WaitForSeconds(.1f); anim.SetBool("osmotrRevolver", false); }
    public IEnumerator revolverReload() 
    {
        if (view.IsMine)
        {
            yield return new WaitForSeconds(.1f);
            anim.SetBool("reloadRevolver", false);
            yield return new WaitForSeconds(2.1f);
            byte ammoCheck = (byte)(ammoValue + ammoReload);
            if (ammoReload < 6)
            {
                if (ammoValue + ammoReload > 6)
                {
                    byte ammoToRemove = (byte)(6 - ammoValue);
                    ammoValue += ammoToRemove;
                    ammoReload -= ammoToRemove;
                }
                if (ammoValue + ammoReload <= 6)
                {
                    ammoValue += ammoReload;
                    ammoReload = 0;
                }
            }
            if (ammoReload >= 6)
            {
                byte ammoToRemove = (byte)(6 - ammoValue);
                ammoValue += ammoToRemove;
                ammoReload -= ammoToRemove;
            }
            anim.SetBool("reloadRevolver", false);
            isReloading = false;
        }
    }
    public IEnumerator respawnTiming() { yield return new WaitForSeconds(4); if (died) { canRespawn = true; } }
    void revolverFire()
    {
        if (!adminKick && !adminBan)
        {
            GameObject bullet = PhotonNetwork.Instantiate(revolverBullet.name, bulletStart.position, bulletStart.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
        }
        if (adminKick && !adminBan)
        {
            GameObject bullet = PhotonNetwork.Instantiate(kickBullet.name, bulletStart.position, bulletStart.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
        }
        if (!adminKick && adminBan)
        {
            GameObject bullet = PhotonNetwork.Instantiate(banBullet.name, bulletStart.position, bulletStart.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
        }
        //rb.velocity = cameraFPS.transform.forward * 50f;
        verticalRotation -= 5;
    }
    void cubegunFire()
    {
        if (cubegunMaterial == "green")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletGreen.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "black")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletBlack.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "yellow")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletYellow.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "cloud")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletCloud.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "blue")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletBlue.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "bricks")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletBricks.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "red")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletRed.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "wood")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletWood.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "cement")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletCement.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        if (cubegunMaterial == "embrassedMetal")
        {
            GameObject cube = PhotonNetwork.Instantiate(cubegunBulletEmbrassedMetal.name, cubegunStart.position, cubegunStart.rotation);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
        }
        //rb.velocity = cameraFPS.transform.forward * 50f;
        verticalRotation -= 1;
    }
    void PlayerRespawn()
    {
        if (died) { hp = 100; staminaValue = maxStaminaValue; camera3d.Follow = gameObject.transform; gameObject.transform.position = spawnPlace.transform.position; gameObject.transform.rotation = spawnPlace.transform.rotation; rb.isKinematic = true; canRespawn = true; inShield = true; }
    }
    public void buttonPlay() { justConnected = false; inMenu = false; if (died && canRespawn) { died = false; canRespawn = false; } if (spawnPlaces.Length == 0) { spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)]; } }
    public void buttonExit() { chatMan.unsubscribeFromChat(new string[] { PhotonNetwork.CurrentRoom.Name }); }
    public void settingsOpen() { if (view.IsMine) { settingsPopup.SetActive(true); menuButtons.SetActive(false); inSettings = true; } }
    public void settingsClose() { if (view.IsMine) { settingsPopup.SetActive(false); menuButtons.SetActive(true); PlayerPrefs.SetFloat("sensitivityPlus", sensitivityPlus.value); inSettings = false; } }
    public void respawnButton() { if (canRespawn) { PlayerRespawn(); died = false; canRespawn = false; } if (spawnPlaces.Length != 1) { spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)]; } }
    public void bottomRespawnButton() { inMenu = true; deathScreenAnim.SetBool("died", false); PlayerRespawn(); if (spawnPlaces.Length != 1) { if (!canRespawn) { spawnPlace = spawnPlaces[Random.Range(0, spawnPlaces.Length)]; } } }

    //mobile controls

    public void changeStateOfTelik()
    {
        if (view.IsMine)
        {
            if (telikLogic != null)
            {
                telikLogic.isPlaying = !telikLogic.isPlaying;
            }
        }
    }

    private bool IsTouchOverUI(Touch touch)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touch.position
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("ignoreTouch") || result.gameObject.CompareTag("joystickObject"))
            {
                return true;
            }
        }
        return false;
    }
    public void shootButtonDown()
    {
        shootMobile = true;
    }
    public void shootButtonUp()
    {
        shootMobile = false;
        if (weaponID == 2 && canShoot && ammoValue != 0 && !isReloading && !cubegunCanRemoveBlock && !inMenu) { anim.SetBool("cubegunEmpty", true); StartCoroutine(cubegunShoot()); canShoot = false; cubegunFire(); anim.SetBool("cubegunReadyToRemove", false); cubegunCanRemoveBlock = false; cubegunClearTime = 0f; }
        if (weaponID == 2 && cubegunCanRemoveBlock)
        {
            Ray ray = new Ray(cameraFPS.transform.position, cameraFPS.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("cubegunCube"))
                {
                    PhotonView hitPhotonView = hit.collider.GetComponent<PhotonView>();
                    if (hitPhotonView != null && hitPhotonView.IsMine) { PhotonNetwork.Destroy(hit.collider.gameObject); }
                    if (hitPhotonView != null && !hitPhotonView.IsMine) { hitPhotonView.RPC("RequestDestroyObject", hitPhotonView.Owner, hitPhotonView.ViewID); hit.collider.gameObject.SetActive(false); }
                    if (ammoValue != 0 && ammoReload != 0) { ammoReload += 1; }
                    if (ammoValue != 0 && ammoReload == 0) { ammoReload += 1; }
                    if (ammoValue == 0 && ammoReload == 0) { ammoValue = 1; }
                }
            }
            anim.SetBool("cubegunReadyToRemove", false);
            cubegunCanRemoveBlock = false;
            cubegunClearTime = 0f;
        }
        if (cubegunClearTime < 1f) { cubegunClearTime = 0; }
    }

    public void jumpButtonDown()
    {
        if (canBoost)
        {
            anim.SetBool("jumped", true);
            jumpBoost += jumpBoostMultiplier;
            rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
        }
    }
    public void jumpButtonUp()
    {
        canBoost = false;
        jumpBoost = 0f;
    }

    public void crouchButtonDown()
    {
        anim.SetBool("crouch", true); moveSpeed = moveSpeedCrouch; crouching = true; wantStand = false; standingHead.enabled = false; standingTors.enabled = false; crouchingHead.enabled = true; crouchingTors.enabled = true;
    }
    public void crouchButtonUp()
    {
        wantStand = true;
    }

    public void runningButtonDown()
    {
        isRunningMobile = true;
    }
    public void runningButtonUp()
    {
        anim.SetBool("running", false); moveSpeed = moveSpeedNormal; StartCoroutine(running());
        isRunningMobile = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress == runningButton)
        {
            if (staminaValue > 0.5f && !crouching && walk) { anim.SetBool("running", true); moveSpeed = moveSpeedRun; staminaValue -= staminaMinus; recentlyRun = false; }
        }
        if (eventData.pointerPress == jumpButton)
        {
            if (canBoost)
            {
                anim.SetBool("jumped", true);
                jumpBoost += jumpBoostMultiplier;
                rb.AddForce(Vector3.up * jumpBoost, ForceMode.Impulse);
            }
        }
        if (eventData.pointerPress == crouchButton)
        {
            anim.SetBool("crouch", true); moveSpeed = moveSpeedCrouch; crouching = true; wantStand = false; standingHead.enabled = false; standingTors.enabled = false; crouchingHead.enabled = true; crouchingTors.enabled = true;
        }
        if (eventData.pointerPress == weaponShootButton)
        {
            if (weaponID == 1 && canShoot && ammoValue != 0 && !isReloading) { anim.SetBool("revolverShoot", true); StartCoroutine(revolverShoot()); canShoot = false; revolverFire(); anim.SetBool("osmotrRevolver", false); ammoValue -= 1; }
            if (cubegunClearTime < 1f) { cubegunClearTime += .02f; }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerPress == runningButton)
        {
            anim.SetBool("running", false); moveSpeed = moveSpeedNormal; StartCoroutine(running());
        }
        if (eventData.pointerPress == jumpButton)
        {
            canBoost = false;
            jumpBoost = 0f;
        }
        if (eventData.pointerPress == crouchButton)
        {
            wantStand = true;
        }
        if (eventData.pointerPress == weaponShootButton)
        {
            if (weaponID == 2 && canShoot && ammoValue != 0 && !isReloading && !cubegunCanRemoveBlock && !inMenu) { anim.SetBool("cubegunEmpty", true); StartCoroutine(cubegunShoot()); canShoot = false; cubegunFire(); anim.SetBool("cubegunReadyToRemove", false); cubegunCanRemoveBlock = false; cubegunClearTime = 0f; }
            if (weaponID == 2 && cubegunCanRemoveBlock)
            {
                Ray ray = new Ray(cubegunStart.transform.position, cubegunStart.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("cubegunCube"))
                    {
                        PhotonView hitPhotonView = hit.collider.GetComponent<PhotonView>();
                        if (hitPhotonView != null && hitPhotonView.IsMine) { PhotonNetwork.Destroy(hit.collider.gameObject); }
                        if (hitPhotonView != null && !hitPhotonView.IsMine) { hitPhotonView.RPC("RequestDestroyObject", hitPhotonView.Owner, hitPhotonView.ViewID); hit.collider.gameObject.SetActive(false); }
                        if (ammoValue != 0 && ammoReload != 0) { ammoReload += 1; }
                        if (ammoValue != 0 && ammoReload == 0) { ammoReload += 1; }
                        if (ammoValue == 0 && ammoReload == 0) { ammoValue = 1; }
                    }
                }
                anim.SetBool("cubegunReadyToRemove", false);
                cubegunCanRemoveBlock = false;
                cubegunClearTime = 0f;
            }
            if (cubegunClearTime < 1f) { cubegunClearTime = 0; }
        }
    }
    public void mobileJumpButton() 
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("jumped", true);
            anim.SetBool("sillyDance", false);
            canBoost = true;
            StartCoroutine(jboost());
        }
    }
    public void mobileChangeViewCamera() { FirstPS = !FirstPS; }
    public void mobileResetCharacter() { hp = 0; }
    public void mobileDanceButton() { anim.SetBool("sillyDance", true); }
    public void mobileWeaponOsmotr() 
    { 
        if (weaponID == 1) 
        { 
            anim.SetBool("osmotrRevolver", true); 
            StartCoroutine(osmotrRevReset()); 
        }
        if (gameModifiers.flashlightEnabled) 
        { 
            flashlightActions.SetBool("on", !flashlightActions.GetBool("on")); 
        }
    }
    public void mobileWeaponThrow() { weaponID = 0; }
    public void mobileWeaponReload() 
    {
        if (weaponID == 1 && ammoValue != 6 && ammoReload != 0)
        {
            anim.SetBool("reloadRevolver", true);
            if (!isReloading)
            {
                StartCoroutine(revolverReload());
                isReloading = true;
            }
        }
    }
}