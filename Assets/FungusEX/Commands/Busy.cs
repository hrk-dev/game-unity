using UnityEngine;
using Fungus;

[CommandInfo("Sys",
         "Set Busy",
         "�����Ƿ��ڿɲٿ�״̬")]
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
            return "�رղٿ�";
        else
            return "�����ٿ�";
    }

    public override Color GetButtonColor()
    {
        return new Color32(150, 182, 255, 255);
    }
}
