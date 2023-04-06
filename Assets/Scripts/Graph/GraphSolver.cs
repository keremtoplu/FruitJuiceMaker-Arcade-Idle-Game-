using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

public class GraphSolver
{

    private Graph graph;

    private SimplePriorityQueue<Node> simplePriorityQueue;

    private float[] dist;

    private List<Node> randomNodeSelectionList;

    private Dictionary<Node, Dictionary<Node, Node>> nodeToClosestPaths;

    public GraphSolver(Graph graph)
    {
        this.graph = graph;
        this.simplePriorityQueue = new SimplePriorityQueue<Node>();
        this.nodeToClosestPaths = new Dictionary<Node, Dictionary<Node, Node>>();
        this.randomNodeSelectionList = new List<Node>(graph.nodes.Count - 1);

        GenerateAllPossiblePaths();
    }

    public Stack<Node> GetClosestPath(Node source, Node target, out float pathDistance)
    {
        pathDistance = 0;
        var stack = new Stack<Node>();
        var u = target;
        var prev = target;
        while (u != source)
        {
            prev = u;
            stack.Push(u);
            u = nodeToClosestPaths[source][u];

            pathDistance += Vector3.Distance(prev.transform.position, u.transform.position);
        }
        stack.Push(source);

        return stack;
    }

    public void GenerateAllPossiblePaths()
    {
        this.dist = new float[graph.nodes.Count];
        nodeToClosestPaths.Clear();

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            FindAllClosestPaths(graph.nodes[i]);
        }
    }

    private void FindAllClosestPaths(Node source)
    {
        simplePriorityQueue.Clear();

        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        dist[source.Index] = 0;

        foreach (var node in graph.nodes)
        {
            if (node != source)
            {
                dist[node.Index] = Mathf.Infinity;
            }
            simplePriorityQueue.Enqueue(node, dist[node.Index]);
        }

        while (simplePriorityQueue.Count > 0)
        {
            var node = (Node)simplePriorityQueue.Dequeue();

            foreach (var outgoing in node.OutgoingList)
            {
                var newDist = dist[node.Index] + Vector3.Distance(node.transform.position, outgoing.transform.position);
                if (newDist < dist[outgoing.Index])
                {
                    dist[outgoing.Index] = newDist;

                    prev[outgoing] = node;
                    simplePriorityQueue.UpdatePriority(outgoing, newDist);
                }
            }
        }
        if (nodeToClosestPaths.ContainsKey(source) == false)
        {
            nodeToClosestPaths.Add(source, prev);
        }
    }
}
