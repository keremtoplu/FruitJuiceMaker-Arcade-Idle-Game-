using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public float radius;
    public Color nodeColor;
    public List<Node> nodes;


    // private void OnDrawGizmos() 
    // {
    //     nodes.Clear();
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         var node = transform.GetChild(i).GetComponent<Node>();
    //         nodes.Add(node);
    //         node.Index = i;

    //         DrawSphere(node);
    //         DrawArrows(node);
    //         DrawNumbers(node, i);
    //        // ConnectNodes(node);
    //     }
    // }

    // private void ConnectNodes(Node node)
    // {
    //     for (int j = 0; j < node.OutgoingList.Count; j++)
    //     {
    //         if (node.OutgoingList[j].IncomingList.Contains(node) == false)
    //         {
    //             node.OutgoingList[j].IncomingList.Add(node);
    //         }
    //     }

    //     for (int j = 0; j < node.IncomingList.Count; j++)
    //     {
    //         if (node.OutgoingList[j].OutgoingList.Contains(node) == false)
    //         {
    //             node.OutgoingList[j].OutgoingList.Add(node);
    //         }
    //     }
    // }

    // private void DrawNumbers(Node node, int index)
    // {
    //     var guiStyle = new GUIStyle();
    //     guiStyle.fontStyle = FontStyle.Bold;
    //     guiStyle.fontSize = 16;
    //     Handles.Label(node.transform.position + Vector3.up * 1.5f, index.ToString(), guiStyle);
    // }

    // private void DrawSphere(Node node)
    // {
    //     node.transform.position = new Vector3(node.transform.position.x, 0, node.transform.position.z);
    //     Gizmos.color = new Color(nodeColor.r, nodeColor.g, nodeColor.b, .9f);
    //     Gizmos.DrawSphere(node.transform.position, radius);
    // }

    // private void DrawArrows(Node node)
    // {
    //     for (int i = 0; i < node.IncomingList.Count; i++)
    //     {
    //         DrawArrow(node.transform.position, node.IncomingList[i].transform.position, Color.blue);
    //     }

    //     for (int i = 0; i < node.OutgoingList.Count; i++)
    //     {
    //         DrawArrow(node.transform.position, node.OutgoingList[i].transform.position, Color.blue);
    //     }
    // }

    // public void DrawArrow(Vector3 pos, Vector3 target, Color color, float arrowHeadLength = 1, float arrowHeadAngle = 20.0f)
    // {
    //     Gizmos.color = color;
    //     var direction = ((target - pos).normalized * (Vector3.Distance(pos, target) - radius));
    //     Gizmos.DrawRay(pos, direction);

    //     Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
    //     Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
    //     Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
    //     Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    // }
}