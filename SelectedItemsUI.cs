using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Handles UI for selectable items*/
public class SelectedItemsUI : MonoBehaviour
{
    [Header("Scale selected object")] 
    [SerializeField, Range(1, 2)] private float scaleAmount = 1.1f;
    
    private bool _isSelected = false;
    private Vector3 _originalScale;
    private Behaviour _halo;

    private Collider _collider;
    
    #region Listener to events

    public void ItemSelected()
    {
        if (!_isSelected)
        {
            _halo.enabled = true;
            transform.localScale *= scaleAmount;
            _isSelected = true;
        }
    }

    public void Deselection()
    {
        if (_isSelected)
        {
            _halo.enabled = false;
            transform.localScale = _originalScale;
            _isSelected = false;
        }
    }

    #endregion
    
    private void Start()
    {
        _originalScale = transform.localScale;
        _halo = (Behaviour) gameObject.GetComponent("Halo");
    }


}
