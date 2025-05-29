using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject PauseBTN;
    [SerializeField] GameObject InventoryBTN;

    public void OpenInventory()
    {
        Inventory.SetActive(true);
        PauseBTN.SetActive(false);
        InventoryBTN.SetActive(false);

    }


    public void CloseInventory()
    {
        Inventory.SetActive(false);
        PauseBTN.SetActive(true);
        InventoryBTN.SetActive(true);

    }
}
