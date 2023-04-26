using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsters;

    [SerializeField]
    private GameObject left, right;

    private GameObject spawnedMonster;
    private GameObject[] positions;

    private int randomPosition;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        positions = new GameObject[2] { left, right };
        StartCoroutine(SpawnMonsters());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            randomPosition = Random.Range(0, positions.Length);
            randomIndex = Random.Range(0, monsters.Length);
            spawnedMonster = Instantiate(monsters[randomIndex]);

            if (randomPosition == 0)
            {
                SetStartingPosition(0);
            }

            else
            {
                SetStartingPosition(1);
            }

            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    private void SetStartingPosition(int position)
    {
        spawnedMonster.transform.position = positions[position].transform.position;
    }
}