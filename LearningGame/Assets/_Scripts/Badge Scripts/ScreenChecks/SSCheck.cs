using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSCheck : MonoBehaviour
{
public Achievements achievements;
    public event Action SS;

    void Start(){
         achievements = GameObject.FindObjectOfType<Achievements>();
    }
   public void OnEnable(){
    
    SS+=achievements.D1;

    if(achievements.Check[3] == false){
       
        SS?.Invoke();
    }

   }

    private void OnDestroy(){
         SS-=achievements.D1;
    }
}
