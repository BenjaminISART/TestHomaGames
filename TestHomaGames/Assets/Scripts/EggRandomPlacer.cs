using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggRandomPlacer : MonoBehaviour
{
    public GameObject childEgg;
    public float maxOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 modifiedPos = childEgg.transform.localPosition;
        modifiedPos.x = Random.Range(-maxOffset, maxOffset);
        childEgg.transform.localPosition = modifiedPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
