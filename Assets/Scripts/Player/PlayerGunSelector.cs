using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    public GunSelector gunSelector;

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Pistol":
                gunSelector.SelectGun((int)GunSelector.Gun.Pistol);
                Destroy(collider.gameObject);
                break;

            case "Rifle":
                gunSelector.SelectGun((int)GunSelector.Gun.Rifle);
                Destroy(collider.gameObject);
                break;

            default:
                break; 
        }
    }
}
