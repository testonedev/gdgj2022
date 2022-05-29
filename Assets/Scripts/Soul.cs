using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;
using UltEvents;

public class Soul : MonoBehaviour
{
    public float interactionDistance = 1f;
    public float interactionTime = 2f;

    public GameObject modelBlue;
    public GameObject modelRed;

    public UltEvent gainFaith;
    public UltEvent loseFaith;

    public UltEvent onEnable;
    public UltEvent onDisable;

    private NavMeshAgent agent;
    private int alignmentPoints;
    private Transform hellEntrance;
    private Transform heavenEntrance;
    private Plot targetPlot;
    private int alignmentVisits;

    //runtime
    private int currentPlotIndex = 0;
    private GameManager gameManager;
    private bool interactionFinished;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        //assuming only have one (singleton)
        gameManager = GameManager.Instance;
        currentPlotIndex = 0;
        alignmentVisits = 0;
        alignmentPoints = 0;

        hellEntrance = HellEntrance.Instance.transform;
        heavenEntrance = HeavenEntrance.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlotInRange())
        {
            StartCoroutine(Interaction());
            targetPlot = null;
        }
    }

    /// <summary>
    /// Trigger for spawner to start the object
    /// </summary>
    public void StartObject()
    {
        onEnable.Invoke();
        MoveToNextTarget();
    }

    public void FinishConversion()
    {
        //in case of point parity, soul goes to hell - easy as that

        if (alignmentPoints > 0) GoToHeaven();
        else if (alignmentPoints < 0) GoToHell();
        else
        {
            if(alignmentVisits > 0) GoToHeaven();
            else if (alignmentVisits <0) GoToHell();
            else GoToHell();
        }
    }

    private void MoveToNextTarget()
    {
        targetPlot = GameManager.GetTargetPlot(currentPlotIndex);
        agent.SetDestination(targetPlot.transform.position);
    }

    private void GoToHeaven()
    {
        agent.SetDestination(heavenEntrance.transform.position);
    }

    [ContextMenu("GoToHell")]
    private void GoToHell()
    {
        agent.SetDestination(hellEntrance.transform.position);
    }

    private bool PlotInRange()
    {
        if (targetPlot == null) return false;
        
        //if in range, use interaction for a while

        if(Vector3.Distance(targetPlot.transform.position, transform.position) <= interactionDistance)
        {
            return true;
        }

        return false;
    }

    private IEnumerator Interaction()
    {
        if (targetPlot.Alignment == Alignment.Heaven)
        {
            gainFaith.Invoke();
            alignmentVisits++;
        }
        else
        {
            loseFaith.Invoke();
            alignmentVisits--;
        }

        if (targetPlot.Alignment == Alignment.Heaven) alignmentPoints += Mathf.Abs(targetPlot.GetAlignmentPoints());
        else alignmentPoints -= Mathf.Abs(targetPlot.GetAlignmentPoints());

        if (alignmentPoints >= 0)
        {
            modelBlue.SetActive(true);
            modelRed.SetActive(false);
        }
        else
        {
            modelBlue.SetActive(false);
            modelRed.SetActive(true);
        }

        interactionFinished = false;

        while (!interactionFinished)
        {
            yield return new WaitForSeconds(interactionTime);
            interactionFinished = true;
        }

        FinishedInteraction();
    }

    private void FinishedInteraction()
    {   
        currentPlotIndex++;

        if(currentPlotIndex < 7)
        {
            MoveToNextTarget();
        }
        else
        {
            FinishConversion();
        }
    }

    public void OnExitWorld(Alignment goToAlignment)
    {
        onDisable.Invoke();
        GameManager.SoulWentToAfterlife(goToAlignment, Mathf.Abs(alignmentPoints));
        LeanPool.Despawn(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<HeavenEntrance>() != null) OnExitWorld(Alignment.Heaven);
        else if (other.GetComponentInParent<HellEntrance>() != null) OnExitWorld(Alignment.Hell);
    }
}
