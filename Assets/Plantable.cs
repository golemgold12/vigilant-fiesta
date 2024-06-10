using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] FlowerPrefabs; // prefabs of flowers to use

    [SerializeField] GameObject currFlow;
    [SerializeField] List<GameObject> PlantableObjects = new List<GameObject>();
    [SerializeField] int MaxSize = 10;
    
    bool canPlant = true;
    void Start()
    {
        PlantObjects();
    }

    // Update is called once per frame
    void PlantObjects()
    {
        if(canPlant == true && FlowerPrefabs.Length != 0)
        {
            canPlant = false;
            StartCoroutine(PlantObjectCoro(MaxSize, .1f));

        }
    }
    IEnumerator PlantObjectCoro(int amount = 5, float plantspeed = .1f)
    {
        yield return null;

        for(int i = 0; i < amount; i++){

            GameObject newFlower = Instantiate(FlowerPrefabs[Random.Range(0, FlowerPrefabs.Length)],gameObject.transform);
            newFlower.transform.position = gameObject.transform.position + new Vector3(2 * i, 1.33f, 0);
            newFlower.GetComponent<FlowerScript>().FlowPosition = i;
            newFlower.GetComponent<FlowerScript>().Plant = this;

            PlantableObjects.Add(newFlower);


            yield return new WaitForSeconds(plantspeed);
            currFlow = newFlower;
            }
    }
    public void RespawnFlower(int pos, GameObject flowey)
    {
        if (flowey)
        {
            Destroy(flowey, .2f);
            StartCoroutine(RespawnFlow(pos));
        }

    }
    IEnumerator RespawnFlow(int pos)
    {
        yield return new WaitForSeconds(3f);

        GameObject newFlower = Instantiate(FlowerPrefabs[Random.Range(0, FlowerPrefabs.Length)], gameObject.transform);
        newFlower.transform.position = gameObject.transform.position + new Vector3(2 * pos, 1.33f, 0);
        PlantableObjects[pos] = newFlower;
        newFlower.GetComponent<FlowerScript>().Plant = this;
        yield return new WaitForSeconds(.2f);

    }
}
