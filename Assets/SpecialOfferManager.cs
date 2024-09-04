using System.Collections.Generic;
using UnityEngine;

public class SpecialOfferManager : MonoBehaviour
{
    //if any other offers are added here go to TODO:
    [SerializeField] private GameObject[] offerObject;  //0:unlock all skin+coin, 1:noads+coin
    [SerializeField] private GameObject noOffers;

    private bool[] disableConditions;

    void Start()
    {
        disableConditions = new bool[offerObject.Length];
        noOffers?.SetActive(true);
        ActivateRandomGameObject();
    }

    private void ActivateRandomGameObject()
    {
        FullFillConditions();
        List<int> validIndices = new List<int>();

        for(int i = 0 ; i < offerObject.Length ; i++)
        {
            if(!disableConditions[i])
            {
                validIndices.Add(i);
            }
        }

        if(validIndices.Count > 0)
        {
            int randomIndex = validIndices[Random.Range(0, validIndices.Count)];

            foreach(GameObject obj in offerObject)
            {
                obj.SetActive(false);
            }

            offerObject[randomIndex].SetActive(true);
            noOffers?.SetActive(false);
        }
        else
        {
            noOffers?.SetActive(true);

            foreach(GameObject obj in offerObject)
            {
                obj.SetActive(false);
            }
        }
    }

    public void FullFillConditions()
    {
        disableConditions[0] = IsAllSkinUnlocked();
        disableConditions[1] = GameDataManager.Instance.IsNoAdsSubPurchased;
        // TODO: add disableConditions[2],[3].... and implement when it will be true
    }

    private bool AreAllConditionsMet()
    {
        foreach(bool condition in disableConditions)
        {
            if(!condition)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsAllSkinUnlocked()
    {
        for (int i = 0; i < GameDataManager.Instance.IsPlayerUnlocked.Length; i++)
        {
            if (!GameDataManager.Instance.IsPlayerUnlocked[i])
            {
                return false;
            }
        }
        return true;
    }

}
