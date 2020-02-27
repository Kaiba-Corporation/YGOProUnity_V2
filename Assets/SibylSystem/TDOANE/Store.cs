using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {

    public UILabel diamondsLbl;
    public UITexture avatarDiamondsPic;
    public UILabel avatarDiamondsLbl;
    public UILabel avatarBuyLbl;
    public UIButton buyAvatarBtn;
    public UITexture cardBackDiamondsPic;
    public UILabel cardBackDiamondsLbl;
    public UILabel cardBackBuyLbl;
    public UIButton buyCardBackBtn;
    public UITexture avatarCardBackDiamondsPic;
    public UILabel avatarCardBackDiamondsLbl;
    public UILabel avatarCardBackBuyLbl;
    public UIButton buyAvatarCardBackBtn;
    public UIButton backBtn;
    public UIButton donateBtn;

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_buy_avatar", OnBuyAvatar);
        UIHelper.registEvent(gameObject, "btn_buy_card_back", OnBuyCardBack);
        UIHelper.registEvent(gameObject, "btn_buy_avatar_card_back", OnBuyAvatarCardBack);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
        UIHelper.registEvent(gameObject, "btn_donate", OnDonate);
    }

    public void LoadStore()
    {
        if (Program.I().tdoane.AvatarItem && !Program.I().tdoane.CardBackItem)
        {
            buyAvatarBtn.enabled = false;
            buyAvatarCardBackBtn.enabled = false;

            avatarDiamondsPic.enabled = false;
            avatarDiamondsLbl.enabled = false;
            avatarCardBackDiamondsPic.enabled = false;
            avatarCardBackDiamondsLbl.enabled = false;

            avatarBuyLbl.text = "OWNED";
            avatarCardBackBuyLbl.text = "UNAVAILABLE";

            buyAvatarBtn.GetComponent<UISprite>().color = Color.magenta;
            buyAvatarCardBackBtn.GetComponent<UISprite>().color = Color.red;

            avatarBuyLbl.transform.localPosition = new Vector3(avatarBuyLbl.transform.localPosition.x, avatarBuyLbl.transform.localPosition.y + 30, avatarBuyLbl.transform.localPosition.z);
            avatarCardBackBuyLbl.transform.localPosition = new Vector3(avatarCardBackBuyLbl.transform.localPosition.x, avatarCardBackBuyLbl.transform.localPosition.y + 30, avatarCardBackBuyLbl.transform.localPosition.z);
        }
        else if (!Program.I().tdoane.AvatarItem && Program.I().tdoane.CardBackItem)
        {
            buyCardBackBtn.enabled = false;
            buyAvatarCardBackBtn.enabled = false;

            cardBackDiamondsPic.enabled = false;
            cardBackDiamondsLbl.enabled = false;
            avatarCardBackDiamondsPic.enabled = false;
            avatarCardBackDiamondsLbl.enabled = false;

            cardBackBuyLbl.text = "OWNED";
            avatarCardBackBuyLbl.text = "UNAVAILABLE";

            buyCardBackBtn.GetComponent<UISprite>().color = Color.magenta;
            buyAvatarCardBackBtn.GetComponent<UISprite>().color = Color.red;

            cardBackBuyLbl.transform.localPosition = new Vector3(cardBackBuyLbl.transform.localPosition.x, cardBackBuyLbl.transform.localPosition.y + 30, cardBackBuyLbl.transform.localPosition.z);
            avatarCardBackBuyLbl.transform.localPosition = new Vector3(avatarCardBackBuyLbl.transform.localPosition.x, avatarCardBackBuyLbl.transform.localPosition.y + 30, avatarCardBackBuyLbl.transform.localPosition.z);
        }
        else if (Program.I().tdoane.AvatarItem && Program.I().tdoane.CardBackItem)
        {
            buyAvatarBtn.enabled = false;
            buyCardBackBtn.enabled = false;
            buyAvatarCardBackBtn.enabled = false;

            avatarDiamondsPic.enabled = false;
            avatarDiamondsLbl.enabled = false;
            cardBackDiamondsPic.enabled = false;
            cardBackDiamondsLbl.enabled = false;
            avatarCardBackDiamondsPic.enabled = false;
            avatarCardBackDiamondsLbl.enabled = false;

            avatarBuyLbl.text = "OWNED";
            cardBackBuyLbl.text = "OWNED";
            avatarCardBackBuyLbl.text = "OWNED";

            buyAvatarBtn.GetComponent<UISprite>().color = Color.magenta;
            buyCardBackBtn.GetComponent<UISprite>().color = Color.magenta;
            buyAvatarCardBackBtn.GetComponent<UISprite>().color = Color.magenta;

            avatarBuyLbl.transform.localPosition = new Vector3(avatarBuyLbl.transform.localPosition.x, avatarBuyLbl.transform.localPosition.y + 30, avatarBuyLbl.transform.localPosition.z);
            cardBackBuyLbl.transform.localPosition = new Vector3(cardBackBuyLbl.transform.localPosition.x, cardBackBuyLbl.transform.localPosition.y + 30, cardBackBuyLbl.transform.localPosition.z);
            avatarCardBackBuyLbl.transform.localPosition = new Vector3(avatarCardBackBuyLbl.transform.localPosition.x, avatarCardBackBuyLbl.transform.localPosition.y + 30, avatarCardBackBuyLbl.transform.localPosition.z);
        }
    }

    public void SetDiamonds(string diamonds)
    {
        diamondsLbl.text = diamonds;
        int myDiamonds = Convert.ToInt32(diamonds);

        if (!Program.I().tdoane.AvatarItem && myDiamonds >= 400)
            avatarDiamondsLbl.color = Color.green;
        else if (!Program.I().tdoane.AvatarItem && myDiamonds < 400)
            avatarDiamondsLbl.color = Color.red;

        if (!Program.I().tdoane.CardBackItem && myDiamonds >= 400)
            cardBackDiamondsLbl.color = Color.green;
        else if (!Program.I().tdoane.CardBackItem && myDiamonds < 400)
            cardBackDiamondsLbl.color = Color.red;

        if (!Program.I().tdoane.AvatarItem && !Program.I().tdoane.CardBackItem && myDiamonds >= 700)
            avatarCardBackDiamondsLbl.color = Color.green;
        else if (!Program.I().tdoane.AvatarItem && !Program.I().tdoane.CardBackItem && myDiamonds < 700)
            avatarCardBackDiamondsLbl.color = Color.red;
    }

    void OnBuyAvatar()
    {
        Program.I().tdoane.client.Send("Buy<{]>Avatar<{]>Diamonds");
        Program.I().tdoane.storeForm.SetActive(false);
    }

    void OnBuyCardBack()
    {
        Program.I().tdoane.client.Send("Buy<{]>Card Back<{]>Diamonds");
        Program.I().tdoane.storeForm.SetActive(false);
    }

    void OnBuyAvatarCardBack()
    {
        Program.I().tdoane.client.Send("Buy<{]>Avatar & Card Back<{]>Diamonds");
        Program.I().tdoane.storeForm.SetActive(false);
    }

    void OnBack()
    {
        Program.I().tdoane.storeForm.SetActive(false);
        Program.I().tdoane.profileForm.SetActive(true);
    }

    void OnDonate()
    {
        Program.I().tdoane.storeForm.SetActive(false);
        Program.I().tdoane.ShowDonate();
    }
}
