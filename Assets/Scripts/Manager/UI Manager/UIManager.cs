using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public virtual void Enable(bool active)
    {
        if (active)
        {
            SetChilds(active);
        }
        else
        {
            SetChilds(active);
        }
    }

    private void SetChilds(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
