using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPanel : MonoBehaviour
{
    public GameObject Panel1;

    public void OpenPanel1()
    {
        if (Panel1 != null)
        {
            bool isActive = Panel1.activeSelf;

            Panel1.SetActive(!isActive);
        }
    }
}
