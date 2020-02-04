using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour {

    public UILabel title;
    public UILabel message;
    public UIButton ok;

    string nextForm;

    void Start ()
    {
        UIHelper.registEvent(ok, onOk);
    }

    void onOk ()
    {
        if (nextForm == "Login")
            Program.I().tdoane.loginForm.SetActive(true);
        if (nextForm == "Register")
            Program.I().tdoane.registerForm.SetActive(true);
        if (nextForm == "Host Custom")
            Program.I().tdoane.hostCustomForm.SetActive(true);
        if (nextForm == "Close")
            Program.I().quit();

        Destroy(gameObject);
    }

    public void SetMessageBoxInformation(string title, string message, string nextForm)
    {
        this.title.text = title;
        this.message.text = message;
        this.nextForm = nextForm;
    }
}
