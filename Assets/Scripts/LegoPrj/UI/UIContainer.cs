using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    virtual public void Show()
    {
        gameObject.SetActive(true);
    }

    virtual public void Hide()
    {
        gameObject.SetActive(false);
    }
}
