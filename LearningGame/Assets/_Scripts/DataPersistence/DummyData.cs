using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyData : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int scoreCount;

    public void LoadData(GameData data)
    {
        this.scoreCount = data.score;
    }

    public void SaveData(ref GameData data) { 
        data.score = this.scoreCount;
    }
}
