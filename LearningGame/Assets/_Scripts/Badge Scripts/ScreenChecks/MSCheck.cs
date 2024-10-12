using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSCheck : MonoBehaviour
{
public Achievements achievements;
    public event Action MS;

    void Start(){
         achievements = GameObject.FindObjectOfType<Achievements>();
    }
   public void OnEnable(){
    
    MS+=achievements.S1;

    if(achievements.Check[1] == false){
       
        MS?.Invoke();
    }

   }

    private void OnDestroy(){
         MS-=achievements.S1;
    }
}
