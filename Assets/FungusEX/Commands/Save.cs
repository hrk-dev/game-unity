using Fungus;

[CommandInfo("Sys",
         "Save",
         "����")]
public class Save : Command
{
    public override void OnEnter()
    {
        GameData.Save();

        Continue();
    }

    public override string GetSummary()
    {
        return "����";
    }
}
