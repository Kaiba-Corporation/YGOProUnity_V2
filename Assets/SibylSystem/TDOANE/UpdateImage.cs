using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateImage : MonoBehaviour {

    public UILabel title;
    public UIInput linkTxt;
    public UIButton updateBtn;
    public UIButton backBtn;

    int type;

    void Start () {
        UIHelper.registEvent(gameObject, "btn_update", OnUpdate);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
    }

    public void SetType(int newType)
    {
        type = newType;
        linkTxt.value = "";

        if (type == 0)
            title.text = "UPDATE CARD BACK";
        else if (type == 1)
            title.text = "UPDATE AVATAR";
    }

    private void OnUpdate()
    {
        Program.I().tdoane.profileForm.SetActive(true);
        Program.I().tdoane.updateImageForm.SetActive(false);

        if (type == 0)
        {
            Program.I().tdoane.client.Send("UpdateCardBack<{]>" + linkTxt.value);
            Program.I().tdoane.profileForm.GetComponent<Profile>().SetCardBack(linkTxt.value);
        }
        else if (type == 1)
        {
            Program.I().tdoane.client.Send("UpdateAvatar<{]>" + linkTxt.value);
            Program.I().tdoane.profileForm.GetComponent<Profile>().LoadAvatar(linkTxt.value);
        }
    }

    private void OnBack()
    {
        Program.I().tdoane.profileForm.SetActive(true);
        Program.I().tdoane.updateImageForm.SetActive(false);
    }
}
