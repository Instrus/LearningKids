using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSCheck : MonoBehaviour
{
public Achievements achievements;
    public event Action HS;

    void Start(){
         achievements = GameObject.FindObjectOfType<Achievements>();
    }
   public void OnEnable(){
    
    HS+=achievements.B1;

    if(achievements.Check[0] == false){
       
        HS?.Invoke();
    }

   }

    private void OnDestroy(){
         HS-=achievements.B1;
    }
}
