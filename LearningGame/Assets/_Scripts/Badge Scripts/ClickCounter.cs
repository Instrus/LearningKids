using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ClickCounter : MonoBehaviour
{
   public Image frame1;
    public Image frame2;

    public Image frame3;

    public Image frame4;

    public Image frame5;


   public Sprite InvisF;
   public Sprite BF;

   public Sprite SF;

   public Sprite GF1;

   public Sprite DF;

   public Sprite GF2;

   int r1C=3;

    int cCount=0;

    private bool fChange=false;

    public void bronzeClick(){
        ++cCount;
    
    if(cCount == r1C){
        frame1.enabled=true;

        frame1.sprite = BF;
        }
    }

     public void silverClick(){
         ++cCount;
         if(cCount == r1C){
        frame1.enabled=false;
        frame2.enabled=true;
        frame2.sprite = SF;
        }
     }
      public void goldClick(){
         ++cCount;
         if(cCount == r1C){
        frame2.enabled=false;
        frame3.enabled=true;
        frame3.sprite = GF1;
        }
      }
       public void diamondClick(){
         ++cCount;
        if(cCount == r1C){
        frame3.enabled=false;
        frame4.enabled=true;
        frame4.sprite = DF;
        }
       }
        public void gemClick(){
             ++cCount;
        if(cCount == r1C){
        frame4.enabled=false;
        frame5.enabled=true;
        frame5.sprite = GF2;
        fChange=true;
        }
    }



}
