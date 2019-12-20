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

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_login", onLogin);
        UIHelper.registEvent(gameObject, "btn_regist", onRegister);
    }

    private void onLogin()
    {
        loginBtn.enabled = false;

        if (!Program.I().tdoane.DownloadClientInfo())
        {
            Program.I().tdoane.loginForm.SetActive(false);
            return;
        }

        UserLogin();
    }

    private void onRegister()
    {
        Program.I().tdoane.loginForm.SetActive(false);
        Program.I().tdoane.ShowRegisterForm();
    }

    public void UserLogin()
    {
        if (usernameTxt.value.Length == 0 || passwordTxt.value.Length == 0)
        {
            Program.I().tdoane.loginForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("LOGIN ERROR", "You have to enter your username and password!", "Login");
            loginBtn.enabled = true;
            return;
        }
        else if (!termsChk.value)
        {
            Program.I().tdoane.loginForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("LOGIN ERROR", "You have to agree to the terms of service!", "Login");
            loginBtn.enabled = true;
            return;
        }
        else
        {
            Program.I().tdoane.client.Connect(Program.I().tdoane.IP, Program.I().tdoane.LobbyPort);

            int sessionStatus = 0;
            if (rememberChk)
                sessionStatus = 1;
            if (PlayerPrefs.GetInt("Session_Status") == 2 && PlayerPrefs.GetString("Saved_Username") == usernameTxt.value && PlayerPrefs.GetString("Saved_Password") == passwordTxt.value)
                sessionStatus = 2;

            Program.I().tdoane.client.Send("Login<{]>" + usernameTxt.value + "<{]>" + Utils.Encrypt(passwordTxt.value) + "<{]>0<{]>" + Utils.GetSecureCode() + "<{]>0<{]>" + sessionStatus.ToString());

            if (rememberChk)
            {
                PlayerPrefs.SetString("Saved_Username", usernameTxt.value);
                PlayerPrefs.SetInt("Remember_Info", 1);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetString("Saved_Username", "");
                PlayerPrefs.SetString("Saved_Password", "");
                PlayerPrefs.SetInt("Session_Status", 0);
                PlayerPrefs.SetInt("Remember_Info", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public void EnableLoginButton()
    {
        loginBtn.enabled = true;
    }
}
