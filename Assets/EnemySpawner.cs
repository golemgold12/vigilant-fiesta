using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

   public GameObject[] EnemyTypes;
    [SerializeField] float SpawnDelay;
    [SerializeField] List<GameObject> CurrentEnemies = new List<GameObject>();
    bool active = true, turbo = false;
    public int StartingWaveAmnt = 10;
    int currentWave;
    [SerializeField] GameObject EnemyFolder;
    private IEnumerator WaveCoro;

    bool balls = true;
    void Start()
    {
        StartCoroutine(CheckWave());
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            active = false;

            StartCoroutine(StartWave());
        }
        
    }
    IEnumerator CheckWave()
    {
        while (balls == true)
        {
            yield return new WaitForSeconds(10f);
            
            CurrentEnemies.RemoveAll(x => !x);
            if (CurrentEnemies.Count <= 0)
            {
                active = true;

            }
            StartingWaveAmnt = Mathf.RoundToInt(StartingWaveAmnt * 1.2f);
        }



    }

    IEnumerator StartWave()
    {
        for(int i = 0; i < StartingWaveAmnt; i++)
        {
            yield return new WaitForSeconds(.1f);
            int rando = Random.Range(0, 2);
         

                GameObject newEnemy = Instantiate(EnemyTypes[Random.Range(0,EnemyTypes.Length)], EnemyFolder.transform);
                newEnemy.transform.position = EnemyFolder.transform.position + new Vector3(Random.Range(-15.55f, 15.55f), Random.Range(-2.55f, 2.55f), 0);
            newEnemy.GetComponent<EnemyAI>().InitialBoost = Random.Range(150f, 300f);
            CurrentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(SpawnDelay);

        }
        yield return new WaitForSeconds(120f);
        active = true;
    }
}
