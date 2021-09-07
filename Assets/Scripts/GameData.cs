using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum Direction
{
    down,
    right,
    left,
    up
}

static class GameData
{
    static public bool isBusy = false;

    static public string scene;
    static public Vector2 vector;
    static public Direction direction;
    static public Dictionary<string, bool> switchList = new Dictionary<string, bool>();

    static public void Save()
    {
        SaveData save = new SaveData();
        save.scene = scene;
        save.x = vector.x;
        save.y = vector.y;
        save.direction = direction;
        save.switchList = switchList;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    static public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            scene = save.scene;
            vector.x = save.x;
            vector.y = save.y;
            direction = save.direction;
            switchList = save.switchList;

            SceneManager.LoadSceneAsync(scene);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
