﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private UserIP userIP = new UserIP();
    private TcpToErlang t = null;
    private int copyNumber = 1;
    private static ArrayList uploadedFiles = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        FileUpload.Attributes["onchange"] = "UploadFile(this)";

        if (!this.IsPostBack)
        {
            //userIP.displayIP(DataList1);
            //Response.AppendHeader("Refresh", "3");
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        userIP.displayIP(DataList1);
    }

    protected void send(object sender, EventArgs e)
    {
        t = new TcpToErlang();
        t.sendMessage("start");
    }

    protected String getFolderNameFromIp(String ip)
    {
        int hashedIP = ip.GetHashCode();
        string folderName = hashedIP.ToString();
        return folderName;

    }

    protected void uploadFile(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(FileUpload.FileName);
                string nameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
                string extension = Path.GetExtension(filename);

                if (extension.ToLower() == ".json")
                {
                    //Users ip 
                    string ip = "192.1.45.188";
                    //Create a folder name from the ip of the user
                    string folderName = getFolderNameFromIp(ip);
                    string folderPath = Server.MapPath("Uploads");

                    //Checking if a directory for that user exists if not create one
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(new Uri(folderPath + @"\" + folderName).LocalPath);
                    }

                    string curFile = Server.MapPath("~/Uploads/" + folderName + "/") + filename;
                    bool state = File.Exists(curFile) ? true : false;

                    if (state)
                    {
                        string fileNum = Convert.ToString(copyNumber);
                        string filecopy = string.Concat(nameWithoutExtension, "-Copy") + fileNum;
                        FileUpload.SaveAs(Server.MapPath("~/Uploads/" + folderName + "/") + filecopy + extension);
                        addUploadedFiles(filecopy);
                        copyNumber++;
                    }
                    else
                    {
                        FileUpload.SaveAs(Server.MapPath("~/Uploads/" + folderName + "/") + filename);
                        addUploadedFiles(nameWithoutExtension);
                    }
                    addToPanel(this, e);

                }
                else
                {
                    //display message
                }

            }

            catch (Exception ex)
            {
                string errorMsg = "Error";
                errorMsg += ex.Message;
                throw new Exception(errorMsg);
            }
        }
    }
    protected void addUploadedFiles(string fileName)
    {
        uploadedFiles.Add(fileName);
    }


    protected void addToPanel(object sender, EventArgs e)
    {
        foreach (string file in uploadedFiles)
        {
            Button btn = new Button();
            btn.OnClientClick = "return false;";
            btn.Width = 160;
            btn.Text = file;
            pnlUploads.Controls.Add(btn);
        }
    }

    protected void removeAllControls(object sender, EventArgs e)
    {
        foreach (Control item in pnlUploads.Controls.OfType<Button>())
        {
            pnlUploads.Controls.Remove(item);
        }
    }

    protected void restartProcess(object sender, EventArgs e)
    {
        Process[] processlist = Process.GetProcesses();


        foreach (Process p in processlist)
        {
            if (p.ProcessName == "node")
            {
                p.Kill();
                p.WaitForExit();
            }
            //Response.Write(process.ProcessName + "<br>");
        }

        Process process = new Process();
        process.StartInfo.WorkingDirectory = "c:\\Users\\Murat Kan\\Desktop\\serverside";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c node listen.js";
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();
        //string output = si.StandardOutput.ReadToEnd();
        process.Close();
        //Response.Write(output);
    }
}