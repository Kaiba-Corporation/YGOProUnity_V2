using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostCustom : MonoBehaviour {

    public UIButton hostBtn;
    public UIButton backBtn;

    public UIInput startingLpTxt;
    public UIInput startingHandTxt;
    public UIInput cardsPerDrawTxt;

    public UIToggle dontRecoverTimeChk;
    public UIToggle dontShuffleDeckChk;
    public UIToggle dontCheckDeckChk;

    public UIPopupList banlistCmb;
    public UIPopupList regionCmb;
    public UIPopupList duelModeCmb;
    public UIPopupList masterRulesCmb;

    void Start () {
        UIHelper.registEvent(gameObject, "btn_host", OnHost);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
        UIHelper.registEvent(gameObject, "cmbox_duel_mode", OnDuelMode);
    }

    private void OnHost()
    {
        string gameName = "0";

        if (regionCmb.value == "TCG/OCG" && banlistCmb.value == "TCG")
            gameName += "0";
        else if (regionCmb.value == "TCG/OCG" && banlistCmb.value == "OCG")
            gameName += "1";
        else if (regionCmb.value == "TCG/OCG" && banlistCmb.value == "TRADITIONAL")
            gameName += "2";
        else if (regionCmb.value == "TCG/OCG" && banlistCmb.value == "NO BANLIST")
            gameName += "3";
        else if (regionCmb.value == "TCG" && banlistCmb.value == "TCG")
            gameName += "4";
        else if (regionCmb.value == "TCG" && banlistCmb.value == "OCG")
            gameName += "5";
        else if (regionCmb.value == "TCG" && banlistCmb.value == "TRADITIONAL")
            gameName += "6";
        else if (regionCmb.value == "TCG" && banlistCmb.value == "NO BANLIST")
            gameName += "7";
        else if (regionCmb.value == "OCG" && banlistCmb.value == "TCG")
            gameName += "8";
        else if (regionCmb.value == "OCG" && banlistCmb.value == "OCG")
            gameName += "9";
        else if (regionCmb.value == "OCG" && banlistCmb.value == "TRADITIONAL")
            gameName += "A";
        else if (regionCmb.value == "OCG" && banlistCmb.value == "NO BANLIST")
            gameName += "B";

        if (duelModeCmb.value == "SINGLE DUEL")
            gameName += "0";
        else if (duelModeCmb.value == "MATCH DUEL")
            gameName += "1";
        else if (duelModeCmb.value == "TAG DUEL")
            gameName += "2";

        if (masterRulesCmb.value == "RULE 1 (ORIGINAL)")
            gameName += "1";
        else if (masterRulesCmb.value == "RULE 2 (SYNCHRO)")
            gameName += "2";
        else if (masterRulesCmb.value == "RULE 3 (PENDULUM)")
            gameName += "3";
        else if (masterRulesCmb.value == "RULE 4 (LINKS)")
            gameName += "4";
        else if (masterRulesCmb.value == "RULE 5 (APRIL 2020)")
            gameName += "5";

        int duelSettings = 0;
        if (dontCheckDeckChk.value) duelSettings += 1;
        if (dontShuffleDeckChk.value) duelSettings += 2;
        if (dontRecoverTimeChk.value) duelSettings += 4;

        gameName += duelSettings.ToString();

        int startingLp = 8000;
        int startingHand = 5;
        int cardsPerDraw = 1;

        try { startingLp = Convert.ToInt32(startingLpTxt.value); } catch { InvalidLifePoints(); return; }
        try { startingHand = Convert.ToInt32(startingHandTxt.value); } catch { InvalidStartingHand(); return; }
        try { cardsPerDraw = Convert.ToInt32(cardsPerDrawTxt.value); } catch { InvalidCardsPerDraw(); return; }

        string startingLpStr, startingHandStr, cardsPerDrawStr;

        if (startingLp > 0 && startingLp < 1000000000)
            startingLpStr = startingLp.ToString();
        else
        {
            InvalidLifePoints();
            return;
        }

        if (startingHand >= 0 && startingHand <= 9) startingHandStr = startingHand.ToString();
        else if (startingHand == 10) startingHandStr = "A";
        else if (startingHand == 11) startingHandStr = "B";
        else if (startingHand == 12) startingHandStr = "C";
        else if (startingHand == 13) startingHandStr = "D";
        else if (startingHand == 14) startingHandStr = "E";
        else if (startingHand == 15) startingHandStr = "F";
        else { InvalidStartingHand(); return; }

        if (cardsPerDraw >= 0 && cardsPerDraw <= 9) cardsPerDrawStr = cardsPerDraw.ToString();
        else if (cardsPerDraw == 10) cardsPerDrawStr = "A";
        else if (cardsPerDraw == 11) cardsPerDrawStr = "B";
        else if (cardsPerDraw == 12) cardsPerDrawStr = "C";
        else if (cardsPerDraw == 13) cardsPerDrawStr = "D";
        else if (cardsPerDraw == 14) cardsPerDrawStr = "E";
        else if (cardsPerDraw == 15) cardsPerDrawStr = "F";
        else { InvalidCardsPerDraw(); return; }

        gameName += startingHandStr + cardsPerDrawStr + "," + startingLpStr + ",";
        gameName += Utils.GetRandomString(19 - gameName.Length);

        Program.I().tdoane.hostCustomForm.SetActive(false);
        Program.I().selectServer.joinGame(Program.I().tdoane.Username, Program.I().tdoane.IP, Program.I().tdoane.GamePort.ToString(), gameName);
    }

    private void OnBack()
    {
        Program.I().tdoane.hostCustomForm.SetActive(false);
        Program.I().tdoane.ShowGameList();
    }

    private void OnDuelMode()
    {
        if (duelModeCmb.value == "TAG DUEL")
            startingLpTxt.value = "16000";
        else
            startingLpTxt.value = "8000";
    }

    private void InvalidLifePoints()
    {
        Program.I().tdoane.hostCustomForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("INVALID LIFE POINTS", "Please enter a valid life points value, your life points must me between 0 and 1000000000", "Host Custom");
    }

    private void InvalidStartingHand()
    {
        Program.I().tdoane.hostCustomForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("INVALID STARTING HAND", "Please enter a valid starting hand value, your starting hand must be between 0 and 15", "Host Custom");
    }

    private void InvalidCardsPerDraw()
    {
        Program.I().tdoane.hostCustomForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("INVALID CARDS PER DRAW", "Please enter a valid cards per draw value, your starting hand must be between 0 and 15", "Host Custom");
    }
}
