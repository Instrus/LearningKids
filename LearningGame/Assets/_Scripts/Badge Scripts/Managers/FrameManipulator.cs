using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FrameManip : MonoBehaviour
{
    public Image[] frames = new Image[5];

    public Image title;
    public Sprite[] alt = new Sprite[5];
    public TextMeshProUGUI[] tt = new TextMeshProUGUI[5];

    public Achievements achievements;
    int bCount=0,sCount=0,goCount=0,dCount=0,geCount=0;

    void Start(){
            achievements = GameObject.FindObjectOfType<Achievements>();

            for (int i=0;i<5;i++){
             frames[i].enabled=false;
             tt[i].enabled = false;
            }
    }
        public void onClick(){
            if(bCount!=3){
                bCount=achievements.GetBC();
                if((bCount == 3) && (frames[0].enabled == false) && (frames[1].enabled == false) && (frames[2].enabled == false) && (frames[3].enabled == false) && (frames[4].enabled == false)){
                    bronze();
                }
             }
            
              if(sCount!=3){
                 sCount=achievements.GetSC();
                if((sCount == 3) && (frames[1].enabled == false) && (frames[2].enabled == false) && (frames[3].enabled == false) && (frames[4].enabled == false)){
                     silver();
                }
              }

              if(goCount!=3){
                goCount=achievements.GetGoC();
                if((goCount ==3) && (frames[2].enabled == false) && (frames[3].enabled == false) && (frames[4].enabled == false)){
                    gold();
                }
              }

                if(dCount!=3){
                    dCount=achievements.GetDC();
                    if((dCount==3) && (frames[3].enabled == false) && (frames[4].enabled == false)){
                    diamond();
                    }
                }   
            
                if(geCount!=3){
                    geCount=achievements.GetGeC();
                    if((geCount == 3) && (frames[4].enabled == false)){
                    gem();
                    }
                }   

        }

        public void bronze(){
                    frames[0].enabled=true; 
                   
                    tt[0].enabled=true;
                    
                    title.sprite=alt[0];
        }

        public void silver(){
                    frames[0].enabled=false;
                    frames[1].enabled=true; 
                    
                    tt[0].enabled=false;
                    tt[1].enabled=true;
                    
                    title.sprite=alt[1];
        }


    public void gold(){
                    frames[0].enabled=false;
                    frames[1].enabled=false;
                    frames[2].enabled=true; 
                    
                    tt[0].enabled=false;
                    tt[1].enabled=false;
                    tt[2].enabled=true;

                    title.sprite=alt[2];


        }


     public void diamond(){
                    frames[0].enabled=false;
                    frames[1].enabled=false;
                    frames[2].enabled=false;
                    frames[3].enabled=true; 

                    tt[0].enabled=false;
                    tt[1].enabled=false;
                    tt[2].enabled=false;
                    tt[3].enabled=true;

                    title.sprite=alt[3];
        }


     public void gem(){
                    frames[0].enabled=false;
                    frames[1].enabled=false;
                    frames[2].enabled=false;
                    frames[3].enabled=false;
                    frames[4].enabled=true; 

                    tt[0].enabled=false;
                    tt[1].enabled=false;
                    tt[2].enabled=false;
                    tt[3].enabled=false;
                    tt[4].enabled=true;

                    title.sprite=alt[4];
        }
    
    
}





