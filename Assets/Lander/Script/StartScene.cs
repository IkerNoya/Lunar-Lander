using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class StartScene : MonoBehaviour
{
    

    void Start()
    {
        if (!GlobalData.GlobalDataCarrier.FirstRun)
        {
            Load();
        }
        else
        {
            
            Save();
        }
    }
    


    public void StartGame()
    {
        GlobalData.GlobalDataCarrier.FirstRun = true;    
        GlobalData.GlobalDataCarrier.Score = 0;
        GlobalData.GlobalDataCarrier.Fuel = 800.0f;
        GlobalData.GlobalDataCarrier.LandedStatus = false;
        GlobalData.GlobalDataCarrier.LandedStatusOk = false;
        GlobalData.GlobalDataCarrier.Level = 1;
        SceneManager.LoadScene("Lander1");
        
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/game.dat"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fStream = File.Open(Application.persistentDataPath + "/game.dat", FileMode.Open);
            SaveManager saver = (SaveManager)binary.Deserialize(fStream);
            fStream.Close();

            GlobalData.GlobalDataCarrier.HighScore = saver.HighScore;

        }
    }

    void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream fStream = File.Create(Application.persistentDataPath + "/game.dat");
        SaveManager saver = new SaveManager();
        saver.HighScore = GlobalData.GlobalDataCarrier.HighScore;

        binary.Serialize(fStream, saver);
        fStream.Close();


    }

    [Serializable]
    class SaveManager
    {
        public int HighScore;

    }

}
