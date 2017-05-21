using System.Collections.Generic;

public class WorkerBee : Bee
{

    public int nectar;
    public int maxNectar;

    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        var set = new HashSet<KeyValuePair<string, object>>();
        set.Add(new KeyValuePair<string, object>("hasNectar", true));
        set.Add(new KeyValuePair<string, object>("fillHiveNectar", true));

        return set;
    }

    public override HashSet<KeyValuePair<string, object>> getWorldState()
    {
        return new HashSet<KeyValuePair<string, object>>()
        {
            new KeyValuePair<string, object>("hasNectar", nectar > 0),
            new KeyValuePair<string, object>("hasMaxNectar", nectar >= maxNectar),
            new KeyValuePair<string, object>("hiveCanStoreNectar", BeeHive.main.canStoreNectar()),
        };
    }

    public bool hasNectar()
    {
        return nectar > 0;
    }
}