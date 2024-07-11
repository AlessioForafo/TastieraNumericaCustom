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
    private IUAVariable typingDecimals;
    private int numberOfDecimals;
    private SpinBox sBox;

    public override void Start()
    {
        typingDecimals = LogicObject.GetVariable("typingDecimals");
        numberOfDecimals = 0;
        sBox = ((SpinBox)Owner);
        Clear();
    }

    [ExportMethod]
    public void AppendValue(string value) 
    {
        var sboxValue = sBox.Value.ToString();
        var newValue = int.Parse(value);
        var sboxValueHasDecimals = sboxValue.Contains(',');
        var integerPart = sboxValue.Split(',')[0];
        var decimalPart = sboxValueHasDecimals ? sboxValue.Split(',')[1].Substring(0, numberOfDecimals) : string.Empty;
        var res = float.Parse(value);

        if ((bool) typingDecimals.Value.Value && !sboxValueHasDecimals)
        {
            res = int.Parse(sBox.Value + value) * 0.1f;
            numberOfDecimals++;
        } else if (sboxValueHasDecimals)
        {
            numberOfDecimals++;
            decimalPart += value;
            res = (float) (int.Parse(integerPart + decimalPart) * Math.Pow(0.1, numberOfDecimals));
        } 

        sBox.Value = res;
        typingDecimals.Value = false;
    }

    [ExportMethod]
    public void Clear()
    {
        sBox.Value = 0;
        numberOfDecimals = 0;
    }

    [ExportMethod]
    public void ToggleSign()
    {
        sBox.Value *= -1;
    }



    [ExportMethod]
    public void Backspace()
    {
        var sboxValueHasDecimals = sBox.Value.ToString().Contains(',');
        if (sboxValueHasDecimals )
        {
            numberOfDecimals--;
        }

        var sBoxLength = sBox.Value.ToString().Length;
        if (sBoxLength == 1) return;
        var tempValue = sBox.Value.ToString().Substring(0, sBoxLength - 1);
        sBox.Value = float.Parse(tempValue);
    }
}
