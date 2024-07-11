#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
#endregion

public class AppendKeyboard : BaseNetLogic
{
    private TextBox tBox;

    public override void Start()
    {
        tBox = ((TextBox)Owner);
        tBox.Text = "";
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void AppendValue(string value) 
    {
        tBox.Text += value;
    }
}
