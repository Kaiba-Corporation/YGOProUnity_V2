using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class UpdateImage : MonoBehaviour {

    public UILabel titleLbl;
    public UIInput linkTxt;
    public UIButton updateBtn;
    public UIButton backBtn;

    int type;

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_update", OnUpdate);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
    }

    public void SetType(int newType)
    {
        type = newType;
        linkTxt.value = "";

        if (type == 0)
            titleLbl.text = "UPDATE CARD BACK";
        else if (type == 1)
            titleLbl.text = "UPDATE AVATAR";
    }

    private void OnUpdate()
    {
        UploadImage(Program.I().tdoane.Token, type, linkTxt.value);
    }

    private void UploadImage(string token, int type, string url)
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["token"] = Program.I().tdoane.Token;
            values["fileType"] = type == 0 ? "sleeve" : "avatar";
            values["img_url"] = url;

            var response = client.UploadValues(@"http://ygopro.org/kaibapro/uploads/upload_from_link.php", values);

            var responseString = Encoding.Default.GetString(response);
            if (responseString.Contains("SUCCESS"))
            {
                Program.I().tdoane.updateImageForm.SetActive(false);
                Program.I().tdoane.RequestProfile();
            }
            else
            {
                Program.I().tdoane.updateImageForm.SetActive(false);
                Program.I().tdoane.CreateMessageBox("File Upload Error!", responseString, "Profile");
            }
        }
    }

    private void OnBack()
    {
        Program.I().tdoane.profileForm.SetActive(true);
        Program.I().tdoane.updateImageForm.SetActive(false);
    }
}
