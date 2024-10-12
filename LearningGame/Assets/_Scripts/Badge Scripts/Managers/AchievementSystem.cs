using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Achievements: MonoBehaviour
{
    //Badge Asset List
    public List<Image[]> BadgeList = new List<Image[]>();
    
    public Image[] Badges= new Image[15];
    public Image[] Tasks= new Image[15];
    public Sprite[] Alternates = new Sprite[6];
    public bool[] Check = new bool[15];
    
    public Image pwind, popup, conf;
    public TextMeshProUGUI bEarn, cTot, btext;
     //Achievement Counters for frames
     int bCount=0,sCount=0,goCount=0,dCount=0,geCount=0;
     //Player Trackers
     int playerScore=0, playerCurrency=0, badgesEarned=0, totalRcvd=0;
    //Player Data Declaration
    [SerializeField] PlayerData playerData; 

    void Start(){
        //intialize player data
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        
        Check = playerData.GetUA();
        
        BadgeList.Add(Badges);  
        BadgeList.Add(Tasks); 
        
        pwind.enabled=false;
        popup.enabled=false;
        conf.enabled=false;
        bEarn.enabled=false;
        cTot.enabled=false;
        btext.enabled=false;
        
        for(int i=0;i<Check.Length;i++){
            if(Check[i] == true){
                if(i == 0){
                B1();
                }
                 if(i == 1){
                S1();
                }
                if(i == 2){
                Go1();
                }
                 if(i == 3){
                D1();
                }
                 if(i == 4){
                Ge1();
                }
                if(i == 5){
                B2();
                }
                 if(i == 6){
                S2();
                }
                if(i == 7){
                Go2();
                }
                 if(i == 8){
                D2();
                }
                 if(i == 9){
                Ge2();
                }
                if(i == 10){
                B3();
                }
                 if(i == 11){
                S3();
                }
                if(i == 12){
                Go3();
                }
                 if(i == 13){
                D3();
                }
                 if(i == 14){
                Ge3();
                }
            }

        }
    
    }

    public void OnClick(){
        playerScore = playerData.GetScore();
        playerCurrency = playerData.GetCurrency();

        if((Check[5] == false) && (playerScore >= 50)){
                B2();
        }

        if((Check[6] == false) && (playerScore >= 150)){
                S2();
        }

        if((Check[7] == false) && (playerScore >= 250)){
                Go2();
        }

        if((Check[8] == false) && (playerScore >= 350)){
                D2();
        }

        if((Check[9] == false) && (playerScore >= 500)){
                Ge2();
        }
    
          if((Check[10] == false) && (playerCurrency >= 250)){
                B3();
        }

        if((Check[11] == false) && (playerCurrency >= 350)){
                S3();
        }

        if((Check[12] == false) && (playerCurrency >= 500)){
                Go3();
        }

        if((Check[13] == false) && (playerCurrency >= 600)){
                D3();
        }

        if((Check[14] == false) && (playerCurrency >= 700)){
                Ge3();
        }  

             if(badgesEarned > 0){
            pwind.enabled = true;
            popup.enabled = true;
            conf.enabled=true;
            bEarn.enabled=true;
            cTot.enabled=true;
            btext.enabled=true;

            bEarn.text = $"You Have Earned {badgesEarned} New Badge(s)!";
            cTot.text = $"Reward: ${totalRcvd}";
            playerData.AddCurrency(totalRcvd);
            badgesEarned = 0;
            totalRcvd = 0;

        }

    }
        public void B1(){
            ++bCount;
           BadgeList[0][0].sprite = Alternates[0];
           BadgeList[1][0].sprite = Alternates[5];
            
            if(Check[0] == false){
                totalRcvd+=10;
                ++badgesEarned;
                playerData.SetUA(0);
           }   
        }
    
        public void B2(){
            ++bCount;
            BadgeList[0][5].sprite = Alternates[0];
            BadgeList[1][5].sprite = Alternates[5];
             
             if(Check[5] == false){
                totalRcvd+=10;
                ++badgesEarned;
                playerData.SetUA(5);
           }   
        }

        public void B3(){
            ++bCount;
            BadgeList[0][10].sprite = Alternates[0];
            BadgeList[1][10].sprite = Alternates[5];
            
            if(Check[10] == false){
                totalRcvd+=10;
                ++badgesEarned;
                playerData.SetUA(10);
           }   
        }

        public void S1(){
            ++sCount;
             BadgeList[0][1].sprite = Alternates[1];
            BadgeList[1][1].sprite = Alternates[5];

            if(Check[1] == false){
                totalRcvd+=20;
                ++badgesEarned;
                playerData.SetUA(1);
           }   
        }
    
        public void S2(){
            ++sCount;
             BadgeList[0][6].sprite = Alternates[1];
            BadgeList[1][6].sprite = Alternates[5];

             if(Check[6] == false){
                totalRcvd+=20;
                ++badgesEarned;
                playerData.SetUA(6);
           }   
        }
        public void S3(){
            ++sCount;
            BadgeList[0][11].sprite = Alternates[1];
            BadgeList[1][11].sprite = Alternates[5];
           
            if(Check[11] == false){
                totalRcvd+=20;
                ++badgesEarned;
                playerData.SetUA(11);
           }   
        }
    
        public void Go1(){
                ++goCount;
                 BadgeList[0][2].sprite = Alternates[2];
                BadgeList[1][2].sprite = Alternates[5];
               
               if(Check[2] == false){
                totalRcvd+=30;
                ++badgesEarned;
                playerData.SetUA(2);
           }   
        }
        
        public void Go2(){
            ++goCount;
            BadgeList[0][7].sprite = Alternates[2];
            BadgeList[1][7].sprite = Alternates[5];
           
           if(Check[7] == false){
                totalRcvd+=30;
                ++badgesEarned;
                playerData.SetUA(7);
           }   
        }
    public void Go3(){
           ++goCount;
            BadgeList[0][12].sprite = Alternates[2];
            BadgeList[1][12].sprite = Alternates[5];
           
           if(Check[12] == false){
                totalRcvd+=30;
                ++badgesEarned;
                playerData.SetUA(12);
           }   
        }
    
    public void D1(){
            ++dCount;
           BadgeList[0][3].sprite = Alternates[3];
            BadgeList[1][3].sprite = Alternates[5];
            
            if(Check[3] == false){
                totalRcvd+=40;
                ++badgesEarned;
                playerData.SetUA(3);
           }   
        }
    
    public void D2(){
        ++dCount;
         BadgeList[0][8].sprite = Alternates[3];
            BadgeList[1][8].sprite = Alternates[5];
          
          if(Check[8] == false){
                totalRcvd+=40;
                ++badgesEarned;
                playerData.SetUA(8);
           }   
    }
   public void D3(){
        ++dCount;
         BadgeList[0][13].sprite =Alternates[3];
            BadgeList[1][13].sprite = Alternates[5];
            
            if(Check[13] == false){
                totalRcvd+=40;
                ++badgesEarned;
                playerData.SetUA(13);
           }   
    }
    
    public void Ge1(){
            ++geCount;
            BadgeList[0][4].sprite = Alternates[4];
            BadgeList[1][4].sprite = Alternates[5];

            if(Check[4] == false){
                totalRcvd+=50;
                ++badgesEarned;
                playerData.SetUA(4);
           }   
    }
    
    public void Ge2(){
        ++geCount;
         BadgeList[0][9].sprite = Alternates[4];
            BadgeList[1][9].sprite = Alternates[5];
            
            if(Check[9] == false){
                totalRcvd+=50;
                ++badgesEarned;
                playerData.SetUA(9);
           }   
    }
    public void Ge3(){
        ++geCount;
         BadgeList[0][14].sprite = Alternates[4];
            BadgeList[1][14].sprite = Alternates[5];
            
            if(Check[14] == false){
                totalRcvd+=50;
                ++badgesEarned;
                playerData.SetUA(14);
           }   
    }

    public int GetBC(){
        return bCount;
    }    
    public int GetSC(){
        return sCount;
    }  
    public int GetGoC(){
        return goCount;
    }  
    public int GetDC(){
        return dCount;
    }  
    public int GetGeC(){
        return geCount;
    }

    public void Confirm(){

        pwind.enabled = false;
        popup.enabled = false;
        conf.enabled=false;
        bEarn.enabled=false;
        cTot.enabled=false;
        btext.enabled=false;
    }

}
