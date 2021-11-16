using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollectibleController : MonoBehaviour
{
    public bool startsEmpty = false;

    public List<GameObject> lowFences = new List<GameObject>(3) { null, null, null };
    public List<GameObject> highFences = new List<GameObject>(3) { null, null, null };

    public List<GameObject> lowEggs = new List<GameObject> { null, null, null };
    public List<GameObject> highEggs = new List<GameObject> { null, null, null };

    [HideInInspector]
    public bool respawn = false;
    float timerRespawn = 5f;



    void Start()
    {
        GeneratePattern();
    }



    void Update()
    {
        if (respawn)
        {
            if (timerRespawn <= 0f)
            {
                GeneratePattern();
                respawn = false;
                timerRespawn = 5f;
            }

            else
            {
                timerRespawn -= Time.deltaTime;
            }
        }
    }



    // -2 = 2 fences / -1 = 1 fence / 0 = nothing / 1 = 1 egg / 2 = 2 eggs
    void GeneratePattern()
    {
        lowFences.ForEach(e => e.SetActive(false));
        highFences.ForEach(e => e.SetActive(false));
        lowEggs.ForEach(e => e.SetActive(false));
        highEggs.ForEach(e => e.SetActive(false));

        if(!startsEmpty)
        {
            float random = Random.Range(-4.5f, 2.5f);
            int pos1 = (random < 0f) ? Mathf.CeilToInt(random) : Mathf.FloorToInt(random);
            random = Random.Range(-4.5f, 2.5f);
            int pos2 = (random < 0f) ? Mathf.CeilToInt(random) : Mathf.FloorToInt(random);
            random = Random.Range(-4.5f, 2.5f);
            int pos3 = (random < 0f) ? Mathf.CeilToInt(random) : Mathf.FloorToInt(random);

            ManageRandomResult(0, pos1);
            ManageRandomResult(1, pos2);
            ManageRandomResult(2, pos3);
        }

        startsEmpty = false;
    }

    void ManageRandomResult(int pos, int result)
    {
        lowFences[pos].SetActive(result <= -1);
        highFences[pos].SetActive(result <= -3);
        lowEggs[pos].SetActive(result >= 1);
        highEggs[pos].SetActive(result >= 2);
    }
}
