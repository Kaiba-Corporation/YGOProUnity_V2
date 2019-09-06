using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class NetworkClient : MonoBehaviour {

    public List<String> messages = new List<String>();

    NetworkStream stream;
    StreamWriter streamw;
    StreamReader streamr;
    TcpClient client;
    Thread t;
    public delegate void DAddItem(string s);

    public void Connect(string ip, int port)
    {
        try
        {
            client = new TcpClient();
            client.Connect(ip, port);
            if (client.Connected)
            {
                stream = client.GetStream();
                streamw = new StreamWriter(stream);
                streamr = new StreamReader(stream);
                t = new Thread(Listen);
                t.Start();
            }
        }
        catch { }
    }

    public void Disconnect()
    {
        try
        {
            client.GetStream().Close();
            client.Close();
        } catch { }
    }

    private void Listen()
    {
        while (client.Connected)
            Receive(streamr.ReadLine());
    }

    private void Receive(string s)
    {
        try
        {
            lock (messages)
                messages.Add(s);
        } catch { }
    }

    public void Send(string message)
    {
        streamw.Write(message + "\r\n");
        streamw.Flush();
    }

    public void Tick()
    {
        while (messages.Count > 0)
        {
            lock (messages)
            {
                Parse(messages[0]);
                messages.RemoveAt(0);
            }
        }
    }

    private void Parse(string message)
    {
        if (message != null && message.Length > 0)
        {
            string[] messageArray = Regex.Split(message, "<{]>");

            if (messageArray[0] == "LoginVerified") OnLogin(messageArray);
            else if (messageArray[0] == "WrongPassword") OnWrongPassword(messageArray);
            else if (messageArray[0] == "RegisterComplete") OnRegisterComplete(messageArray);
            else if (messageArray[0] == "RegisterFail") OnRegisterFail(messageArray);
        }
    }

    private void OnLogin(string[] message)
    {
        Program.I().tdoane.loginForm.SetActive(false);
        //Program.I().initializeMenu();
    }

    private void OnWrongPassword(string[] message)
    {
        Disconnect();
        Program.I().tdoane.loginForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("WRONG PASSWORD", "You have entered an incorrect password, please double check your password and try again!", "Login");
        Program.I().tdoane.loginForm.GetComponent<Login>().EnableLoginButton();
    }

    private void OnRegisterComplete(string[] message)
    {
        Disconnect();
        Program.I().tdoane.registerForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("ACCOUNT CREATED", "You have successfully created your account, you may now log in!", "mod_login");
        //Program.I().EnableRegisterButton();
    }

    private void OnRegisterFail(string[] message)
    {
        Disconnect();
        Program.I().tdoane.registerForm.SetActive(false);
        Program.I().tdoane.CreateMessageBox("REGISTRATION ERROR", "An account with that username already exists, please use a different username!", "mod_regist");
        //Program.I().EnableRegisterButton();
    }
}
