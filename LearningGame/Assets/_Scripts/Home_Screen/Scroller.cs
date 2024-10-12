using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    private RawImage _img;
    private float _x, _y;

    private void Awake()
    {
        _x = 0.0025f;
        _y = -0.005f;
        _img = GetComponent<RawImage>();
    }

    private void Start()
    {
        ExperimentalGM.instance.gameStarted += DisableScroller;
        ExperimentalGM.instance.gameFinished += EnableScoller;
    }

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }

    public void EnableScoller()
    {
        gameObject.SetActive(true);
    }

    public void DisableScroller()
    {
        gameObject.SetActive(false);
    }

    
}