using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Position for spawning in game")]
    [SerializeField]
    private Transform hand;

    [SerializeField] 
    private PlayerInput playerInput;
    

    #region Metods that listens to events
    public void SpawnObject(int property)
    {
        GameObject ingredient = ObjectPool.GetObject(property, hand.position);
        playerInput.ObtainedObject = ingredient;
    }
    #endregion

}
