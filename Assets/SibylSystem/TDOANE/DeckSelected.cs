using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelected : MonoBehaviour {

    public int deckId;

    public UIButton claimBtn;
    public UIButton refuseBtn;

    public UILabel deckNameLbl;
    public UILabel diamondsLbl;

    void Start () {
        UIHelper.registEvent(gameObject, "btn_claim", OnClaim);
        UIHelper.registEvent(gameObject, "btn_refuse", OnRefuse);
    }

    public void Load(string[] message)
    {
        deckId = Convert.ToInt32(message[1]);
        deckNameLbl.text = message[2];
        diamondsLbl.text = message[7];
    }

    public void OnClaim()
    {
        Program.I().tdoane.client.Send("ClaimDeckReward<{]>" + deckId.ToString());
    }

    public void OnRefuse()
    {
        Program.I().tdoane.client.Send("ClaimDeckReward<{]>" + deckId.ToString());
    }
}
