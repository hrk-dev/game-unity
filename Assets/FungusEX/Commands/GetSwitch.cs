using UnityEngine;
using Fungus;

[CommandInfo("Sys",
         "Get Switch",
         "��ȡ���ص�����")]
public class GetSwitch : Command
{
    [SerializeField] private string variableName;
    [SerializeField] private bool defaultSwitch = false;
    [SerializeField] private Flowchart Flowchart;


    public override void OnEnter()
    {
        bool flag;
        if (GameData.switchList.ContainsKey(variableName))
        {
            flag = GameData.switchList[variableName];
        }
        else
        {
            flag = defaultSwitch;
            GameData.switchList.Add(variableName, flag);
        }

        Flowchart.SetBooleanVariable(variableName, flag);

        Continue();
    }

    public override string GetSummary()
    {
        return "��ȡ���� " + variableName;
    }
}
