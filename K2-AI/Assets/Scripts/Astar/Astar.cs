using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Astar
{
	/// <summary>
	/// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
	/// Note that you will probably need to add some helper functions
	/// from the startPos to the endPos
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="endPos"></param>
	/// <param name="grid"></param>
	/// <returns></returns>
	List<Node> cellsOpen;
	List<Node> cellsClosed;
	List<Vector2Int> positions;

	public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
		cellsOpen = new List<Node>();
		cellsClosed = new List<Node>();
		positions = new List<Vector2Int>();

		cellsOpen.Add(new Node(startPos, null, 0, 0));

		/*for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; i < grid.GetLength(1); j++)
			{
				cellsOpen.Add(new Node(grid[i, j].gridPosition, null, GScore, HScore));
			}
		}*/

		while (cellsOpen.Count != 0)
		{
			Node currentNode = null;
			for (int i = 0; i < cellsOpen.Count; i++)
			{
				int lowestScore = 100;
				if (cellsOpen[i].FScore < lowestScore)
				{
					lowestScore = (int)cellsOpen[i].FScore;
					currentNode = cellsOpen[i];
				}
			}

			if (currentNode != null)
			{
				if (currentNode.position == endPos)
				{
					for (int i = cellsClosed.Count; i > 0; i--)
					{
						positions.Add(cellsClosed[i].position);
					}
				}
				else
				{
					cellsOpen.Remove(currentNode);
					cellsClosed.Add(currentNode);
				}
			}

			List<Node> tempNodes = new List<Node>();
			AddIfNotNull(tempNodes, CreateNode(currentNode.position.x - 1, currentNode.position.y, startPos, endPos, grid, currentNode));
			AddIfNotNull(tempNodes, CreateNode(currentNode.position.x, currentNode.position.y - 1, startPos, endPos, grid, currentNode));
			AddIfNotNull(tempNodes, CreateNode(currentNode.position.x + 1, currentNode.position.y, startPos, endPos, grid, currentNode));
			AddIfNotNull(tempNodes, CreateNode(currentNode.position.x, currentNode.position.y + 1, startPos, endPos, grid, currentNode));

			foreach (Node node in tempNodes)
			{
				if (!cellsClosed.Contains(node) && !cellsOpen.Contains(node))// && is not walkable?
				{
					node.GScore = node.GScore + currentNode.GScore;
					cellsOpen.Add(node);
				}
			}
		}

		return positions;
	}

	private static void AddIfNotNull<T>(List<T> list, T addition)
	{
		if (addition != null)
		{
			list.Add(addition);
		}
	}

	private Node CreateNode(int i, int j, Vector2Int startPos, Vector2Int endPos, Cell[,] grid, Node parent)
	{
		try
		{
			//AStar: https://medium.com/@nicholas.w.swift/easy-a-star-pathfinding-7e6689c7f7b2
			//F = G + H
			//G
			int GScore = (int)(Mathf.Pow(grid[i, j].gridPosition.x - startPos.x, 2) + Mathf.Pow(grid[i, j].gridPosition.y - startPos.y, 2));

			//H
			int HScore = (int)(Mathf.Pow(grid[i, j].gridPosition.x - endPos.x, 2) + Mathf.Pow(grid[i, j].gridPosition.y - endPos.y, 2));

			return new Node(grid[i, j].gridPosition, parent, GScore, HScore);
		}
		catch (IndexOutOfRangeException)
		{
			return null;
		}
	}

	/// <summary>
	/// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
	/// </summary>
	public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
