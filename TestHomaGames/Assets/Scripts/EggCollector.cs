using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollector : MonoBehaviour
{
    public GameObject chickenContainer;
    public GameObject eggPoolContainer;

    public Animator animator;

    public GameObject psContainer;
    public ParticleSystem psEgg;

    List<GameObject> eggPool = new List<GameObject>();

    uint nbEggsCollected = 0;

    float lerpAnimate = 0f;
    bool animateGet = false;
    bool animateLose = false;



    void Start()
    {
        foreach (Transform child in eggPoolContainer.transform)
        {
            eggPool.Add(child.gameObject);
        }
    }



    void Update()
    {
        ManageAnimations();

        Vector3 baseChickenPosition = chickenContainer.transform.localPosition;
        baseChickenPosition.y = nbEggsCollected * 2 * 0.625f;

        if (nbEggsCollected > 0)
        {
            baseChickenPosition.y -= (2f * 0.625f) * (1f - eggPool[(int)nbEggsCollected - 1].transform.localScale.y);
        }

        for (int i = 0; i < nbEggsCollected; i++)
        {
            eggPool[i].SetActive(true);
            eggPool[i].transform.localPosition = new Vector3(baseChickenPosition.x, (i * 2f) * 0.625f, 0f);
        }
        for (int i = (int)nbEggsCollected; i < eggPool.Count; i++)
        {
            eggPool[i].SetActive(false);
        }

        chickenContainer.transform.localPosition = baseChickenPosition;

        animator.SetBool("HasEgg", nbEggsCollected > 0);
    }



    void ManageAnimations()
    {
        if (animateGet)
        {
            eggPool[(int)nbEggsCollected - 1].transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerpAnimate);
            lerpAnimate += Time.deltaTime * 2f;

            if (lerpAnimate >= 1f)
            {
                animateGet = false;
                eggPool[(int)nbEggsCollected - 1].transform.localScale = Vector3.one;
                lerpAnimate = 0f;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Egg")
        {
            nbEggsCollected += 1;
            other.gameObject.SetActive(false);

            animateGet = true;
        }

        if (other.gameObject.tag == "Fence")
        {
            if (nbEggsCollected > 0)
            {
                psContainer.transform.position = eggPool[0].transform.position;
                psEgg.Play();

                nbEggsCollected -= 1;
            }
            else
            {
                // lose function
            }
        }    

        if (other.gameObject.tag == "OCG")
        {
            other.gameObject.GetComponent<ObstacleCollectibleController>().respawn = true;
        }
    }
}
