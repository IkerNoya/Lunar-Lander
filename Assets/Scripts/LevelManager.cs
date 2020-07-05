using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    int choice = 0;
    GameManager manager;
    void Start()
    {
        manager = GameManager.Get();
        Random.InitState(System.DateTime.Now.Millisecond);
        choice = Random.Range(0, 3);
        if (choice == 3) choice = 2;
        manager.currentLevelSelection = choice;
        foreach (GameObject go in levels)
        {
            go.SetActive(false);
        }
        levels[choice].SetActive(true);
    }
}