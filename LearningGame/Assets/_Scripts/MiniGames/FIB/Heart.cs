using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image fill; // red part of heart

    public bool filled; // to search through filled hearts

    [SerializeField] public Animator anim;
    [SerializeField] public string animation_name;

    private void Awake()
    {
        fill.enabled = true;
        filled = true;
    }

    public void FillHeart()
    {
        fill.enabled = true;
        filled = true;
    }

    public void EmptyHeart()
    {
        fill.enabled = false;
        filled = false;
    }

    public void DestroyHeart()
    {
        anim.SetTrigger("destroy");

        // wait till animation is done playing? turn invis or destroy
    }

}
