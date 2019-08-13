using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour {

    public UILabel title;
    public UILabel message;
    public UIButton ok;

    void Start () {
        UIHelper.registEvent(ok, onOk);
    }

    void onOk ()
    {
        Destroy(gameObject);
    }
}
