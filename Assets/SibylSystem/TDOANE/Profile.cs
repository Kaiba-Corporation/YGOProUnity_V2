using System;
using System.Net;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour {

    public UITexture avatar;
    public UITexture cardBack;

    public UIButton changeAvatarBtn;
    public UIButton changeCardBackBtn;
    public UIButton okBtn;

    public UIPopupList statisticsCmb;

    public UILabel usernameLbl;
    public UILabel rankLbl;
    public UILabel teamLbl;
    public UILabel levelLbl;
    public UILabel xpLbl;
    public UILabel wpLbl;
    public UILabel goldLbl;
    public UILabel diamondsLbl;
    public UILabel singleRatingLbl;
    public UILabel singleWinLbl;
    public UILabel singleDrawLbl;
    public UILabel singleLostLbl;
    public UILabel matchRatingLbl;
    public UILabel matchWinLbl;
    public UILabel matchDrawLbl;
    public UILabel matchLostLbl;
    public UILabel tagRatingLbl;
    public UILabel tagWinLbl;
    public UILabel tagDrawLbl;
    public UILabel tagLostLbl;

    string imageUrl;
    string cardBackUrl;

    int position = 0;

    string singleRankedRating, singleRankedWin, singleRankedDraw, singleRankedLost;
    string matchRankedRating, matchRankedWin, matchRankedDraw, matchRankedLost;
    string tagRankedRating, tagRankedWin, tagRankedDraw, tagRankedLost;

    string singleUnrankedWin, singleUnrankedDraw, singleUnrankedLost;
    string matchUnrankedWin, matchUnrankedDraw, matchUnrankedLost;
    string tagUnrankedWin, tagUnrankedDraw, tagUnrankedLost;

    void Start()
    {
        UIHelper.registEvent(gameObject, "cmbox_statistics", OnUpdateStatistics);
        UIHelper.registEvent(gameObject, "btn_change_avatar", OnChangeAvatar);
        UIHelper.registEvent(gameObject, "btn_change_card_back", OnChangeCardBack);
        UIHelper.registEvent(gameObject, "btn_ok", OnOk);
    }

    public void Load(string[] message)
    {
        usernameLbl.text = Program.I().tdoane.Username;
        rankLbl.text = Program.I().tdoane.Rank;
        teamLbl.text = Program.I().tdoane.Team;

        int level = 1;
        int totalXp = Convert.ToInt32(message[27]);
        while ((level * 500) < totalXp)
        {
            totalXp = totalXp - (level * 500);
            level = level + 1;
        }

        levelLbl.text = level.ToString();
        xpLbl.text = totalXp + "/" + (level * 500);
        wpLbl.text = Program.I().tdoane.Wp;
        goldLbl.text = message[11];
        diamondsLbl.text = message[41];

        imageUrl = message[9];

        singleRankedRating = message[10];
        singleRankedWin = message[12];
        singleRankedDraw = message[13];
        singleRankedLost = message[14];

        matchRankedRating = message[23];
        matchRankedWin = message[24];
        matchRankedDraw = message[25];
        matchRankedLost = message[26];

        tagRankedRating = message[15];
        tagRankedWin = message[16];
        tagRankedDraw = message[17];
        tagRankedLost = message[18];

        singleUnrankedWin = message[28];
        singleUnrankedDraw = message[29];
        singleUnrankedLost = message[30];

        matchUnrankedWin = message[34];
        matchUnrankedDraw = message[35];
        matchUnrankedLost = message[36];

        tagUnrankedWin = message[31];
        tagUnrankedDraw = message[32];
        tagUnrankedLost = message[33];

        LoadAvatar();
        SetLocalCardBackImage();
        UpdateStatistics();
    }

    public void LoadAvatar()
    {
        if (Program.I().tdoane.AvatarItem && imageUrl != "")
            StartCoroutine(DownloadAvatar());
    }

    public void LoadAvatar(string url)
    {
        imageUrl = url;
        LoadAvatar();
    }

    IEnumerator DownloadAvatar()
    {
        try
        {
            WWW www = new WWW(@"http://ygopro.org/textures.php?link=" + imageUrl + "?v=" + Utils.GetRandomString(10));
            yield return www;
            if (!string.IsNullOrEmpty(www.text))
            {
                Texture2D avatarTexture = new Texture2D(www.texture.width, www.texture.height);
                www.LoadImageIntoTexture(avatarTexture);
                avatar.mainTexture = avatarTexture;
            }
        }
        finally { }
    }

    void SetLocalCardBackImage()
    {
        cardBack.mainTexture = GameTextureManager.myBack;
    }

    public void SetCardBack(string link)
    {
        cardBackUrl = link;
        StartCoroutine(DownloadCardBack());
    }

    IEnumerator DownloadCardBack()
    {
        try
        {
            WWW www = new WWW(@"http://ygopro.org/textures.php?link=" + cardBackUrl + "?v=" + Utils.GetRandomString(10));
            yield return www;
            if (!string.IsNullOrEmpty(www.text))
            {
                Texture2D cardBackTexture = new Texture2D(www.texture.width, www.texture.height);
                www.LoadImageIntoTexture(cardBackTexture);
                cardBack.mainTexture = cardBackTexture;
            }
        }
        finally { }
    }

    void UpdateStatistics()
    {
        if (statisticsCmb.value == "RANKED")
        {
            singleRatingLbl.gameObject.SetActive(true);
            matchRatingLbl.gameObject.SetActive(true);
            tagRatingLbl.gameObject.SetActive(true);

            if (position == 1)
            {
                singleWinLbl.transform.localPosition = new Vector3(singleWinLbl.transform.localPosition.x, singleWinLbl.transform.localPosition.y - 25, singleWinLbl.transform.localPosition.z);
                singleDrawLbl.transform.localPosition = new Vector3(singleDrawLbl.transform.localPosition.x, singleDrawLbl.transform.localPosition.y - 25, singleDrawLbl.transform.localPosition.z);
                singleLostLbl.transform.localPosition = new Vector3(singleLostLbl.transform.localPosition.x, singleLostLbl.transform.localPosition.y - 25, singleLostLbl.transform.localPosition.z);
                matchWinLbl.transform.localPosition = new Vector3(matchWinLbl.transform.localPosition.x, matchWinLbl.transform.localPosition.y + -25, matchWinLbl.transform.localPosition.z);
                matchDrawLbl.transform.localPosition = new Vector3(matchDrawLbl.transform.localPosition.x, matchDrawLbl.transform.localPosition.y - 25, matchDrawLbl.transform.localPosition.z);
                matchLostLbl.transform.localPosition = new Vector3(matchLostLbl.transform.localPosition.x, matchLostLbl.transform.localPosition.y - 25, matchLostLbl.transform.localPosition.z);
                tagWinLbl.transform.localPosition = new Vector3(tagWinLbl.transform.localPosition.x, tagWinLbl.transform.localPosition.y - 25, tagWinLbl.transform.localPosition.z);
                tagDrawLbl.transform.localPosition = new Vector3(tagDrawLbl.transform.localPosition.x, tagDrawLbl.transform.localPosition.y - 25, tagDrawLbl.transform.localPosition.z);
                tagLostLbl.transform.localPosition = new Vector3(tagLostLbl.transform.localPosition.x, tagLostLbl.transform.localPosition.y - 25, tagLostLbl.transform.localPosition.z);

                position = 0;
            }

            singleRatingLbl.text = singleRankedRating;
            singleWinLbl.text = singleRankedWin;
            singleDrawLbl.text = singleRankedDraw;
            singleLostLbl.text = singleRankedLost;
            matchRatingLbl.text = matchRankedRating;
            matchWinLbl.text = matchRankedWin;
            matchDrawLbl.text = matchRankedDraw;
            matchLostLbl.text = matchRankedLost;
            tagRatingLbl.text = tagRankedRating;
            tagWinLbl.text = tagRankedWin;
            tagDrawLbl.text = tagRankedDraw;
            tagLostLbl.text = tagRankedLost;
        }
        else
        {
            singleRatingLbl.gameObject.SetActive(false);
            matchRatingLbl.gameObject.SetActive(false);
            tagRatingLbl.gameObject.SetActive(false);

            if (position == 0)
            {
                singleWinLbl.transform.localPosition = new Vector3(singleWinLbl.transform.localPosition.x, singleWinLbl.transform.localPosition.y + 25, singleWinLbl.transform.localPosition.z);
                singleDrawLbl.transform.localPosition = new Vector3(singleDrawLbl.transform.localPosition.x, singleDrawLbl.transform.localPosition.y + 25, singleDrawLbl.transform.localPosition.z);
                singleLostLbl.transform.localPosition = new Vector3(singleLostLbl.transform.localPosition.x, singleLostLbl.transform.localPosition.y + 25, singleLostLbl.transform.localPosition.z);
                matchWinLbl.transform.localPosition = new Vector3(matchWinLbl.transform.localPosition.x, matchWinLbl.transform.localPosition.y + 25, matchWinLbl.transform.localPosition.z);
                matchDrawLbl.transform.localPosition = new Vector3(matchDrawLbl.transform.localPosition.x, matchDrawLbl.transform.localPosition.y + 25, matchDrawLbl.transform.localPosition.z);
                matchLostLbl.transform.localPosition = new Vector3(matchLostLbl.transform.localPosition.x, matchLostLbl.transform.localPosition.y + 25, matchLostLbl.transform.localPosition.z);
                tagWinLbl.transform.localPosition = new Vector3(tagWinLbl.transform.localPosition.x, tagWinLbl.transform.localPosition.y + 25, tagWinLbl.transform.localPosition.z);
                tagDrawLbl.transform.localPosition = new Vector3(tagDrawLbl.transform.localPosition.x, tagDrawLbl.transform.localPosition.y + 25, tagDrawLbl.transform.localPosition.z);
                tagLostLbl.transform.localPosition = new Vector3(tagLostLbl.transform.localPosition.x, tagLostLbl.transform.localPosition.y + 25, tagLostLbl.transform.localPosition.z);

                position = 1;
            }

            singleWinLbl.text = singleUnrankedWin;
            singleDrawLbl.text = singleUnrankedDraw;
            singleLostLbl.text = singleUnrankedLost;
            matchWinLbl.text = matchUnrankedWin;
            matchDrawLbl.text = matchUnrankedDraw;
            matchLostLbl.text = matchUnrankedLost;
            tagWinLbl.text = tagUnrankedWin;
            tagDrawLbl.text = tagUnrankedDraw;
            tagLostLbl.text = tagUnrankedLost;
        }
    }
    
    void OnUpdateStatistics()
    {
        UpdateStatistics();
    }

    void OnChangeAvatar()
    {
        if (Program.I().tdoane.AvatarItem)
        {
            Program.I().tdoane.ShowUpdateImageForm(1);
            Program.I().tdoane.profileForm.SetActive(false);
        }
        else
        {
            Program.I().tdoane.CreateMessageBox("ITEM NOT OWNED", "In order to change the avatar you need to purchase the 'Change Avatar Item'. This item can be purchased using diamonds. Diamonds are obtained by donating!", "Store");
            Program.I().tdoane.profileForm.SetActive(false);
        }
    }

    void OnChangeCardBack()
    {
        if (Program.I().tdoane.CardBackItem)
        {
            Program.I().tdoane.ShowUpdateImageForm(0);
            Program.I().tdoane.profileForm.SetActive(false);
        }
        else
        {
            Program.I().tdoane.CreateMessageBox("ITEM NOT OWNED", "In order to change the card back you need to purchase the 'Change Card Back Item'. This item can be purchased using diamonds. Diamonds are obtained by donating!", "Store");
            Program.I().tdoane.profileForm.SetActive(false);
        }
    }

    void OnOk()
    {
        Program.I().tdoane.profileForm.SetActive(false);
        Program.I().shiftToServant(Program.I().menu);
    }
}