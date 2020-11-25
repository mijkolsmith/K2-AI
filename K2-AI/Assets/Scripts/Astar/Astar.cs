using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
	Dictionary<Vector2Int, Node> nodeDictionary;
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
		nodeDictionary = new Dictionary<Vector2Int, Node>();

		//set the GScore of the start node to 0
		nodesOpen.Add(new Node(startPos,
					null,
					int.MaxValue,
					//alternatively: (int)Vector2Int.Distance(grid[i, j].gridPosition, endPos)   -   manhattan distance:
					startPos.x - endPos.x + startPos.y - endPos.y,
					grid[startPos.x, startPos.y]));
		nodesOpen[0].GScore = 0;
		nodeDictionary.Add(startPos, nodesOpen[0]);

		while (nodesOpen.Count != 0)
		{//loop until all the nodes have been gone over by the algorithm
			Node currentNode = null;
			//make the currentNode the node with the lowest FScore
			currentNode = nodesOpen.Aggregate((curMin, x) => (curMin == null) || (curMin != null || (x.FScore < curMin.FScore)) ? x : curMin);

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

			//add 4x node (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1) and check for walls
			List<Node> neighbours = new List<Node>();
			if (!currentNode.cell.HasWall(Wall.LEFT)) neighbours.Add(GetNodeFromCell(grid[currentNode.position.x - 1, currentNode.position.y], endPos));
			if (!currentNode.cell.HasWall(Wall.RIGHT)) neighbours.Add(GetNodeFromCell(grid[currentNode.position.x + 1, currentNode.position.y], endPos));
			if (!currentNode.cell.HasWall(Wall.DOWN)) neighbours.Add(GetNodeFromCell(grid[currentNode.position.x, currentNode.position.y - 1], endPos));
			if (!currentNode.cell.HasWall(Wall.UP)) neighbours.Add(GetNodeFromCell(grid[currentNode.position.x, currentNode.position.y + 1], endPos));

			//update the GScore of neighbours
			foreach (Node node in neighbours)
			{
				float tempGScore = currentNode.GScore + 1;
				if (tempGScore < node.GScore)
				{//the new path is shorter, update the GScore and the parent (for pathing)
					node.GScore = tempGScore;
					node.parent = currentNode;
					nodesOpen.Add(node);
				}
			}
		}
		
		return positions;
	}

	private Node GetNodeFromCell(Cell cell, Vector2Int endPos)
	{
		Vector2Int pos = cell.gridPosition;
		if (nodeDictionary.ContainsKey(pos))
		{
			return nodeDictionary[pos];
		}
		else
		{
			Node n = new Node(pos,
				null,
				int.MaxValue,
				pos.x - endPos.x + pos.y - endPos.y,
				cell);
			nodeDictionary.Add(pos, n);
			return n;
		}
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
