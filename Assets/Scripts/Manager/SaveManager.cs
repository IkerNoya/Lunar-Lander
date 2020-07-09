using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    public struct SaveData
    {
        public string Name { get; set; }
        public int Score { get; set; }
    };
    public struct LoadData
    {
        public string Name { get; set; }
        public int Score { get; set; }
    };

    public static void Save(string name, int score)
    {
        string dataPath = Application.persistentDataPath + "Highscore.dat";
        SaveData data = new SaveData();
        data.Name = name;
        data.Score = score;
        FileStream highscore = File.OpenWrite(dataPath);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(highscore, data);
   }
   public static LoadData Load()
   {
        string dataPath = Application.persistentDataPath + "Highscore.dat";
        LoadData data = new LoadData();
        FileStream highscore = File.OpenRead(dataPath);
        BinaryFormatter bf = new BinaryFormatter();
        data = (LoadData)bf.Deserialize(highscore);
        return data;
   }
}
