using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoSCheck : MonoBehaviour
{
public Achievements achievements;
    public event Action ScoS;

    void Start(){
         achievements = GameObject.FindObjectOfType<Achievements>();
    }
   public void OnEnable(){
    
    ScoS+=achievements.Go1;

    if(achievements.Check[2] == false){
       
        ScoS?.Invoke();
    }

   }

    private void OnDestroy(){
         ScoS-=achievements.Go1;
    }
}
