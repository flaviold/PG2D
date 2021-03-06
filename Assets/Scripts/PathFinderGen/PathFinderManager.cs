﻿using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public enum NodeState
{
    Untested,
    Open,
    Closed
}

public class PathFinderManager : MonoBehaviour {
    public List<PathPoint> points;

    private List<PathNode> nodes;

    PathNode startNode;
    PathNode targetNode;

    public List<Vector3> FindPath(Vector3 start, Vector3 target)
    {
		if (points != null || points.Count <= 0) BuildPoints();
        List<Vector3> path = new List<Vector3>();
        nodes = PointsToNodes();
        startNode = GetNodeFromVector(start);
		targetNode = GetNodeFromVector(target);
        fillHFromNodes(targetNode);

        if (Search(startNode))
        {
            PathNode node = targetNode;
            while (node.Parent != null)
            {
                path.Add(node.location);
                node = node.Parent;
            }
            path.Reverse();
        }

        return path;
    }

    public void fillHFromNodes(PathNode targetNode)
    {
        foreach (var node in nodes)
        {
            node.FillH(targetNode);
        }
    }

    private bool Search(PathNode currentNode)
    {
        currentNode.state = NodeState.Closed;
        List<PathNode> nextNodes = GetSiblingNodes(currentNode);

        // Sort by F-value so that the shortest possible routes are considered first
        nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        foreach (var nextNode in nextNodes)
        {
            // Check whether the end node has been reached
            if (nextNode.location == targetNode.location)
            {
                return true;
            }
            else
            {
                // If not, check the next set of nodes
                if (Search(nextNode)) // Note: Recurses back into Search(Node)
                    return true;
            }
        }

        // The method returns false if this path leads to be a dead end
        return false;
    }

    private List<PathNode> GetSiblingNodes(PathNode fromNode)
    {
        List<PathNode> walkableNodes = new List<PathNode>();

        foreach (var sId in fromNode.siblings)
        {

            PathNode node = nodes.Where(n => n.id == sId).Single();

            if (node == null)
            {
                continue;
            }
            // Ignore already-closed nodes
            if (node.state == NodeState.Closed)
                continue;

            // Already-open nodes are only added to the list if their G-value is lower going via this route.
            if (node.state == NodeState.Open)
            {
                float traversalCost = node.GetDistance(node.location, fromNode.location);
                float gTemp = fromNode.G + traversalCost;
                if (gTemp < node.G)
                {
                    node.Parent = fromNode;
                    walkableNodes.Add(node);
                }
            }
            else
            {
                // If it's untested, set the parent and flag it as 'Open' for consideration
                node.Parent = fromNode;
                node.state = NodeState.Open;
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }

    private PathNode GetNodeFromVector(Vector3 point)
    {
        PathNode bestNode = null;
        float bestDist = 0f;
        foreach (var node in nodes)
        {
            if (bestNode == null)
            {
                bestNode = node;
                bestDist = bestNode.GetDistance(bestNode.location, point);
                continue;
            }

            var currentNodeDist = node.GetDistance(node.location, point);
            if (currentNodeDist < bestDist)
            {
                bestNode = node;
                bestDist = currentNodeDist;
            }
        }

        return bestNode;
    }
    private PathNode GetNodeForTarget(Vector3 target)
    {
        PathNode bestNode = null;
        var positivo = (target.x >= 0);
        float bestDist = 0f;
        foreach (var node in nodes)
        {
            if (bestNode == null)
            {
                bestNode = node;
                bestDist = Mathf.Abs(bestNode.location.y - target.y);
                continue;
            }
            var currentNodeDist = Mathf.Abs(node.location.y - target.y);
            if (positivo)
            {
                if ((node.location.x <= 0) && (currentNodeDist < bestDist))
                {
                    bestNode = node;
                    bestDist = currentNodeDist;
                }
            }
            else
            {
                if ((node.location.x >= 0) && (currentNodeDist < bestDist))
                {
                    bestNode = node;
                    bestDist = currentNodeDist;
                }
            }
        }

        return bestNode;
    }

    private List<PathNode> PointsToNodes()
    {
        List<PathNode> nodes = new List<PathNode>();
        
        //First turn all points to nodes
        foreach (var point in points)
        {
            nodes.Add(new PathNode()
            {
                id = point.id,
                siblings = point.siblings,
                location = point.transform.position,
            });
        }

        return nodes;
    }

    public void BuildPoints()
    {
		var mapPoints = GameObject.FindGameObjectsWithTag("PathPoint");
        points = new List<PathPoint>();
		foreach (var p in mapPoints)
        {
            var point = p.gameObject.GetComponent<PathPoint>();
            points.Add(point);
        }
    }

    public float GetDistance(Vector3 a, Vector3 b)
    {
        var dX = a.x - b.x;
        var dY = a.y - b.y;
        var dZ = a.z - b.z;
        return Mathf.Sqrt((dX * dX) + (dY * dY) + (dZ * dZ));
    }
}

public class PathNode {

    public int id;

    public float G = 0;

    public float H;

    public float F
    {
        get { return G + H; }
    }

    public NodeState state = NodeState.Untested;

    public List<int> siblings;

    public Vector3 location;

    private PathNode parent;

    public PathNode Parent
    {
        get { return parent; }
        set
        {
            parent = value;
            G = parent.G + GetDistance(location, parent.location);
        }
    }

    public float GetDistance(Vector3 a, Vector3 b)
    {
        var dX = a.x - b.x;
        var dY = a.y - b.y;
        var dZ = a.z - b.z;
        return Mathf.Sqrt((dX*dX) + (dY*dY) + (dZ*dZ));
    }

    public void FillH(PathNode endPoint)
    {
        H = GetDistance(location, endPoint.location);
    }
}

