using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASCheck : MonoBehaviour
{
public Achievements achievements;
    public event Action AS;

    void Start(){
         achievements = GameObject.FindObjectOfType<Achievements>();
    }
   public void OnEnable(){
    
    AS+=achievements.Ge1;

    if(achievements.Check[4] == false){
       
        AS?.Invoke();
    }

   }

    private void OnDestroy(){
         AS-=achievements.Ge1;
    }
}
