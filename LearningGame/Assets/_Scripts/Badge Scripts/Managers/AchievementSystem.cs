using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Achievements: MonoBehaviour
{
    //Badge Asset List
    public List<Image[]> BadgeList = new List<Image[]>();
    
    public Image[] Badges= new Image[15];
    public Image[] Tasks= new Image[15];
    public Sprite[] Alternates = new Sprite[6];
    public bool[] Check = new bool[15];
  

     //Achievement Counters for frames
     int bCount=0,sCount=0,goCount=0,dCount=0,geCount=0 ;
     //Player Trackers
     int playerScore=0, playerCurrency=0;
    //Player Data Declaration
    [SerializeField] PlayerData playerData; 

    void Start(){
        //intialize player data
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        //intialize list
        BadgeList.Add(Badges);  
        BadgeList.Add(Tasks); 
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
    }
        public void B1(){
            ++bCount;
           BadgeList[0][0].sprite = Alternates[0];
           BadgeList[1][0].sprite = Alternates[5];
           Check[0] = true;
           
            
        }
    
        public void B2(){
            ++bCount;
            BadgeList[0][5].sprite = Alternates[0];
            BadgeList[1][5].sprite = Alternates[5];
            Check[5] = true;  
        }

        public void B3(){
            ++bCount;
            BadgeList[0][10].sprite = Alternates[0];
            BadgeList[1][10].sprite = Alternates[5];
            Check[10] = true;         
        }

        public void S1(){
            ++sCount;
             BadgeList[0][1].sprite = Alternates[1];
            BadgeList[1][1].sprite = Alternates[5];
            Check[1] = true;  
        }
    
        public void S2(){
            ++sCount;
             BadgeList[0][6].sprite = Alternates[1];
            BadgeList[1][6].sprite = Alternates[5];
            Check[6] = true;  
        }
        public void S3(){
            ++sCount;
            BadgeList[0][11].sprite = Alternates[1];
            BadgeList[1][11].sprite = Alternates[5];
            Check[11] = true;  
        }
    
        public void Go1(){
                ++goCount;
                 BadgeList[0][2].sprite = Alternates[2];
            BadgeList[1][2].sprite = Alternates[5];
            Check[2] = true;  
        }
        
        public void Go2(){
            ++goCount;
            BadgeList[0][7].sprite = Alternates[2];
            BadgeList[1][7].sprite = Alternates[5];
            Check[7] = true;  
        }
    public void Go3(){
           ++goCount;
            BadgeList[0][12].sprite = Alternates[2];
            BadgeList[1][12].sprite = Alternates[5];
            Check[12] = true;  
        }
    
    public void D1(){
            ++dCount;
           BadgeList[0][3].sprite = Alternates[3];
            BadgeList[1][3].sprite = Alternates[5];
            Check[3] = true;  
        }
    
    public void D2(){
        ++dCount;
         BadgeList[0][8].sprite = Alternates[3];
            BadgeList[1][8].sprite = Alternates[5];
            Check[8] = true;  
    }
   public void D3(){
        ++dCount;
         BadgeList[0][13].sprite =Alternates[3];
            BadgeList[1][13].sprite = Alternates[5];
            Check[13] = true;  
    }
    
    public void Ge1(){
            ++geCount;
            BadgeList[0][4].sprite = Alternates[4];
            BadgeList[1][4].sprite = Alternates[5];
            Check[4] = true;  
    }
    
    public void Ge2(){
        ++geCount;
         BadgeList[0][9].sprite = Alternates[4];
            BadgeList[1][9].sprite = Alternates[5];
            Check[9] = true;  
    }
    public void Ge3(){
        ++geCount;
         BadgeList[0][14].sprite = Alternates[4];
            BadgeList[1][14].sprite = Alternates[5];
            Check[14] = true;  
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

}
