using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimUI : MonoBehaviour
{
    [SerializeField] 
    private Sprite handCursor;
    [SerializeField] 
    private Sprite cursor;
    
    #region Listener to events

    public void ItemSelected()
    {
        GetComponent<SpriteRenderer>().sprite = handCursor;
    }

    public void Deselection()
    {
        GetComponent<SpriteRenderer>().sprite = cursor;
    }

    #endregion

}
