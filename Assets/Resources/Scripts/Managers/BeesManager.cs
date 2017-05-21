using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesManager : MonoBehaviour
{
    public WorkerBee workerPrefab;
    public BumbleBee bumbleBeePrefab;
    public QueenBee queenBee;
    public Transform respawner;
    public List<Bee> bees;

    public static BeesManager main;

    private void Awake()
    {
        if (main == null)
            main = this;
    }

	// Use this for initialization
	void Start () {
		bees = new List<Bee>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addWorker()
    {
        Bee bee = Instantiate(workerPrefab) as WorkerBee;
        bee.transform.SetPositionAndRotation(respawner.position, Quaternion.identity);
        createBee(bee);
    }

    public void addBumbleBee()
    {
        Bee bee = Instantiate(bumbleBeePrefab) as BumbleBee;
        createBee(bee);
    }

    public void addQueen()
    {
        Bee bee = Instantiate(queenBee) as QueenBee;
        createBee(bee);
    }

    public void createBee(Bee bee)
    {
        bee.setName("#" + bees.Count);
        bees.Add(bee);
        BeeViewer.main.setBee(bee);
    }

    public void killBee(Bee bee)
    {
        bees.Remove(bee);
        Destroy(bee.gameObject);
    }
}
