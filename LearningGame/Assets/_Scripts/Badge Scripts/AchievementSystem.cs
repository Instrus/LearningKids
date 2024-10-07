using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Achievements: MonoBehaviour
{
    public Image GB1,CB1,GB2,CB2,GB3,CB3,GS1,CS1,GS2,CS2,GS3,CS3,GGo1,CGo1,GGo2,CGo2,GGo3,CGo3,GD1,CD1,GD2,CD2,GD3,CD3,GGe1,CGe1,GGe2,CGe2,GGe3,CGe3;
    public GameObject Home_Screen,Shop_Screen,Scores_Screen,Avatar_Screen,Minigames_Screen;
     int bCount=0,sCount=0,goCount=0,dCount=0,geCount=0;
    void Start(){
        CB1.enabled = false;
        CB2.enabled = false;
        CB3.enabled = false;
        CS1.enabled = false;
        CS2.enabled = false;
        CS3.enabled = false;
        CGo1.enabled = false;
        CGo2.enabled = false;
        CGo3.enabled = false;
        CD1.enabled = false;
        CD2.enabled = false;
        CD3.enabled = false;
        CGe1.enabled = false;
        CGe2.enabled = false;
        CGe3.enabled = false;
    }
    void Update()
    {
        if(CB1.enabled == false){
        B1();
        }
        //B2();
        //B3();
        S1();
        //S2();
        //S3();
        Go1();
        //Go2();
        //Go3();
        D1();
        //D2();
        //D3();
        Ge1();
        //Ge2();
        //Ge3();
    }
    public void B1(){
        if(Home_Screen.activeSelf){
            ++bCount;
            GB1.enabled=false;
            CB1.enabled=true;
        }
    }
    /*public void B2(){}
    public void B3(){}
    */
    public void S1(){
        if(Shop_Screen.activeSelf){
            ++sCount;
            GS1.enabled=false;
            CS1.enabled=true;
        }
    }
    /*
    public void S2(){}
    public void S3(){}
    */
    public void Go1(){
        if(Scores_Screen.activeSelf){
            ++goCount;
            GGo1.enabled=false;
            CGo1.enabled=true;
        }
    }
    /*
    public void Go2(){}
    public void Go3(){}
    */
    public void D1(){
        if(Avatar_Screen.activeSelf){
            ++dCount;
            GD1.enabled=false;
            CD1.enabled=true;
        }
    }
    /*
    public void D2(){}
    public void D3(){}
    */
    public void Ge1(){
        if(Minigames_Screen.activeSelf){
            ++geCount;
            GGe1.enabled=false;
            CGe1.enabled=true;
        }
    }
    /*
    public void Ge2(){}
    public void Ge3(){}
    */
}
