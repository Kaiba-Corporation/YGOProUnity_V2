using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DuelAI : MonoBehaviour {

    public Texture duelingRobot;
    public Texture yugiMuto;
    public Texture setoKaiba;
    public Texture joeyWheeler;
    public Texture dartz;

    public UIButton duelBtn;
    public UIButton backBtn;

    public UITexture selectedOpponentPic;
    public UITexture opponent1Pic;
    public UITexture opponent2Pic;
    public UITexture opponent3Pic;
    public UITexture opponent4Pic;

    public UILabel selectedOpponentLbl;
    public UILabel opponent1Lbl;
    public UILabel opponent2Lbl;
    public UILabel opponent3Lbl;
    public UILabel opponent4Lbl;

    public UIPopupList masterRulesCmb;
    public UIPopupList selectedDeckCmb;

    void Start () {
        UIHelper.registEvent(gameObject, "btn_duel", OnDuel);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);

        UIHelper.registEvent(gameObject, "pic_opponent1", OnOpponent1);
        UIHelper.registEvent(gameObject, "pic_opponent2", OnOpponent2);
        UIHelper.registEvent(gameObject, "pic_opponent3", OnOpponent3);
        UIHelper.registEvent(gameObject, "pic_opponent4", OnOpponent4);

        UIHelper.registEvent(gameObject, "cmbox_master_rules", GetDeckList);
    }

    private void OnDuel()
    {
        TextInfo mTextInfo = new CultureInfo("en-US", false).TextInfo;
        string deck = mTextInfo.ToTitleCase(selectedDeckCmb.value.ToLower());
        string masterRules = "5";

        if (deck == "Chain Burn" || deck == "Cyber Dragon" || deck == "Dark Magician" || deck == "Gren Maju Thunder Boarder" || deck == "Mokey Mokey"
            || deck == "Mokey Mokey King" || deck == "Sky Striker" || deck == "Toadally Awesome" || deck == "Zexal Weapons")
        {
            deck = deck.Replace(" ", "");
        }
        else if (deck == "Blue-Eyes White Dragon")
            deck = "Blue-Eyes";
        else if (deck == "Blue-Eyes Max Dragon")
            deck = "BlueEyesMaxDragon";
        else if (deck == "Level VIII")
            deck = "Level8";
        else if (deck == "Rank V")
            deck = "Rank5";
        else if (deck == "Shaddoll Dinosaur")
            deck = "LightswornShaddoldinosour";
        else if (deck == "Yugi Muto Deck")
        {
            Program.I().tdoane.client.Send("J.A.R.V.I.S.<{]>Yugi" + Random.Range(1, 6));
            return;
        }
        else if (deck == "Seto Kaiba Deck")
        {
            Program.I().tdoane.client.Send("J.A.R.V.I.S.<{]>Kaiba" + Random.Range(1, 9));
            return;
        }
        else if (deck == "Joey Wheeler Deck")
        {
            Program.I().tdoane.client.Send("J.A.R.V.I.S.<{]>Joey");
            return;
        }
        else if (deck == "Dartz Deck")
        {
            Program.I().tdoane.client.Send("J.A.R.V.I.S.<{]>Dartz");
            return;
        }
        
        Program.I().tdoane.client.Send("DuelingRobot<{]>" + deck + "<{]>" + masterRules + "<{]>0<{]>8000<{]>5<{]>1<{]>3<{]>false");
    }

    private void OnBack()
    {
        Program.I().tdoane.duelAiForm.SetActive(false);
        Program.I().shiftToServant(Program.I().menu);
    }

    private void OnOpponent1()
    {
        ChangeOpponent(1);
    }

    private void OnOpponent2()
    {
        ChangeOpponent(2);
    }

    private void OnOpponent3()
    {
        ChangeOpponent(3);
    }

    private void OnOpponent4()
    {
        ChangeOpponent(4);
    }

    private void ChangeOpponent(int opponent)
    {
        Texture newOpponentPic;
        Texture oldOponentPic = selectedOpponentPic.mainTexture;

        string newOpponentTxt;
        string oldSelectedOpponent = selectedOpponentLbl.text.Replace("SELECTED OPPONENT: ", "");

        if (opponent == 1)
        {
            newOpponentTxt = opponent1Lbl.text;
            newOpponentPic = opponent1Pic.mainTexture;
            opponent1Lbl.text = oldSelectedOpponent;
            opponent1Pic.mainTexture = oldOponentPic;
        }
        else if (opponent == 2)
        {
            newOpponentTxt = opponent2Lbl.text;
            newOpponentPic = opponent2Pic.mainTexture;
            opponent2Lbl.text = oldSelectedOpponent;
            opponent2Pic.mainTexture = oldOponentPic;
        }
        else if (opponent == 3)
        {
            newOpponentTxt = opponent3Lbl.text;
            newOpponentPic = opponent3Pic.mainTexture;
            opponent3Lbl.text = oldSelectedOpponent;
            opponent3Pic.mainTexture = oldOponentPic;
        }
        else
        {
            newOpponentTxt = opponent4Lbl.text;
            newOpponentPic = opponent4Pic.mainTexture;
            opponent4Lbl.text = oldSelectedOpponent;
            opponent4Pic.mainTexture = oldOponentPic;
        }

        if (newOpponentTxt == "DUELING ROBOT")
        {
            masterRulesCmb.GetComponent<UIPopupList>().Clear();

            masterRulesCmb.GetComponent<UIPopupList>().AddItem("RULE 3 (PENDULUM)");
            masterRulesCmb.GetComponent<UIPopupList>().AddItem("RULE 4 (LINKS)");
            masterRulesCmb.GetComponent<UIPopupList>().AddItem("RULE 5 (APRIL 2020)");

            masterRulesCmb.value = "RULE 4 (LINKS)";
        }
        else
        {
            masterRulesCmb.GetComponent<UIPopupList>().Clear();

            masterRulesCmb.GetComponent<UIPopupList>().AddItem("RULE 3 (PENDULUM)");
            
            masterRulesCmb.value = "RULE 3 (PENDULUM)";
        }

        selectedOpponentLbl.text = "SELECTED OPPONENT: " + newOpponentTxt;
        selectedOpponentPic.mainTexture = newOpponentPic;

        GetDeckList();
    }

    private void GetDeckList()
    {
        string selectedOpponent = selectedOpponentLbl.text.Replace("SELECTED OPPONENT: ", "");

        selectedDeckCmb.GetComponent<UIPopupList>().Clear();

        if (selectedOpponent == "DUELING ROBOT")
        {
            if (masterRulesCmb.value == "RULE 3 (PENDULUM)")
            {
                selectedDeckCmb.value = "BLACKWING";
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLACKWING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES MAX DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES WHITE DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CHAIN BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CYBER DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DARK MAGICIAN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DRAGUNITY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("EVILSWARM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("FROG");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("GRAVEKEEPER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("HORUS");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LEVEL VIII");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LIGHTSWORN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY KING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("NEKROZ");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("PHANTASM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("QLIPHORT");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RAINBOW");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RANK V");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SHADDOLL DINOSAUR");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("TOADALLY AWESOME");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("YOSENJU");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ZEXAL WEAPONS");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ZOODIAC");
            }
            else if (masterRulesCmb.value == "RULE 4 (LINKS)")
            {
                selectedDeckCmb.value = "ALTERGEIST";
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ALTERGEIST");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLACKWING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES MAX DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES WHITE DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CHAIN BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CYBER DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DARK MAGICIAN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DRAGUN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DRAGUNITY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("EVILSWARM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("FROG");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("GRAVEKEEPER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("GREN MAJU THUNDER BOARDER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("HORUS");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LEVEL VIII");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LIGHTSWORN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY KING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("NEKROZ");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ORCUST");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("PHANTASM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("QLIPHORT");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RAINBOW");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RANK V");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ST1732");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SALAMANGREAT");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SHADDOLL DINOSAUR");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SKY STRIKER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("TRICKSTAR");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("YOSENJU");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ZEXAL WEAPONS");
            }
            else if (masterRulesCmb.value == "RULE 5 (APRIL 2020)")
            {
                selectedDeckCmb.value = "ALTERGEIST";
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ALTERGEIST");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLACKWING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES MAX DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BLUE-EYES WHITE DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CHAIN BURN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("CYBER DRAGON");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DARK MAGICIAN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DRAGUN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DRAGUNITY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("EVILSWARM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("FROG");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("GRAVEKEEPER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("GREN MAJU THUNDER BOARDER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("HORUS");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LEVEL VIII");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("LIGHTSWORN");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("MOKEY MOKEY KING");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("NEKROZ");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ORCUST");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("PHANTASM");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("QLIPHORT");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RAINBOW");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("RANK V");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ST1732");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SALAMANGREAT");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SHADDOLL DINOSAUR");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SKY STRIKER");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("TOADALLY AWESOME");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("TRICKSTAR");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("YOSENJU");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ZEXAL WEAPONS");
                selectedDeckCmb.GetComponent<UIPopupList>().AddItem("ZOODIAC");
            }
        }
        else if (selectedOpponent == "YUGI MUTO")
        {
            selectedDeckCmb.GetComponent<UIPopupList>().AddItem("YUGI MUTO DECK");
            selectedDeckCmb.value = "YUGI MUTO DECK";
        }
        else if (selectedOpponent == "SETO KAIBA")
        {
            selectedDeckCmb.GetComponent<UIPopupList>().AddItem("SETO KAIBA DECK");
            selectedDeckCmb.value = "SETO KAIBA DECK";
        }
        else if (selectedOpponent == "JOEY WHEELER")
        {
            selectedDeckCmb.GetComponent<UIPopupList>().AddItem("JOEY WHEELER DECK");
            selectedDeckCmb.value = "JOEY WHEELER DECK";
        }
        else if (selectedOpponent == "DARTZ")
        {
            selectedDeckCmb.GetComponent<UIPopupList>().AddItem("DARTZ DECK");
            selectedDeckCmb.value = "DARTZ DECK";
        }
    }
}
