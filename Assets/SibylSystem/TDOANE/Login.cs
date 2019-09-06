using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour {

    public UIInput usernameTxt;
    public UIInput passwordTxt;
    public UIToggle termsChk;
    public UIToggle rememberChk;
    public UIButton loginBtn;
    public UIButton registerBtn;

    void Start () {
        UIHelper.registEvent(gameObject, "btn_login", onLogin);
        UIHelper.registEvent(gameObject, "btn_regist", onRegister);
    }

    private void onLogin()
    {
        loginBtn.enabled = false;

        if (usernameTxt.value.Length == 0 || passwordTxt.value.Length == 0) {
            Program.I().tdoane.loginForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("LOGIN ERROR", "You have to enter your username and password!", "Login");
            loginBtn.enabled = true;
            return;
        } else if (termsChk.value == false) {
            Program.I().tdoane.loginForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("LOGIN ERROR", "You have to agree to the terms of service!", "Login");
            loginBtn.enabled = true;
            return;
        } else {
            Program.I().tdoane.client.Connect(Program.I().tdoane.IP, Program.I().tdoane.port);
            Program.I().tdoane.client.Send("Login<{]>" + usernameTxt.value + "<{]>" + passwordTxt.value + "<{]>0<{]>0<{]>0<{]>0");
        }
    }

    private void onRegister()
    {
        Program.I().tdoane.loginForm.SetActive(false);
        Program.I().tdoane.ShowRegisterForm();
    }

    public void EnableLoginButton()
    {
        loginBtn.enabled = true;
    }
}
