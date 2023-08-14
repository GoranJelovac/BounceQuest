using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ForTestingEmpty : MonoBehaviour
{
    public GameObject pera;
    public GameObject parent;

    private void Start()
    {
        GameObject GO = Instantiate(pera);
        //s1Button.transform.SetParent(ChoiceButtonHolder.transform)
        GO.transform.parent = parent.transform;
    }
}
