using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollector : MonoBehaviour
{
    public GameObject chickenContainer;
    public GameObject eggPoolContainer;

    public Animator animator;

    List<GameObject> eggPool = new List<GameObject>();

    uint nbEggsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in eggPoolContainer.transform)
        {
            eggPool.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 baseChickenPosition = chickenContainer.transform.localPosition;
        baseChickenPosition.y = nbEggsCollected * 2 * 0.625f;

        print(nbEggsCollected);
        print(eggPool.Count);

        for (int i = 0; i < nbEggsCollected; i++)
        {
            eggPool[i].SetActive(true);
            eggPool[i].transform.localPosition = new Vector3(baseChickenPosition.x, (i * 2) * 0.625f, 0f);

            chickenContainer.transform.localPosition = baseChickenPosition;
        }

        animator.SetBool("HasEgg", nbEggsCollected > 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Egg")
        {
            nbEggsCollected += 1;
        }
    }
}
