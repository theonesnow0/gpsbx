#nullable disable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuPlayerSkin : MonoBehaviour
{
    public GameObject creeperHalatik;
    public GameObject vasyaPolice;
    public GameObject greyTiger;
    public GameObject redRobot;
    public GameObject michal;
    public GameObject nosok;
    public GameObject cayborg;
    public GameObject mrTemchik;
    public GameObject policeNatalie;
    public GameObject android;
    public GameObject springtrap;
    void Update()
    {
        if (connectToServer.skinID == 1) { creeperHalatik.SetActive(true); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 2) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(true); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 3) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(true); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 4) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(true); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 5) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(true); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 6) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(true); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 7) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(true); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 8) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(true); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 9) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(true); android.SetActive(false); springtrap.SetActive(false); }
        if (connectToServer.skinID == 10) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(true); springtrap.SetActive(false); }
        if (connectToServer.skinID == 11) { creeperHalatik.SetActive(false); vasyaPolice.SetActive(false); greyTiger.SetActive(false); redRobot.SetActive(false); michal.SetActive(false); nosok.SetActive(false); cayborg.SetActive(false); mrTemchik.SetActive(false); policeNatalie.SetActive(false); android.SetActive(false); springtrap.SetActive(true); }
    }
}
