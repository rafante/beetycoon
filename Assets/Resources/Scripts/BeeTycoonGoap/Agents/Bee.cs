using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GOAPAgent))]
public abstract class Bee : MonoBehaviour, IGOAP
{
    public float minTargetDistance;
    public string name;
    public int curFood;
    public int curEnergy;
    public int curLife;
    public int curAge;
    public int curArmor;
    public int maxFood;
    public int maxEnergy;
    public int maxLife;
    public int maxAge;
    public int maxArmor;
    private int _foodKey, _energyKey, _lifeKey, _ageKey;
    public float baseSpeed;
    [Range(0, 1)] public float moovingFactor, foodFactor, ageFactor;
    private float _curSpeed;
    public int foodSpend, energySpend, lifeSpend, ageSpend;
    public long foodSpendTime, energySpendTime, lifeSpendTime, ageSpendTime;
    public const int DYING_HUNGRY = 3;
    public const int HUNGRY = 2;
    public const int REGULAR_HUNGER = 1;
    public const int SATISFIED = 0;
    public const int EGG = 0;
    public const int CHILD = 1;
    public const int YOUNG = 2;
    public const int MATURE = 3;
    public const int OLD = 4;
    public GOAPAgent agent;
    public List<string> thoughts;

    // Use this for initialization
    void Start()
    {
        thoughts = new List<string>();
        agent = GetComponent<GOAPAgent>();
        _foodKey = GameManager.main.addCounter(foodSpendTime);
        _energyKey = GameManager.main.addCounter(energySpendTime);
        _lifeKey = GameManager.main.addCounter(lifeSpendTime);
        _ageKey = GameManager.main.addCounter(ageSpendTime);
    }

    public void think(string thoughtKey)
    {
    }

    // Update is called once per frame
    void Update()
    {
        handleFood();
        handleEnergy();
        handleAge();
        handleLife();
    }

    public bool isMoving()
    {
        return _curSpeed > 0;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public float getSpeed()
    {
        return baseSpeed;
    }

    private void handleFood()
    {
        if (curFood <= 0)
        {
            curFood = 0;
            return;
        }
        if (GameManager.main.counterComplete(_foodKey))
        {
            curFood -= foodSpend;
            if (isMoving())
                curFood -= Mathf.FloorToInt(foodSpend * moovingFactor);
        }
    }

    private void handleEnergy()
    {
        if (curEnergy <= 0)
        {
            curEnergy = 0;
            return;
        }
        if (GameManager.main.counterComplete(_energyKey))
        {
            int hangryLevel = getHangerLevel();
            curEnergy -= energySpend;
            if (hangryLevel > REGULAR_HUNGER)
                curEnergy -= Mathf.FloorToInt(energySpend * foodFactor);

            int ageLevel = getAgeLevel();
            if (ageLevel > YOUNG)
                curEnergy -= Mathf.FloorToInt((ageLevel - 2f) * ageFactor);
        }
    }

    private void handleAge()
    {
        if (curAge <= 0)
        {
            curAge = 0;
            die();
            return;
        }
        if (curAge > 0)
        {
            if (GameManager.main.counterComplete(_ageKey))
            {
                curAge -= ageSpend;
            }
        }
    }

    private void handleLife()
    {
        if (GameManager.main.counterComplete(_lifeKey))
        {
            if (curLife <= 0)
            {
                die();
                return;
            }
            if (getHangerLevel() == DYING_HUNGRY)
            {
                curLife -= Mathf.FloorToInt(lifeSpend * foodFactor);
            }
            if (getHangerLevel() == SATISFIED && curFood < maxFood)
            {
                curLife += lifeSpend;
            }
            if (curLife < 0)
                curLife = 0;
            if (curLife > maxLife)
                curLife = maxLife;
        }
    }

    public void die()
    {
        BeesManager.main.killBee(this);
    }

    private int getAgeLevel()
    {
        float agePercent = (curAge * 100.0f) / 100f;
        if (agePercent < 10)
            return EGG;
        if (agePercent < 20)
            return CHILD;
        if (agePercent < 65)
            return YOUNG;
        if (agePercent < 70)
            return MATURE;
        return OLD;
    }

    private int getHangerLevel()
    {
        float foodPercent = (curFood * 100.0f) / 100f;
        if (foodPercent < 10)
            return DYING_HUNGRY;
        if (foodPercent < 33)
            return HUNGRY;
        if (foodPercent <= 50)
            return REGULAR_HUNGER;
        return SATISFIED;
    }

    void OnMouseDown()
    {
        //BeeViewer.main.setBee(this);
    }

    void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 10;
        transform.position = newPos;
    }

    public virtual HashSet<KeyValuePair<string, object>> getWorldState()
    {
        return new HashSet<KeyValuePair<string, object>>()
        {
            new KeyValuePair<string, object>("hasMaxNectar", false),
            new KeyValuePair<string, object>("canLayEgg", false),
            new KeyValuePair<string, object>("canCopulate", false),
        };
    }

    public abstract HashSet<KeyValuePair<string, object>> createGoalState();

    public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
    }

    public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
    {
    }

    public void actionsFinished()
    {
    }

    public void planAborted(GOAPAction aborter)
    {
        aborter.target = null;
    }

    public bool moveAgent(GOAPAction nextAction)
    {
        Vector3 direction = nextAction.target.transform.position - transform.position;
        bool right = nextAction.target.transform.position.x > transform.position.x;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (right)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        transform.Translate(direction * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, nextAction.target.transform.position);
        if (distance < minTargetDistance)
        {
            nextAction.setInRange(true);
            return true;
        }
        else
        {
            nextAction.setInRange(false);
            return false;
        }
    }
}