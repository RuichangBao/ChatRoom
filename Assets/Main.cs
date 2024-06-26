using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform itemParent;
    public ItemClass itemPrefab;
    private void Start()
    {
        itemPrefab.gameObject.SetActive(false);
    }
}