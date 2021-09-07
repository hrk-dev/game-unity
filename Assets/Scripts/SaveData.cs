using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string scene;
    public float x;
    public float y;
    public Direction direction;
    public Dictionary<string, bool> switchList;
}
