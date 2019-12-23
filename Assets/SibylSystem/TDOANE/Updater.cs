using System;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour {

    public UILabel updateProgress;

    public int myVersion;
    int requiredVersion;

    public int downloadingUpdate;
    int updatesToDownload;

    decimal bytesReceived;
    decimal totalBytesToReceive;

    public decimal filesExtracted;
    public decimal totalFilesToExtract;

    public string updateError = "";

    List<string> updates = new List<string>();

    void Update()
    {
        decimal progressPercentage;

        if (updateError != "")
        {
            updateProgress.text = updateError;
        }
        else if (bytesReceived != totalBytesToReceive)
        {
            progressPercentage = Math.Round((bytesReceived / totalBytesToReceive) * 100);
            updateProgress.text = "Downloading Update: " + downloadingUpdate.ToString() + "/" + updatesToDownload.ToString() + Environment.NewLine + Environment.NewLine + "Download Progress: " + progressPercentage + "%";
        }
        else
        {
            progressPercentage = Math.Round((filesExtracted / totalFilesToExtract) * 100);
            updateProgress.text = "Installing Update: " + downloadingUpdate.ToString() + "/" + updatesToDownload.ToString() + Environment.NewLine + Environment.NewLine + "Install Progress: " + progressPercentage + "%";
        }
    }

    public void InitializeUpdater(int myVersion, int requiredVersion)
    {
        updatesToDownload = requiredVersion - myVersion;

        this.myVersion = myVersion;
        this.requiredVersion = requiredVersion;

        CheckForUpdates();
    }

    private void CheckForUpdates()
    {
        if (myVersion < requiredVersion)
        {
            DownloadUpdate(myVersion + 1);
        }
        else
        {
            Program.I().tdoane.updateBox.SetActive(false);
            Program.I().tdoane.loginForm.GetComponent<Login>().UserLogin();
        }
    }

    private void DownloadUpdate(int update)
    {
        downloadingUpdate++;

        bytesReceived = 0;
        totalBytesToReceive = 0;

        filesExtracted = 0;
        totalFilesToExtract = 0;

        string url = Program.I().tdoane.UpdatesDirectory + update + ".zip";
        
        try
        {
            WebClient webClient = new WebClient();
            webClient.DownloadDataCompleted += Completed;
            webClient.DownloadProgressChanged += ProgressChanged;
            webClient.DownloadDataAsync(new Uri(url));
        }
        catch (Exception e)
        {
            File.WriteAllText("ERROR.txt", e.ToString());
        }
    }

    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        bytesReceived += e.BytesReceived;
        totalBytesToReceive = e.TotalBytesToReceive;
    }

    private void Completed(object sender, DownloadDataCompletedEventArgs e)
    {
        Program.I().ExtractZipFile(e.Result, "", true);
        File.WriteAllText("config/version.conf", myVersion.ToString());
        CheckForUpdates();
    }
}
