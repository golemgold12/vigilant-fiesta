using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Plantable Plant;
    public bool Pickable = false;
    public int FlowPosition;
    [SerializeField] int CurrentGrowthStage;
    [SerializeField] Sprite[] GrowthSprites;
    private bool db = false;
    private bool living = true;
   public int FlowerType = 0;
    [SerializeField] int MaxStage = 5;
    [SerializeField] float GrowChance = .95f;

    private void Start()
    {
        living = true;
    }
    public void Pick()
    {

        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        CurrentGrowthStage = -1; // -1 is essentially dead
        Pickable = false;
        if (Plant && living == true)
        {
            living = false;
            InitRespawn();


        }
    }

    private void FixedUpdate()
    {
        if(Pickable == false && db == false && CurrentGrowthStage >= 0)
        {
            StartCoroutine(GiveIt());

        }
        
    }
    void InitRespawn()
    {
        Plant.RespawnFlower(FlowPosition, gameObject);

    }

    void GrowStage()
    {

        if(CurrentGrowthStage < MaxStage)
        {
            CurrentGrowthStage++;
            gameObject.GetComponent<SpriteRenderer>().sprite = GrowthSprites[CurrentGrowthStage];
            if (CurrentGrowthStage == MaxStage)
            {

                Pickable = true;
            }

        }
    }

    IEnumerator GiveIt()
    {
        db = true;
        yield return null;
        float Roll = Random.value;

        if(Roll >= GrowChance)
        {
            GrowStage();

        }
        yield return new WaitForSeconds(.3f);
        db = false;


    }
}
