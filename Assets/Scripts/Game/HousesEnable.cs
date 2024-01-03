using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousesEnable : MonoBehaviour
{
    [SerializeField]
    private Material greyMat;

    [Space]
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;

    [Space]
    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;

    [Space]
    public GameObject score2;
    public GameObject score3;
    public GameObject score4;

    private void Awake()
    {
        HouseSelect();
    }

    void HouseSelect()
    {
        switch (GameManager.Instance.playerCount)
        {
            case 1:
                {
                    house2.GetComponent<MeshRenderer>().material = greyMat;
                    house3.GetComponent<MeshRenderer>().material = greyMat;
                    house4.GetComponent<MeshRenderer>().material = greyMat;
                    zone2.SetActive(false);
                    zone3.SetActive(false);
                    zone4.SetActive(false);
                    score2.SetActive(false);
                    score3.SetActive(false);
                    score4.SetActive(false);
                }
                break;
            case 2:
                {
                    house3.GetComponent<MeshRenderer>().material = greyMat;
                    house4.GetComponent<MeshRenderer>().material = greyMat;
                    zone3.SetActive(false);
                    zone4.SetActive(false);
                    score3.SetActive(false);
                    score4.SetActive(false);
                }
                break;
            case 3:
                {
                    house4.GetComponent<MeshRenderer>().material = greyMat;
                    zone4.SetActive(false);
                    score4.SetActive(false);
                }
                break;
        }
    }
}
