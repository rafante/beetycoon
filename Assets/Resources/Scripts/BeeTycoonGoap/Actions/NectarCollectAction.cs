using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WorkerBee))]
public class NectarCollectAction : GOAPAction
{
    public string nectarSourceTag;
    public WorkerBee bee;
    private bool collecting;
    public Canvas nectarTextContainer;
    public UnityEngine.UI.Text nectarText;

    void Start()
    {
        nectarTextContainer.enabled = false;
        bee = GetComponent<WorkerBee>();
    }

    public NectarCollectAction()
    {
        addEffect("hasNectar", true);
        cost = 2f;
    }

    public override void reset()
    {
        collecting = false;
    }

    public override bool isDone()
    {
        return bee.nectar == bee.maxNectar;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        var sources = GameObject.FindGameObjectsWithTag(nectarSourceTag);
        Transform closerSource = null;
        float distance = 0;
        float closerDistance = 0;
        if (sources != null && sources.Length > 0)
        {
            closerSource = sources[0].transform;
            closerDistance = Vector3.Distance(closerSource.position, gameObject.transform.position);
            if (sources.Length > 1)
            {
                for (int i = 1; i < sources.Length; i++)
                {
                    var source = sources[i];
                    distance = Vector3.Distance(source.transform.position, gameObject.transform.position);
                    if (distance < closerDistance)
                    {
                        closerDistance = distance;
                        closerSource = source.transform;
                    }
                }
            }
        }
        target = closerSource.gameObject;

        bool sourceCondition = target != null;
        bool nectarCondition = bee.nectar < bee.maxNectar;
        return sourceCondition && nectarCondition;
    }

    public override bool perform(GameObject agent)
    {
        float distance = Vector3.Distance(agent.transform.position, target.transform.position);
        if (distance > bee.minTargetDistance)
            return false;
        if (!collecting)
        {
            collecting = true;
            StartCoroutine(increaseNectar());
        }
        return true;
    }

    IEnumerator increaseNectar()
    {
        yield return new WaitForSeconds(1);
        collecting = false;
        if (bee.nectar < bee.maxNectar)
            bee.nectar++;
        nectarTextContainer.enabled = true;
        nectarText.text = "Nectar\n" + bee.nectar;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}