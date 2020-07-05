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
        levels[0].SetActive(false);
        levels[1].SetActive(false);
        levels[2].SetActive(false);
        choice = manager.levelChoice;
        if (manager.levelChoice == 3)
            choice = 2;
        levels[choice].SetActive(true);
    }
}