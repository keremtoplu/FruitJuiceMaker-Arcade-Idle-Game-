using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBase : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private bool FreezeRotationX;

    [SerializeField]
    private bool FreezeRotationY;

    [SerializeField]
    private bool FreezeRotationZ;

    // CHECK: Don't forget to set agentSpeed in concrete class,
    // it was assigned in Update method before, from AgentSpeedUpgradable
    protected float agentSpeed;

    protected Node currentNode;

    // Next node that agent is going to. Next node in the path
    private Node nextNode;

    // Agent's target. This node is the final node in the path 
    private Node targetNode;

    protected AgentState agentState;

    protected GraphSolver graphSolver;

    private Stack<Node> path;

    private Vector3 normalizedDir;

    private float distanceTraveledInCurrPath;

    private float currPathLength;

    private Node tempNode;
    private int xRotationValue, yRotationValue, zRotationValue;

    [HideInInspector]
    public Vector3 LookDirection;

    public bool ShouldRotate { get; set; } = true;

    public AgentState AgentState => agentState;
    private event Action ReachedToTarget;


    protected virtual void Start()
    {
        LookDirection = transform.forward;

        xRotationValue = Convert.ToInt32(FreezeRotationX);
        yRotationValue = Convert.ToInt32(FreezeRotationY);
        zRotationValue = Convert.ToInt32(FreezeRotationZ);

        tempNode = currentNode;
    }

    protected virtual void Update()
    {
        if (ShouldRotate && LookDirection != transform.forward)
        {
            SetLookDirection();
        }


        if (targetNode == null) return;

        normalizedDir = (nextNode.transform.position - currentNode.transform.position).normalized;

        var nextStepVector = agentSpeed * Time.deltaTime * normalizedDir;
        var nextStep = nextStepVector.magnitude;

        if (nextStep == 0f)
        {
            distanceTraveledInCurrPath = 0f;
            ValidateAndSetupPath();
        }
        else if (distanceTraveledInCurrPath + nextStep < currPathLength)
        {
            distanceTraveledInCurrPath += nextStep;
            transform.position += nextStepVector;
        }
        else
        {
            distanceTraveledInCurrPath = nextStep - (currPathLength - distanceTraveledInCurrPath);
            ValidateAndSetupPath();
        }

        // Calculate targetRotation
        LookDirection = nextNode.transform.position - currentNode.transform.position;
    }

    protected virtual void OnReachedToTarget()
    {
        targetNode = null;
        agentState = AgentState.ReachedToTarget;
        ReachedToTarget?.Invoke();
    }

    private void ValidateAndSetupPath()
    {
        if (ProcessNextPath())
        {
            normalizedDir = (nextNode.transform.position - currentNode.transform.position).normalized;
            transform.position = currentNode.transform.position + normalizedDir * distanceTraveledInCurrPath;
        }
        else
        {
            OnReachedToTarget();
        }
    }

    public void SetTarget(Node target, Action onComplete = null)
    {
        ReachedToTarget += onComplete;
        SetTarget(currentNode, null, target);
    }

    private void SetTarget(Node source, Node nextNode, Node target)
    {
        if (target == null)
        {
            throw new NullReferenceException("Target can not be null. " +
                "Use SetRandomTarget method for random navigation.");
        }

        targetNode = target;
        distanceTraveledInCurrPath = 0f;
        agentState = AgentState.MovingToTarget;

        path = MakeDesicionForPath(source, nextNode, targetNode);

        //tempNode.transform.position = transform.position;
        tempNode = currentNode;
        path.Push(tempNode);

        ProcessNextPath();
    }

    private bool ProcessNextPath()
    {
        if (path.Count == 1)
        {
            currentNode = targetNode;
            return false;
        }

        currentNode = path.Pop();
        nextNode = path.Peek();
        currPathLength = Vector3.Distance(currentNode.transform.position, nextNode.transform.position);

        return true;
    }

    private Stack<Node> MakeDesicionForPath(Node currentNode, Node nextNode, Node targetNode)
    {
        //find closest path from current node
        var pathFromCurrent = graphSolver.GetClosestPath(
            currentNode,
            targetNode,
            out float distanceFromCurrent
        );

        if (nextNode == null)
            return pathFromCurrent;

        //add my distance from current node
        var totalDistanceFromCurrent = distanceFromCurrent;

        //find closest path from next node
        var pathFromNext = graphSolver.GetClosestPath(
            nextNode,
            targetNode,
            out float distanceFromNext
        );

        //add my distance from next node
        var totalDistanceFromNext = distanceFromNext;

        if (totalDistanceFromCurrent <= totalDistanceFromNext)
        {
            return pathFromCurrent;
        }

        return pathFromNext;
    }

    private void SetLookDirection()
    {
        if (LookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(LookDirection);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // NOTE: we use 1-value because if we are freezing then FreezeX is true
            // XRotVal = 1 so we need to set 1-1 = 0
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x * (1 - xRotationValue),
                transform.localEulerAngles.y * (1 - yRotationValue),
                transform.localEulerAngles.z * (1 - zRotationValue)
            );
        }
    }

    public bool IsReachedToTarget()
    {
        return agentState == AgentState.ReachedToTarget;
    }
}
