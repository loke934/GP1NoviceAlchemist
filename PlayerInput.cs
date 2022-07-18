using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Handles the player input for picking up and throwing objects.
 Input for Camera rotation is in CameraMovement script.*/
public class PlayerInput : MonoBehaviour
{
    [Header("Settings for obtained object")]
    [SerializeField, Range(1f, 600f)] 
    public float throwForce = 300f;
    [SerializeField] 
    private bool isSpinning;
    [SerializeField] 
    private Transform hand;

    private SelectionManager selectionManager;
    private GameObject obtainedObject;
    private bool isObjectObtained = false;
    
    public UnityEvent<int> OnSpawnInfiniteObjcect;
    public UnityEvent OnPlaySFX;

    public event Action<int> onSpawn;

    private PickUpSFX pickUpSFX;

    #region Properties
    public bool IsObjectObtained
    {
        get
        {
            return isObjectObtained;
        }
    }

    public GameObject ObtainedObject
    {
        set
        {
            obtainedObject = value;
        }
        get
        {
            return obtainedObject;
        }
    }
    #endregion

    private void PickUpObject()
    {
        obtainedObject = selectionManager.SelectedObject;
        if (obtainedObject.CompareTag("Spawn"))
        {
            int property = obtainedObject.GetComponent<ItemScript>().Property; 
            OnSpawnInfiniteObjcect.Invoke(property);
            isObjectObtained = true;
        }
        SetConstraints();
        OnPlaySFX.Invoke();
        isObjectObtained = true;
    }
    private void ThrowObject()
    {
        obtainedObject.transform.position = transform.position;
        Vector3 throwDir = transform.forward;
        RemoveConstraints();
        obtainedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obtainedObject.GetComponent<Rigidbody>().AddForce(throwDir * throwForce);
        obtainedObject = null;
        isObjectObtained = false;
    }
    private void SetConstraints()
    {
        obtainedObject.transform.SetParent(transform);
        obtainedObject.GetComponent<Rigidbody>().useGravity = false;
        obtainedObject.GetComponent<Collider>().enabled = false;
    }
    private void RemoveConstraints()
    {
        obtainedObject.transform.parent = null;
        obtainedObject.GetComponent<Rigidbody>().useGravity = true;
        obtainedObject.GetComponent<Collider>().enabled = true;
        obtainedObject.GetComponent<Rigidbody>().freezeRotation = false;
    }

    private void UpdatePosition()
    {
        obtainedObject.transform.position = hand.position;
    }
    
    private void Awake()
    {
        selectionManager = GetComponent<SelectionManager>();
        OnSpawnInfiniteObjcect = new UnityEvent<int>();
        OnSpawnInfiniteObjcect.AddListener(FindObjectOfType<Spawner>().SpawnObject);
        pickUpSFX = GetComponent<PickUpSFX>();
        OnPlaySFX = new UnityEvent();
        OnPlaySFX.AddListener(pickUpSFX.PlayPickUpSFX);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            if (isObjectObtained)
            {
                ThrowObject();
            }
            else
            {
                if (selectionManager.IsSelected)
                { 
                    PickUpObject();
                }
                return;
            }
        }
        
        if (isObjectObtained)
        {
            UpdatePosition();
            if (isSpinning)
            {
                obtainedObject.GetComponent<Rigidbody>().freezeRotation = true;
            }
        }
    }
}
