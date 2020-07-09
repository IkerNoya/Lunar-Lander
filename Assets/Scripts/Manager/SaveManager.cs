using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public struct LoadData
    {
        public string Name { get; set; }
        public int Score { get; set; }
    };

    public static void Save(string name, int score)
    {
        string dataPath = Application.dataPath + "/Files/Highscore.txt";
        FileStream highscore = File.OpenWrite(dataPath);
        BinaryWriter bw = new BinaryWriter(highscore);
        bw.Write(name);
        bw.Write(score);
        bw.Close();
        highscore.Close();
   }
   public static void Load(ref string name, ref int score)
   {
        string dataPath = Application.dataPath + "/Files/Highscore.txt";
        FileStream highscore = File.OpenRead(dataPath);
        BinaryReader bw = new BinaryReader(highscore);
        LoadData data = new LoadData();
        data.Name = bw.ReadString();
        data.Score = bw.ReadInt32();
        bw.Close();
        highscore.Close();
        name = data.Name;
        score = data.Score;
    }
}
