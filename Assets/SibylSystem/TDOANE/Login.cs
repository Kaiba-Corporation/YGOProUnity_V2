using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour {

    public UIInput usernameTxt;
    public UIInput passwordTxt;
    public UIToggle rememberChk;
    public UIToggle autoChk;
    public UIButton loginBtn;
    public UIButton registerBtn;

    // Use this for initialization
    void Start () {
        UIHelper.registEvent(gameObject, "btn_login", onLogin);
        UIHelper.registEvent(gameObject, "btn_regist", onRegister);
    }

    private void onLogin()
    {

    }

    private void onRegister()
    {
        Program.I().tdoane.mod_login.SetActive(false);
        Program.I().tdoane.ShowRegisterForm();
    }
}
