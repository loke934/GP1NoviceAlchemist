using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Object pool sizes")] 
    [SerializeField]
    private int poolSizeIngredients = 5;
    [SerializeField] 
    private int poolSizeResetPotion = 5;
    [SerializeField] 
    private int poolSizePotion = 5;

    [Header("Prefabs")] 
    [SerializeField] private GameObject ingredient1Prefab;
    [SerializeField] private GameObject ingredient2Prefab;
    [SerializeField] private GameObject ingredient3Prefab;
    [SerializeField] private GameObject potion1Prefab;
    [SerializeField] private GameObject potion2Prefab;
    [SerializeField] private GameObject potion3Prefab;
    [SerializeField] private GameObject potion4Prefab;
    [SerializeField] private GameObject potion5Prefab;
    [SerializeField] private GameObject potion6Prefab;
    [SerializeField] private GameObject potion7Prefab;
    [SerializeField] private GameObject resetPotionPrefab;

    //Ingredient pools
    private static List<GameObject> ingredient1PoolList;
    private static List<GameObject> ingredient2PoolList;
    private static List<GameObject> ingredient3PoolList;
    //Potion pools
    private static List<GameObject> potion1PoolList;
    private static List<GameObject> potion2PoolList;
    private static List<GameObject> potion3PoolList;
    private static List<GameObject> potion4PoolList;
    private static List<GameObject> potion5PoolList;
    private static List<GameObject> potion6PoolList;
    private static List<GameObject> potion7PoolList;
    private static List<GameObject> resetPotionPoolList;

    private void SetUpPool(int poolSize, GameObject prefab, List<GameObject> list)
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateObject(prefab, list);
        }
    }

    private static GameObject CreateObject(GameObject prefab, List<GameObject> list)
    {
        GameObject item = Instantiate(prefab);
        item.SetActive(false);
        list.Add(item);
        return item;
    }

    public static GameObject GetObject(int property, Vector3 position)
    {
        List<GameObject> list = new List<GameObject>();
        GameObject ingredientToReturn = null;

        switch (property)
        {
            case 1:
                list = ingredient1PoolList;
                break;
            case 2:
                list = ingredient2PoolList;
                break;
            case 3:
                list = ingredient3PoolList;
                break;
            case 4:
                list = potion1PoolList;
                break;
            case 5:
                list = potion2PoolList;
                break;
            case 6:
                list = potion3PoolList;
                break;
            case 7:
                list = potion4PoolList;
                break;
            case 8:
                list = potion5PoolList;
                break;
            case 9:
                list = potion6PoolList;
                break;
            case 10:
                list = potion7PoolList;
                break;
            case 11:
                list = resetPotionPoolList;
                break;
        }

        foreach (GameObject ingredient in list)
        {
            if (!ingredient.activeInHierarchy)
            {
                ingredientToReturn = ingredient;
            }
        }

        if (ingredientToReturn == null)
        {
            ingredientToReturn = CreateObject(list[0], list);
        }

        ingredientToReturn.transform.position = position;
        ingredientToReturn.transform.rotation = Quaternion.identity;
        ingredientToReturn.gameObject.SetActive(true);
        return ingredientToReturn;
    }

    private void Awake()
    {
        ingredient1PoolList = new List<GameObject>(poolSizeIngredients);
        ingredient2PoolList = new List<GameObject>(poolSizeIngredients);
        ingredient3PoolList = new List<GameObject>(poolSizeIngredients);
        potion1PoolList = new List<GameObject>(poolSizePotion);
        potion2PoolList = new List<GameObject>(poolSizePotion);
        potion3PoolList = new List<GameObject>(poolSizePotion);
        potion4PoolList = new List<GameObject>(poolSizePotion);
        potion5PoolList = new List<GameObject>(poolSizePotion);
        potion6PoolList = new List<GameObject>(poolSizePotion);
        potion7PoolList = new List<GameObject>(poolSizePotion);
        resetPotionPoolList = new List<GameObject>(poolSizeResetPotion);

        SetUpPool(poolSizeIngredients, ingredient1Prefab, ingredient1PoolList);
        SetUpPool(poolSizeIngredients, ingredient2Prefab, ingredient2PoolList);
        SetUpPool(poolSizeIngredients, ingredient3Prefab, ingredient3PoolList);
        SetUpPool(poolSizePotion, potion1Prefab, potion1PoolList);
        SetUpPool(poolSizePotion, potion2Prefab, potion2PoolList);
        SetUpPool(poolSizePotion, potion3Prefab, potion3PoolList);
        SetUpPool(poolSizePotion, potion4Prefab, potion4PoolList);
        SetUpPool(poolSizePotion, potion5Prefab, potion5PoolList);
        SetUpPool(poolSizePotion, potion6Prefab, potion6PoolList);
        SetUpPool(poolSizePotion, potion7Prefab, potion7PoolList);
        SetUpPool(poolSizeResetPotion, resetPotionPrefab, resetPotionPoolList);
    }
}

