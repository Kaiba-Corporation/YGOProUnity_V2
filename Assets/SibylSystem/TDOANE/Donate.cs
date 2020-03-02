using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donate : MonoBehaviour {

    public UIButton backBtn;
    public UIButton donateBtn;

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
        UIHelper.registEvent(gameObject, "btn_donate", OnDonate);
    }

    void OnBack()
    {
        Program.I().tdoane.donateForm.SetActive(false);
        Program.I().tdoane.ShowStoreForm();
    }

    void OnDonate()
    {
        Application.OpenURL("http://ygopro.org/donate.php?userid=" + Program.I().tdoane.UserID);
        Program.I().tdoane.donateForm.SetActive(false);
        Program.I().shiftToServant(Program.I().menu);
    }
}