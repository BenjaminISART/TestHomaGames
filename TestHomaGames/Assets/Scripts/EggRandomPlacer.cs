using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggRandomPlacer : MonoBehaviour
{
    public GameObject childEgg;
    public float maxOffset = 10f;



    void Start()
    {
        Vector3 modifiedPos = childEgg.transform.localPosition;
        modifiedPos.x = Random.Range(-maxOffset, maxOffset);
        childEgg.transform.localPosition = modifiedPos;
    }



    void Update()
    {
        
    }
}
