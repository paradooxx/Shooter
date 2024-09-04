using UnityEngine;

public class GunSelector : MonoBehaviour
{
    public enum Gun
    {
        Pistol,
        Rifle
    }

    [SerializeField] private GameObject[] gunPrefabs;
    public GameObject[] guns { get; private set; }

    [SerializeField] private int gunIndex;

    public Transform shootPoint;
    public Transform gunSpawnPoint;

    public PlayerController playerController;

    private void Awake()
    {
        gunSpawnPoint = transform;

        guns = new GameObject[gunPrefabs.Length];

        Instantiate(gunPrefabs[gunIndex], gunSpawnPoint);
    }

    private void Start()
    {
        // for(int i = 1 ; i < guns.Length ; i++)
        // {
        //     guns[i].SetActive(false);
        // }

    }

    public void SelectGun(int index)
    {
        for(int i = 0 ; i < guns.Length ; i ++)
        {
            if(i == index)
            {
                guns[i].gameObject.SetActive(true);
            }
            else
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }
}
