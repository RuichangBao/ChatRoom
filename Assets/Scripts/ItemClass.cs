using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClass : MonoBehaviour
{
    public Button btn;
    public Image image;
    void Start()
    {
        this.btn = GetComponent<Button>();
        this.image = GetComponent<Image>();
    }
}
