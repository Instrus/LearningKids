using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//used https://www.youtube.com/watch?v=jPbdFOPkfy0&list=PLBIb_auVtBwBq9S1R-j4oL0HnlDh_rpLW&index=5
//to create avatar screen prototype
public class Customization : MonoBehaviour
{
   public Image part;
   public Sprite[] options;
   public int index;

   void Update(){
    for (int i = 0; i < options.Length; ++i){
        if(i == index){
            part.sprite = options[i];
        }
    }
   }

   public void SwapFor(){
    if(index < options.Length - 1){
        index++;
    } else{
        index = 0;
    }
   }

   public void SwapBack(){
    if(index<options.Length){
        index--;
    }    
    if(index<0){
        index=options.Length-1;
    }
   }
    }

   


