using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private string _targetMessage;
    public Color HighlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = HighlightColor;
        }
    }
    
    public void OnMouseExit()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, transform.localScale.z);
    }
    
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (_targetObject != null)
        {
            _targetObject.SendMessage(_targetMessage);
        }
    }
}
