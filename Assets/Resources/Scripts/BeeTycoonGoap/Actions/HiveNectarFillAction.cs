using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WorkerBee))]
public class HiveNectarFillAction : GOAPAction
{
    private WorkerBee bee;
    private bool delivering;
    public Canvas nectarTextContainer;
    public UnityEngine.UI.Text nectarText;

    public HiveNectarFillAction()
    {
        addEffect("fillHiveNectar", true);
        cost = 1f;
    }

    void Start()
    {
        bee = GetComponent<WorkerBee>();
    }

    public override void reset()
    {
    }

    public override bool isDone()
    {
        return !BeeHive.main.canStoreNectar() || !bee.hasNectar();
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        var worldState = bee.getWorldState();
        target = BeeHive.main.gameObject;
        return worldState.getBool("hasNectar") && worldState.getBool("hiveCanStoreNectar");
    }

    public override bool perform(GameObject agent)
    {
        float distance = Vector3.Distance(agent.transform.position, target.transform.position);
        if (distance > bee.minTargetDistance)
            return false;
        if (!delivering)
        {
            delivering = true;
            StartCoroutine(deliverNectar(1));
        }
        return true;
    }

    IEnumerator deliverNectar(int amount)
    {
        yield return new WaitForSeconds(1);
        BeeHive.main.deliverNectar(amount);
        bee.nectar -= amount;
        nectarTextContainer.enabled = true;
        nectarText.text = "Nectar\n" + bee.nectar;
        delivering = false;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}