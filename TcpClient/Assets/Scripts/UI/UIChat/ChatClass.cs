using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{

    public class ChatClass : MonoBehaviour
    {
        public Text lab;
        public void SetLab(string str)
        {
            lab.text = str;
        }
        public void Alignment(TextAnchor textAnchor)
        {
            lab.alignment = textAnchor;
        }
    }
}