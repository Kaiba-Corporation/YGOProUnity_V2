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

    public string Username;

    public List<string> BotDecks = new List<string>() { "Altergeist", "Blackwing", "Blue-Eyes", "BlueEyesMaxDragon", "Burn", "ChainBurn", "CyberDragon", "DarkMagician", "Dragunity",
        "Evilswarm", "Frog", "Gravekeeper", "Graydle", "GrenMajuThunderBoarder", "Horus", "Level8", "Lightsworn", "LightswornShaddoldinosour", "MokeyMokey", "MokeyMokeyKing", "Nekroz",
        "Orcust", "Phantasm", "Qliphort", "Rainbow", "Rank5", "ST1732", "Salamangreat", "SkyStriker", "ToadallyAwesome", "Trickstar", "Yosenju", "ZexalWeapons", "Zoodiac" };

    public System.Random rand = new System.Random();

    public GameObject loginForm;
    public GameObject registerForm;
    public GameObject gameList;
    public GameObject updateBox;

    public NetworkClient client = new NetworkClient();

    public bool isLoggedIn = false;

    public void Tick()
    {
        client.Tick();
    }

    public bool DownloadClientInfo()
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

            if (File.Exists("config/version.conf"))
                MinorVersion = Convert.ToInt32(File.ReadAllText("config/version.conf"));
            else
            {
                CreateMessageBox("Game Files Corrupted!", "Your game files are corrupted, please redownload the game from YGOPRO.ORG", "Close");
                return false;
            }

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
        loginForm.SetActive(true);
    }

    public void ShowGameList()
    {
        Program.I().tdoane.client.Send("GetRooms<{]>0");
        if (gameList == null)
            gameList = Instantiate(Resources.Load("mod_room_list")) as GameObject;

        gameList.SetActive(true);
    }

    public void DownloadUpdates(int myVersion, int requiredVersion)
    {
        updateBox = (GameObject)Instantiate(Resources.Load("update_box"));
        updateBox.GetComponent<Updater>().InitializeUpdater(myVersion, requiredVersion);
    }
}
