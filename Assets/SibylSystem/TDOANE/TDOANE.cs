using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDOANE : MonoBehaviour {

    public int version = 1;
    public string IP = "";
    public int port = 2081;

    public GameObject loginForm;
    public GameObject registerForm;
    public NetworkClient client = new NetworkClient();
    public bool isLoggedIn = false;

    public void Tick()
    {
        client.Tick();
    }

    public void CreateMessageBox(string title, string text, string next)
    {
        var message_box = (GameObject)Instantiate(Resources.Load("message_box"));
        message_box.GetComponent<MessageBox>().SetMessageBoxInformation(title, text, next);
    }

    public void ShowRegisterForm()
    {
        if (registerForm == null)
            registerForm = Instantiate(Resources.Load("mod_regist")) as GameObject;

        registerForm.SetActive(true);
    }

    public void ShowLoginForm()
    {
        loginForm.SetActive(true);
    }
}
