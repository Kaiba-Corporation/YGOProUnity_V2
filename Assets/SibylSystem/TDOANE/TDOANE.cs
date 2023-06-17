using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TDOANE : MonoBehaviour {

    public int MajorVersion = 1;
    public int MinorVersion;
    public string GameVersion = "0x133D";
    public string IP;
    public int LobbyPort;
    public int GamePort;
    public string UpdatesDirectory = "http://ygopro.org/updates_2/";

    public string UserID;
    public string Username;
    public string Token;
    public string Rank;
    public string Team;
    public string Wp;
    public bool AvatarItem = false;
    public bool CardBackItem = false;

    public List<string> BotDecks = new List<string>() { "Altergeist", "Blackwing", "Blue-Eyes", "BlueEyesMaxDragon", "Burn", "ChainBurn", "CyberDragon", "DarkMagician", "Dragunity",
        "Evilswarm", "Frog", "Gravekeeper", "Graydle", "GrenMajuThunderBoarder", "Horus", "Level8", "Lightsworn", "LightswornShaddoldinosour", "MokeyMokey", "MokeyMokeyKing", "Nekroz",
        "Orcust", "Phantasm", "Qliphort", "Rainbow", "Rank5", "ST1732", "Salamangreat", "SkyStriker", "ToadallyAwesome", "Trickstar", "Yosenju", "ZexalWeapons", "Zoodiac" };

    public System.Random rand = new System.Random();

    public bool ImagesExtracted = true;

    public bool isBotDuel = false;

    public GameObject loginForm;
    public GameObject registerForm;
    public GameObject gameListForm;
    public GameObject updateBoxForm;
    public GameObject hostCustomForm;
    public GameObject duelAiForm;
    public GameObject profileForm;
    public GameObject updateImageForm;
    public GameObject storeForm;
    public GameObject donateForm;

    public NetworkClient client = new NetworkClient();

    public bool isLoggedIn = false;

    public void Tick()
    {
        client.Tick();
    }

    public bool DownloadClientInfo(bool register = false)
    {
        try
        {
            WebClient downloadClient = new WebClient();
            string clientInfo = downloadClient.DownloadString("http://ygopro.org/ygopro2_data.php");
            string[] clientData = Regex.Split(clientInfo, ",");
            int majorVersion = Convert.ToInt32(clientData[0]);
            int minorVersion = Convert.ToInt32(clientData[1]);
            IP = clientData[2];
            LobbyPort = Convert.ToInt32(clientData[3]);
            GamePort = Convert.ToInt32(clientData[4]);
            UpdatesDirectory = clientData[5];

            if (MajorVersion != majorVersion)
            {
                CreateMessageBox("Client Update Required!", "Your game client is out of date, please download the updated game client from: YGOPRO.ORG", "Close");
                return false;
            }

            if (register)
                return true;

            if (File.Exists("config/version.conf"))
                MinorVersion = Convert.ToInt32(File.ReadAllText("config/version.conf"));
            else
            {
                CreateMessageBox("Game Files Corrupted!", "Your game files are corrupted, please redownload the game from YGOPRO.ORG", "Close");
                return false;
            }

            if (MinorVersion == -1)
            {
                ImagesExtracted = false;
                ExtractImages();
            }

            if (!ImagesExtracted)
                return false;

            if (MinorVersion != minorVersion)
            {
                DownloadUpdates(MinorVersion, minorVersion);
                loginForm.SetActive(false);
                return false;
            }

            return true;
        }
        catch
        {
            CreateMessageBox("Unable To Get Server Info!", "The game is unable to get server info, check your internet connection and try again, if the problem persists please redownload the game from YGOPRO.ORG", "Close");
            return false;
        }
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
        if (loginForm == null)
            loginForm = Instantiate(Resources.Load("mod_login")) as GameObject;

        loginForm.SetActive(true);
    }

    public void ShowGameListForm()
    {
        client.Send("GetRooms<{]>0");
        if (gameListForm == null)
            gameListForm = Instantiate(Resources.Load("mod_room_list")) as GameObject;

        gameListForm.SetActive(true);
    }

    public void ShowDuelAiForm()
    {
        if (duelAiForm == null)
            duelAiForm = Instantiate(Resources.Load("mod_duel_ai")) as GameObject;

        duelAiForm.SetActive(true);
    }

    public void RequestProfile()
    {
        client.Send("RequestMyProfile<{]>" + Username);
    }

    public void ShowProfileForm(string[] message)
    {
        if (profileForm == null)
            profileForm = Instantiate(Resources.Load("mod_profile")) as GameObject;

        profileForm.SetActive(true);
        profileForm.GetComponent<Profile>().Load(message);
    }

    public void ShowUpdateImageForm(int type)
    {
        if (updateImageForm == null)
            updateImageForm = Instantiate(Resources.Load("update_image_box")) as GameObject;

        updateImageForm.SetActive(true);
        updateImageForm.GetComponent<UpdateImage>().SetType(type);
    }

    public void ShowStoreForm()
    {
        if (storeForm == null)
            storeForm = Instantiate(Resources.Load("mod_store")) as GameObject;

        storeForm.GetComponent<Store>().LoadStore();
        storeForm.SetActive(true);
        client.Send("RequestDiamonds<{]>" + Username);
    }

    public void ShowDonate()
    {
        if (donateForm == null)
            donateForm = Instantiate(Resources.Load("mod_donate")) as GameObject;

        donateForm.SetActive(true);
    }

    public void DownloadUpdates(int myVersion, int requiredVersion)
    {
        updateBoxForm = (GameObject)Instantiate(Resources.Load("update_box"));
        updateBoxForm.GetComponent<Updater>().InitializeUpdater(myVersion, requiredVersion);
    }

    public void ExtractImages()
    {
        loginForm.SetActive(false);

        updateBoxForm = (GameObject)Instantiate(Resources.Load("update_box"));
        updateBoxForm.GetComponent<Updater>().ExtractImages();
    }
}