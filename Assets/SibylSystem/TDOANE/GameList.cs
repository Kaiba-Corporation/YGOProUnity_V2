using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameList : MonoBehaviour {

    public UIButton refreshBtn;
    public UIButton singleBtn;
    public UIButton matchBtn;
    public UIButton tagBtn;
    public UIButton duelAIBtn;
    public UIButton backBtn;
    public UIButton searchBtn;
    public UIInput searchTxt;

    public UIButton leftBtn;
    public UIButton rightBtn;
    public UILabel pageLbl;

    public UIButton room0Btn;
    public UIButton room1Btn;
    public UIButton room2Btn;
    public UIButton room3Btn;
    public UIButton room4Btn;

    public UILabel room0Lbl;
    public UILabel room1Lbl;
    public UILabel room2Lbl;
    public UILabel room3Lbl;
    public UILabel room4Lbl;

    public int page = 1;
    public int pages = 1;
    List<string> roomList = new List<string>();
    List<string> tempRoomList = new List<string>();

    void Start ()
    {
        UIHelper.registEvent(gameObject, "btn_refresh", OnRefresh);
        UIHelper.registEvent(gameObject, "btn_host_single", OnSingle);
        UIHelper.registEvent(gameObject, "btn_host_match", OnMatch);
        UIHelper.registEvent(gameObject, "btn_host_tag", OnTag);
        UIHelper.registEvent(gameObject, "btn_duel_ai", OnDuelAI);
        UIHelper.registEvent(gameObject, "btn_left", OnLeft);
        UIHelper.registEvent(gameObject, "btn_right", OnRight);
        UIHelper.registEvent(gameObject, "btn_back", OnBack);
        UIHelper.registEvent(gameObject, "btn_search", OnSearch);
        
        UIHelper.registEvent(gameObject, "btn_room_0", OnRoom0);
        UIHelper.registEvent(gameObject, "btn_room_1", OnRoom1);
        UIHelper.registEvent(gameObject, "btn_room_2", OnRoom2);
        UIHelper.registEvent(gameObject, "btn_room_3", OnRoom3);
        UIHelper.registEvent(gameObject, "btn_room_4", OnRoom4);
    }

    private void OnRefresh()
    {
        page = 1;
        pages = 1;

        Program.I().tdoane.client.Send("GetRooms<{]>0");
    }

    private void OnSingle()
    {
        string gameName = "0004051,8000," + Utils.GetRandomString(6);
        Program.I().tdoane.gameList.SetActive(false);
        Program.I().selectServer.joinGame(Program.I().tdoane.Username, Program.I().tdoane.IP, Program.I().tdoane.GamePort.ToString(), gameName);
    }

    private void OnMatch()
    {
        string gameName = "0014051,8000," + Utils.GetRandomString(6);
        Program.I().tdoane.gameList.SetActive(false);
        Program.I().selectServer.joinGame(Program.I().tdoane.Username, Program.I().tdoane.IP, Program.I().tdoane.GamePort.ToString(), gameName);
    }

    private void OnTag()
    {
        string gameName = "0024051,8000," + Utils.GetRandomString(6);
        Program.I().tdoane.gameList.SetActive(false);
        Program.I().selectServer.joinGame(Program.I().tdoane.Username, Program.I().tdoane.IP, Program.I().tdoane.GamePort.ToString(), gameName);
    }

    private void OnDuelAI()
    {
        Program.I().tdoane.client.Send("DuelingRobot<{]>Altergeist");
    }

    private void OnLeft()
    {
        if (page != 1)
            page--;

        UpdateRooms();
    }

    private void OnRight()
    {
        if (page < pages)
            page++;

        UpdateRooms();
    }

    public void UpdateRoomList(string[] rooms)
    {
        roomList.Clear();
        tempRoomList.Clear();

        foreach (string room in rooms)
            if (room.Substring(0, 1) == "0")
                roomList.Add(room);
        
        tempRoomList.AddRange(roomList);

        UpdatePageStatus();
        UpdateRooms();
    }

    public void UpdateRooms()
    {
        pageLbl.text = page.ToString() + "/" + pages.ToString();

        int counter = 0;

        while ((counter < 5) && (counter + (5 * (page - 1)) < tempRoomList.Count))
        {
            if (counter == 0)
            {
                room0Lbl.text = GetRoomInfo(tempRoomList[counter + (5 * (page - 1))]);
                room0Btn.defaultColor = GetButtonColor(tempRoomList[counter + (5 * (page - 1))]);
                room0Btn.enabled = true;
            }
            else if (counter == 1)
            {
                room1Lbl.text = GetRoomInfo(tempRoomList[counter + (5 * (page - 1))]);
                room1Btn.defaultColor = GetButtonColor(tempRoomList[counter + (5 * (page - 1))]);
                room1Btn.enabled = true;
            }
            else if (counter == 2)
            {
                room2Lbl.text = GetRoomInfo(tempRoomList[counter + (5 * (page - 1))]);
                room2Btn.defaultColor = GetButtonColor(tempRoomList[counter + (5 * (page - 1))]);
                room2Btn.enabled = true;
            }
            else if (counter == 3)
            {
                room3Lbl.text = GetRoomInfo(tempRoomList[counter + (5 * (page - 1))]);
                room3Btn.defaultColor = GetButtonColor(tempRoomList[counter + (5 * (page - 1))]);
                room3Btn.enabled = true;
            }
            else if (counter == 4)
            {
                room4Lbl.text = GetRoomInfo(tempRoomList[counter + (5 * (page - 1))]);
                room4Btn.defaultColor = GetButtonColor(tempRoomList[counter + (5 * (page - 1))]);
                room4Btn.enabled = true;
            }

            counter++;
        }

        while (counter < 5)
        {
            if (counter == 0)
            {
                room0Lbl.text = "";
                room0Btn.ResetDefaultColor();
                room0Btn.enabled = false;
            }
            else if (counter == 1)
            {
                room1Lbl.text = "";
                room1Btn.ResetDefaultColor();
                room1Btn.enabled = false;
            }
            else if (counter == 2)
            {
                room2Lbl.text = "";
                room2Btn.ResetDefaultColor();
                room2Btn.enabled = false;
            }
            else if (counter == 3)
            {
                room3Lbl.text = "";
                room3Btn.ResetDefaultColor();
                room3Btn.enabled = false;
            }
            else if (counter == 4)
            {
                room4Lbl.text = "";
                room4Btn.ResetDefaultColor();
                room4Btn.enabled = false;
            }

            counter++;
        }
    }
    
    void UpdatePageStatus()
    {
        pages = (int)Math.Ceiling(tempRoomList.Count / 5.0);

        if (tempRoomList.Count > 5)
        {
            leftBtn.enabled = true;
            rightBtn.enabled = true;
        }
        else
        {
            leftBtn.enabled = false;
            rightBtn.enabled = false;
        }
    }

    string GetRoomInfo(string room)
    {
        string[] roomParts = Regex.Split(room, ":");
        string gameInfo = "";

        if (room.Substring(2, 1) == "0")
            gameInfo += "SINGLE DUEL";
        else if (room.Substring(2, 1) == "1")
            gameInfo += "MATCH DUEL";
        else
            gameInfo += "TAG DUEL";

        if (room.Substring(1, 1) == "0")
            gameInfo += " - TCG BANLIST";
        else if (room.Substring(1, 1) == "1")
            gameInfo += " - OCG BANLIST";
        else if (room.Substring(1, 1) == "2")
            gameInfo += " - TRADITIONAL BANLIST";
        else if (room.Substring(1, 1) == "3")
            gameInfo += " - NO BANLIST";
        else if (room.Substring(1, 1) == "4")
            gameInfo += " - TCG BANLIST - ONLY TCG CARDS";
        else if (room.Substring(1, 1) == "5")
            gameInfo += " - OCG BANLIST - ONLY TCG CARDS";
        else if (room.Substring(1, 1) == "6")
            gameInfo += " - TRADITIONAL BANLIST - ONLY TCG CARDS";
        else if (room.Substring(1, 1) == "7")
            gameInfo += " - NO BANLIST - ONLY TCG CARDS";
        else if (room.Substring(1, 1) == "8")
            gameInfo += " - TCG BANLIST - ONLY OCG CARDS";
        else if (room.Substring(1, 1) == "9")
            gameInfo += " - OCG BANLIST - ONLY OCG CARDS";
        else if (room.Substring(1, 1) == "A")
            gameInfo += " - TRADITIONAL BANLIST - ONLY OCG CARDS";
        else
            gameInfo += " - NO BANLIST - ONLY OCG CARDS";

        if (room.Substring(3, 1) == "1")
            gameInfo += " - MR: 1";
        else if (room.Substring(3, 1) == "2")
            gameInfo += " - MR: 2";
        else if (room.Substring(3, 1) == "3")
            gameInfo += " - MR: 3";
        else
            gameInfo += " - MR: 4";

        gameInfo += Environment.NewLine + "HOST: " + roomParts[2];

        if (roomParts.Length > 3)
            gameInfo += " - PLAYERS: ";

        int counter = 0;
        while (counter < roomParts.Length)
        {
            if (counter == 3)
                gameInfo += roomParts[counter];
            if (counter >= 4)
                gameInfo += ", " + roomParts[counter];

            counter++;
        }
        
        string rules = "";
        if (room.Length > 7)
        {
            if (room[7] == ',')
            {
                if (room[4] == '1')
                    rules = "RULES: DON'T CHECK DECK";
                else if (room[4] == '2')
                    rules = "RULES: DON'T SHUFFLE DECK";
                else if (room[4] == '3')
                    rules = "RULES: DON'T CHECK DECK, DON'T SHUFFLE DECK";
                else if (room[4] == '4')
                    rules = "RULES: DON'T RECOVER TIME";
                else if (room[4] == '5')
                    rules = "RULES: DON'T CHECK DECK, DON'T RECOVER TIME";
                else if (room[4] == '6')
                    rules = "RULES: DON'T SHUFFLE DECK, DON'T RECOVER TIME";
                else if (room[4] == '7')
                    rules = "RULES: DON'T CHECK DECK, DON'T SHUFFLE DECK, DON'T RECOVER TIME";
            }
        }
        
        if (rules != "")
            gameInfo += Environment.NewLine + rules;

        return gameInfo;
    }

    Color GetButtonColor(string room)
    {
        Color buttonColor = Color.green;

        string[] roomParts = Regex.Split(room, ":");
        string name = roomParts[0];

        if (room.Substring(1, 1) == "3" || room.Substring(1, 1) == "7" || room.Substring(1, 1) == "B")
            return new Color(0.8f, 0f, 0f);
        else if (name.Length > 7)
        {
            if (name[7] == ',')
            {
                if (name[4] == '1' || name[4] == '2' || name[4] == '3' || name[4] == '4' || name[4] == '5' || name[4] == '6' || name[4] == '7')
                    return new Color(0.8f, 0f, 0f);
            }
        }

        if (room.Substring(2, 1) == "0")
            return new Color(0f, 0.71f, 0.027f);
        else if (room.Substring(2, 1) == "1")
            return new Color(0f, 0.027f, 0.71f);
        else
            return new Color(0.749f, 0.486f, 0f);
    }

    void OnRoom0() { JoinGame(0); }
    void OnRoom1() { JoinGame(1); }
    void OnRoom2() { JoinGame(2); }
    void OnRoom3() { JoinGame(3); }
    void OnRoom4() { JoinGame(4); }

    void JoinGame(int roomId)
    {
        string roomInfo = tempRoomList[roomId + (5 * (page - 1))];
        string[] roomParts = Regex.Split(roomInfo, ":");

        Program.I().tdoane.gameList.SetActive(false);
        Program.I().selectServer.joinGame(Program.I().tdoane.Username, Program.I().tdoane.IP, Program.I().tdoane.GamePort.ToString(), roomParts[0]);
    }

    void OnBack()
    {
        Program.I().tdoane.gameList.SetActive(false);
        Program.I().shiftToServant(Program.I().menu);
    }

    void OnSearch()
    {
        page = 1;
        tempRoomList.Clear();

        if (searchTxt.value == "")
            tempRoomList.AddRange(roomList);
        else
        {
            foreach (string room in roomList)
            {
                string[] roomParts = Regex.Split(room, ":");

                int counter = 2;
                while (counter < roomParts.Length)
                {
                    if (roomParts[counter].ToLower().Contains(searchTxt.value.ToLower()))
                        tempRoomList.Add(room);

                    counter++;
                }
            }
        }
        
        UpdatePageStatus();
        UpdateRooms();
    }
}
