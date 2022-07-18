using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Handles selection of items through raycast, adds listeners to UI events*/
public class SelectionManager : MonoBehaviour
{
    [Header("Raycast distance")]
    [SerializeField, Range(10f,50f)]private float rayDist = 20f;

    public UnityEvent OnItemSelected;
    public UnityEvent OnDeselection;
    
    private GameObject _selectedObject;
    private bool _isSelected;

    #region Properties
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
    }

    public GameObject SelectedObject
    {
        get
        {
            return _selectedObject;
        }
    }
    #endregion
    
    private void Awake()
    {
        OnItemSelected = new UnityEvent();
        OnDeselection = new UnityEvent();
        OnItemSelected.AddListener(GetComponentInChildren<AimUI>().ItemSelected);
        OnDeselection.AddListener(GetComponentInChildren<AimUI>().Deselection);
    }
    private void Update()
    {
        if (_isSelected)
        {
            OnDeselection.Invoke();
            _selectedObject = null;
            _isSelected = false;
        }
        
        Vector3 _rayOrigin = transform.position; 
        Vector3 _rayDir = transform.forward;
        RaycastHit hit;
        
        if (Physics.Raycast(_rayOrigin, _rayDir, out hit, rayDist))
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>() || hit.collider.gameObject.CompareTag("Spawn") )
            {
                _selectedObject = hit.collider.gameObject;
                OnItemSelected.Invoke();
                _isSelected = true;
            }
            else
            {
                return;
            }
        }
    }
}
