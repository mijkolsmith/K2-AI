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
	List<Node> nodesOpen;
	List<Vector2Int> positions;

	public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
	{
		//AStar: https://medium.com/@nicholas.w.swift/easy-a-star-pathfinding-7e6689c7f7b2
		/*nodesOpen = new List<Node>();
		cellsClosed = new List<Node>();
		positions = new List<Vector2Int>();

		nodesOpen.Add(new Node(startPos, null, 0, 0));

		for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; i < grid.GetLength(1); j++)
			{
				nodesOpen.Add(new Node(grid[i, j].gridPosition, null, int.MaxValue, (int)Vector2.Distance(grid[i, j].gridPosition, endPos)));
			}
		}

		while (nodesOpen.Count != 0)
		{
			Node currentNode = null;
			for (int i = 0; i < nodesOpen.Count; i++)
			{
				int lowestScore = int.MaxValue;
				if (nodesOpen[i].FScore < lowestScore)
				{
					lowestScore = (int)nodesOpen[i].FScore;
					currentNode = nodesOpen[i];
				}
			}

			if (currentNode != null)
			{
				if (currentNode.position == endPos)
				{
					while(currentNode.parent != null)
					{
						positions.Add(currentNode.position);
						currentNode = currentNode.parent;
					}

					positions.Add(startPos);
					positions.Reverse();
					return positions;
				}
				else
				{
					nodesOpen.Remove(currentNode);
					cellsClosed.Add(currentNode);
				}

				List<Node> tempNodes = new List<Node>();

				AddIfNotNull(tempNodes,
					new Node(grid[currentNode.position.x, currentNode.position.y - 1].gridPosition,
					currentNode, 
					int.MaxValue, 
					(int)Vector2.Distance(grid[currentNode.position.x, currentNode.position.y - 1].gridPosition, endPos)));
				AddIfNotNull(tempNodes,
					new Node(grid[currentNode.position.x, currentNode.position.y - 1].gridPosition,
					currentNode,
					int.MaxValue,
					(int)Vector2.Distance(grid[currentNode.position.x, currentNode.position.y - 1].gridPosition, endPos)));
				AddIfNotNull(tempNodes, 
					new Node(grid[currentNode.position.x + 1, currentNode.position.y].gridPosition,
					currentNode,
					int.MaxValue,
					(int)Vector2.Distance(grid[currentNode.position.x + 1, currentNode.position.y].gridPosition, endPos)));
				AddIfNotNull(tempNodes, 
					new Node(grid[currentNode.position.x, currentNode.position.y + 1].gridPosition,
					currentNode,
					int.MaxValue,
					(int)Vector2.Distance(grid[currentNode.position.x, currentNode.position.y + 1].gridPosition, endPos)));

				foreach (Node node in tempNodes)
				{
					if (!cellsClosed.Contains(node) && !nodesOpen.Contains(node))// && is not walkable?
					{
						node.GScore = node.GScore + currentNode.GScore;
						nodesOpen.Add(node);
					}
				}
			}
		}

		return positions;*/

		//AStar: https://en.wikipedia.org/wiki/A*_search_algorithm
		//initialize an open list and the list to return
		nodesOpen = new List<Node>();
		positions = new List<Vector2Int>();

		//add all the nodes to an open list
		for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; j < grid.GetLength(1); j++)
			{
				nodesOpen.Add(new Node(grid[i, j].gridPosition, 
					null, 
					int.MaxValue, 
					//alternatively: (int)Vector2Int.Distance(grid[i, j].gridPosition, endPos)   -   manhattan distance:
					grid[i,j].gridPosition.x - endPos.x + grid[i, j].gridPosition.y - endPos.y,
					grid[i, j]));
			}
		}

		//set the GScore of the start node to 0
		foreach (Node node in nodesOpen)
		{
			if (node.position == startPos)
			{
				node.GScore = 0;
			}
		}

		while (nodesOpen.Count != 0)
		{//loop until all the nodes have been gone over by the algorithm
			Node currentNode = null;
			for (int i = 0; i < nodesOpen.Count; i++)
			{//make the currentNode the node with the lowest FScore
				int lowestScore = int.MaxValue;
				if (nodesOpen[i].FScore < lowestScore)
				{
					lowestScore = (int)nodesOpen[i].FScore;
					currentNode = nodesOpen[i];
				}
			}

			if (currentNode.position == endPos)
			{//when the endPos is found
				while (currentNode.parent != null)
				{//add all the positions found in reverse order
					positions.Add(currentNode.position);
					currentNode = currentNode.parent;
				}

				//add the startposition
				positions.Add(startPos);
				positions.Reverse();
				nodesOpen = null;
				return positions;
			}

			//remove the current node to not check it again
			nodesOpen.Remove(currentNode);

			//Find 4x node (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) and check for walls
			var neighbours = nodesOpen.Where(n => (n.position.x == currentNode.position.x - 1 && n.position.y == currentNode.position.y && !n.cell.HasWall(Wall.RIGHT)) ||
													(n.position.x == currentNode.position.x + 1 && n.position.y == currentNode.position.y && !n.cell.HasWall(Wall.LEFT)) ||
													(n.position.x == currentNode.position.x && n.position.y == currentNode.position.y - 1 && !n.cell.HasWall(Wall.UP)) ||
													(n.position.x == currentNode.position.x && n.position.y == currentNode.position.y + 1 && !n.cell.HasWall(Wall.DOWN)));

			//update the GScore of neighbours
			foreach (Node node in neighbours)
			{
				float tempGScore = currentNode.GScore + 1;
				if (tempGScore < node.GScore)
				{//the new path is shorter, update the GScore and the parent (for pathing)
					node.GScore = tempGScore;
					node.parent = currentNode;
				}
			}
		}

		return positions;
	}

	/// <summary>
	/// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
	/// </summary>
	public class Node
	{
		public Vector2Int position; //Position on the grid
		public Node parent; //Parent Node of this node
		public Cell cell;

		public float FScore
		{ //GScore + HScore
			get { return GScore + HScore; }
		}
		public float GScore; //Current Travelled Distance
		public float HScore; //Distance estimated based on Heuristic

		public Node() { }
		public Node(Vector2Int position, Node parent, int GScore, int HScore, Cell cell)
		{
			this.position = position;
			this.parent = parent;
			this.GScore = GScore;
			this.HScore = HScore;
			this.cell = cell;
		}
	}
}
