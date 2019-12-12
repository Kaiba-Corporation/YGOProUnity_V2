using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour {

    public UIInput usernameTxt;
    public UIInput passwordTxt;
    public UIInput confirmPasswordTxt;
    public UIButton registerBtn;
    public UIButton backBtn;

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_register", OnRegister);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
    }

    public void OnRegister()
    {
        if (!Program.I().tdoane.DownloadClientInfo())
        {
            Program.I().tdoane.registerForm.SetActive(false);
            return;
        }

        registerBtn.enabled = false;

        if (usernameTxt.value.Length == 0)
        {
            Program.I().tdoane.registerForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("REGISTRATION ERROR", "You have to enter your username and password!", "Register");
            registerBtn.enabled = true;
            return;
        }

        if (passwordTxt.value.Length < 8)
        {
            Program.I().tdoane.registerForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("REGISTRATION ERROR", "Password has to be at least 8 characters long!", "Register");
            registerBtn.enabled = true;
            return;
        }

        if (usernameTxt.value.Contains("<") || usernameTxt.value.Contains(">") || usernameTxt.value.Contains(",") || usernameTxt.value.Contains(":") || usernameTxt.value.Contains(";"))
        {
            Program.I().tdoane.registerForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("REGISTRATION ERROR", "Username can't contain the following symbols: < > , : ;", "Register");
            registerBtn.enabled = true;
            return;
        }

        if (passwordTxt.value != confirmPasswordTxt.value)
        {
            Program.I().tdoane.registerForm.SetActive(false);
            Program.I().tdoane.CreateMessageBox("REGISTRATION ERROR", "Passwords don't match! Please double check and try again!", "Register");
            registerBtn.enabled = true;
            return;
        }

        Program.I().tdoane.client.Connect(Program.I().tdoane.IP, Program.I().tdoane.LobbyPort);
        Program.I().tdoane.client.Send("Register<{]>" + usernameTxt.value + "<{]>" + Utils.Encrypt(passwordTxt.value) + "<{]><{]><{]>" + Utils.GetSecureCode());
    }

    public void OnBack()
    {
        Program.I().tdoane.registerForm.SetActive(false);
        Program.I().tdoane.ShowLoginForm();
    }

    public void EnableRegisterButton()
    {
        registerBtn.enabled = true;
    }
}
