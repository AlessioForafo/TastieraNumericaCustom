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
    private SpinBox sBox;

    public override void Start()
    {
        sBox = ((SpinBox)Owner);
        Clear();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void AppendValue(string value) 
    {
        var tempValue = sBox.Value.ToString() + value;
        sBox.Value = float.Parse(tempValue);
    }

    [ExportMethod]
    public void Clear()
    {
        sBox.Value = 0;
    }

    [ExportMethod]
    public void ToggleSign()
    {
        sBox.Value *= -1;
    }

    [ExportMethod]
    public void Backspace()
    {
        var sBoxLength = sBox.Value.ToString().Length;
        if (sBoxLength == 1) return;
        var tempValue = sBox.Value.ToString().Substring(0, sBoxLength - 1);
        sBox.Value = float.Parse(tempValue);
    }
}
