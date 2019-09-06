using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDOANE : MonoBehaviour {

    public GameObject loginForm;
    public GameObject registerForm;
    //public TCPClient client = new TCPClient();
    public bool isLoggedIn = false;


    // Use this for initialization
    void Start () {
		
	}

    public void CreateMessageBox(string title, string text, string next)
    {
        var message_box = (GameObject)Instantiate(Resources.Load("message_box"));
        message_box.GetComponent<MessageBox>().SetMessageBoxInformation(title, text, next);
    }

    public void ShowRegisterForm()
    {
        registerForm = Instantiate(Resources.Load("mod_regist")) as GameObject;
        registerForm.SetActive(true);
    }
}
