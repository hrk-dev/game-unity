using Fungus;

[CommandInfo("Sys",
         "Load",
         "��ȡ")]
public class Load : Command
{
    public override void OnEnter()
    {
        GameData.Load();

        Continue();
    }

    public override string GetSummary()
    {
        return "��ȡ";
    }
}
