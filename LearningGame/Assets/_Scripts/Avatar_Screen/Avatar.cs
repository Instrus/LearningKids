using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public static Avatar Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null){
            Destroy(this.gameObject);
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

}
