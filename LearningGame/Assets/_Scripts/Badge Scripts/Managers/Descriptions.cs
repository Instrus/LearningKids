using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Descriptions : MonoBehaviour
{
    public Achievements achievements;
      public TextMeshProUGUI[] Desc = new TextMeshProUGUI[15];
      public TextMeshProUGUI[] Rew = new TextMeshProUGUI[15];
      public Button[] Toggle = new Button[15];

    void Start(){
        for(int i = 0; i<15; i++){
          achievements.Tasks[i].enabled =false;
          Desc[i].enabled=false;
          Rew[i].enabled=false;
        }
    }

    public void OnClick(){
        
    for (int i = 0; i < Toggle.Length; i++)
        {
            int Index = i; 
            Toggle[Index].onClick.AddListener(() => OnBClick(Index));
        }
    
    }

    public void OnReturn(){
        Start();
    }
        

    public void OnBClick(int Index){
        Start();
      for(int i=0;i<Toggle.Length;i++){
       if(i==Index){
        achievements.Tasks[i].enabled =true;
          Desc[i].enabled=true;
          Rew[i].enabled=true;
       }
      }
    }
}
