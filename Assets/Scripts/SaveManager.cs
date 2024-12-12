using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Transform carT;
    [SerializeField]
    private Transform playerT;

    private void Awake()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            string json = default;
            using (StreamReader sr = new StreamReader(Path.Combine(Application.persistentDataPath, "save.json")))
            {
                json = sr.ReadToEnd();
            }
            var saveFile = JsonUtility.FromJson<Database.SaveFile>(json);
            carT.position = saveFile.CarTransform.Position;
            carT.rotation = saveFile.CarTransform.Rotation;
            playerT.position = saveFile.PlayerTransform.Position;
            playerT.rotation = saveFile.PlayerTransform.Rotation;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
    }

    private void SaveGame()
    {
        Database.SaveFile saveFile = new Database.SaveFile();
        Database.CarTransform car = new Database.CarTransform();
        Database.PlayerTransform player = new Database.PlayerTransform();
        car.Position = carT.position;
        car.Rotation = carT.rotation;
        player.Position = playerT.position;
        player.Rotation = playerT.rotation;
        saveFile.CarTransform = car;
        saveFile.PlayerTransform = player;
        var json = JsonUtility.ToJson(saveFile);
        using (StreamWriter sw = new StreamWriter(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            sw.Write(json);
            sw.Close();
        }
    }
}
