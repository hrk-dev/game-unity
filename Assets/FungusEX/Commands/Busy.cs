using UnityEngine;
using Fungus;

[CommandInfo("Sys",
         "Set Busy",
         "设置是否处于可操控状态")]
public class SetBusy : Command
{
    [SerializeField] private bool IsBusy;

    public override void OnEnter()
    {
        GameData.isBusy = IsBusy;
        Continue();
    }

    public override string GetSummary()
    {
        if (IsBusy)
            return "关闭操控";
        else
            return "开启操控";
    }

    public override Color GetButtonColor()
    {
        return new Color32(150, 182, 255, 255);
    }
}
