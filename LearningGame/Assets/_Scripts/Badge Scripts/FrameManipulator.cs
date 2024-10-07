using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FrameManip : MonoBehaviour
{
    public Image frame1, frame2, frame3, frame4, frame5;
    int bCount=0,sCount=0,goCount=0,dCount=0,geCount=0;


  /*  public void bInc(){

    }
    public void sInc(){
        
    }
    public void goInc(){
        
    }
    public void dInc(){
        
    }
    public void geInc(){
        
    }
    */
    void Start(){
        frame1.enabled=false;
        frame2.enabled=false;
        frame3.enabled=false;
        frame4.enabled=false;
        frame5.enabled=false;
        bronze();
        silver();
        gold();
        diamond();
        gem();
    }
        
        public void bronze(){
            if((frame2.enabled == false) && (frame3.enabled == false) && (frame4.enabled == false) && (frame5.enabled == false)){
            
                if(bCount == 3){
                    frame1.enabled=true;
                }
            }
        }

        public void silver(){
            if((frame3.enabled == false) && (frame4.enabled == false) && (frame5.enabled == false)){
            
                if(sCount == 3){
                    frame1.enabled=false;
                    frame2.enabled=true;
                 }
            }
        }

     public void gold(){
        if((frame4.enabled == false) && (frame5.enabled == false)){
            
            if(goCount == 3){
                frame2.enabled=false;
                frame3.enabled=true;
            }
        }
    }

     public void diamond(){
        if(frame5.enabled == false){
            
            if(dCount == 3){
                frame3.enabled=false;
                frame4.enabled=true;
            }
        }
    }

     public void gem(){
            if(geCount == 3){
                frame4.enabled=false;
                frame5.enabled=true;
            }
    }
    
    
}





