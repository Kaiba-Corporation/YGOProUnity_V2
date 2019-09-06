using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDOANE : MonoBehaviour {

    public GameObject mod_login;
    public GameObject mod_regist;
    //public TCPClient client = new TCPClient();
    public bool isLoggedIn = false;


    // Use this for initialization
    void Start () {
		
	}

    public void CreateMessageBox(string title, string text, string next)
    {
        //var message_box = (GameObject)Instantiate(Resources.Load("message_box"));
        //message_box.GetComponent<message_box>().SetMessageBoxInformation(title, text, next);
    }

    public void ShowRegisterForm()
    {
        mod_regist = Instantiate(Resources.Load("mod_regist")) as GameObject;
        mod_regist.SetActive(true);
    }
}
