using UnityEngine;
using Fungus;

[CommandInfo("Sys",
         "Set Switch",
         "���ñ���������")]
public class SetSwitch : Command
{
    [SerializeField] private string variableName;
    [SerializeField] private bool flag = false;
    [SerializeField] private Flowchart Flowchart;

    public override void OnEnter()
    {
        if (GameData.switchList.ContainsKey(variableName))
        {
            GameData.switchList[variableName] = flag;
        }
        else
        {
            GameData.switchList.Add(variableName, flag);
        }

        Flowchart.SetBooleanVariable(variableName, flag);

        Continue();
    }

    public override string GetSummary()
    {
        return "���ÿ��� " + variableName + ": " + flag;
    }
}
