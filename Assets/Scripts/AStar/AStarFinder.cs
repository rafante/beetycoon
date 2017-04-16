using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AStarFinder
{
    private IList<Cell> openedList;
    private IList<Cell> closedList;

    private Cell from, to;

    private static AStarFinder finder = null;

    private Cell[,] matrix;
	private Dictionary<int, Vector2> path;

    private AStarFinder()
    {
    }

    /// <summary>
    /// Starter method that sets up the matrix of values that represent the cost of each cell
    /// </summary>
    /// <param name="matrix"></param>
    public void setMatrix(int[,] matrix)
    {
        initMatrix(matrix);
    }

    private void initMatrix(int[,] ints)
    {
        matrix = new Cell[ints.GetLength(0), ints.GetLength(1)];
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                matrix[x, y] = new Cell(x, y, ints[x, y]);
            }
        }
    }

    /// <summary>
    /// Retrieves a 3d array listing the smallest path between (aFrom, bFrom) and (bFrom, bTo)
    /// the array follows the pattern [0 [x,y]], [1 [x,y]] [2 [x,y]] ...
    /// </summary>
    /// <param name="aFrom">x position of the origin</param>
    /// <param name="bFrom">y position of the origin</param>
    /// <param name="aTo">x position of destination</param>
    /// <param name="bTo">y position of destination</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
	public Dictionary<int, Vector2> getPath(int aFrom, int aTo, int bFrom, int bTo)
    {
        if (!isReady())
        {
            throw new Exception("You can't call getPath without before set the matrix calling the setMatrix method");
        }

        if (!validatesFromTo(aFrom, bFrom, aTo, bTo))
        {
			return new Dictionary<int, Vector2>();
        }

        finder.from = getCell(aFrom, aTo);
        finder.to = getCell(bFrom, bTo);

        Debug.Log("Objetivo: " + to);

        setPath();
        return path;
    }

    private int getCost(int x, int y)
    {
        return getCell(x, y).getCost();
    }

    private Cell getCell(int x, int y)
    {
        return matrix[x, y];
    }

    private void setPath()
    {
        Cell curCell;
        openedList = new List<Cell>();
        closedList = new List<Cell>();
        closedList.Add(from);
        curCell = from;
		var index = 0;

		while ((openedList.Count > 0 || !closedList.Contains(to)) && index < 200)
        {
			index++;
            addNeighboorsToOpenedList(curCell);
            curCell = getMinFValue();
			if (curCell == null) {
				Debug.Log ("Célula nula e caminho não encontrado");
				return;
			}
            closedList.Add(curCell);
//            Debug.Log(closedList[closedList.Count - 1]);
            openedList.Remove(curCell);

            if (closedList.Contains(to))
            {
                break;
            }

			//addNeighboorsToOpenedList (curCell);
        }

        if (!closedList.Contains(to))
            path = null;

		List<Cell> reversePath = new List<Cell> ();

		Cell cell = to;
		while (!reversePath.Contains (from)) {
			reversePath.Add (cell);
			cell = cell.parent;
		}

		reversePath.Reverse ();
		path = new Dictionary<int, Vector2> ();

		for (int i = 0; i < reversePath.Count; i++)
        {
			path.Add (i, new Vector2 (reversePath [i].x, reversePath [i].y));
        }

		foreach (var node in path) {
			Debug.Log ((int)node.Value[0] + " : " + (int)node.Value[1]);
		}
        //List<Cell> reverse = (List<Cell>) closedList.Reverse();
    }

    private void addNeighboorsToOpenedList(Cell curCell)
    {
		foreach (Cell cell in getNeighboors(curCell))
		{
			if (!cell.isPassable() || closedList.Contains(cell)) // not walkable or is on closed list
				continue;
			if (!openedList.Contains(cell))
			{
				cell.parent = curCell;
				//cell.update();
				openedList.Add(cell);
			}
			else
			{
				Cell c = new Cell(cell.x, cell.y, cell.type);
				c.parent = curCell;
				if (c.getScore(to) < cell.getScore(to))
				{
					cell.parent = curCell;
				}
			}
		}
    }

    private Cell getMinFValue()
    {
        if (openedList.Count == 0) return null;
		Cell c = openedList [0];
        for (int i = 0; i < openedList.Count; i++)
        {
            Cell cell = openedList[i];
            if (cell.getScore(to) < c.getScore(to))
                c = cell;
        }

        return c;
    }

    /// <summary>
    /// Checks wether the matrix is set
    /// </summary>
    /// <returns></returns>
    public bool isReady()
    {
        return matrix != null && matrix.GetLength(0) > 0 && matrix.GetLength(1) > 0;
    }

    /// <summary>
    /// Returns the AStarFinder static singleton instance
    /// </summary>
    /// <returns></returns>
    public static AStarFinder getInstance()
    {
        if (finder == null)
            finder = new AStarFinder();
        return finder;
    }

    /// <summary>
    /// Checks wether the origin and destination are valid
    /// </summary>
    /// <param name="aFrom"></param>
    /// <param name="bFrom"></param>
    /// <param name="aTo"></param>
    /// <param name="bTo"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private bool validatesFromTo(int aFrom, int bFrom, int aTo, int bTo)
    {
        if (aFrom < 0 || aFrom >= matrix.GetLength(0) || bFrom < 0 || bFrom >= matrix.GetLength(0))
        {
            throw new Exception("Origin is out of bounds");
        }
        if (aTo < 0 || aTo >= matrix.GetLength(0) || bTo < 0 || bTo >= matrix.GetLength(0))
        {
            throw new Exception("Destination is out of bounds");
        }
        if (aFrom == bFrom && aTo == bTo)
        {
            return false;
        }
        return true;
    }

    public List<Cell> getNeighboors(Cell cell)
    {
        return getNeighboors(cell.x, cell.y);
    }

    /// <summary>
    /// Get neighboors
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public List<Cell> getNeighboors(int x, int y)
    {
        List<Cell> neighboors = new List<Cell>();
        int width = matrix.GetLength(0) - 1;
        int height = matrix.GetLength(1) - 1;

        //north
        if (y < height)
        {
            Cell cell = getCell(x, y + 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x, y + 1));
        }
        //northern
        if (x < width && y < height)
        {
            Cell cell = getCell(x + 1, y + 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x + 1, y + 1));
        }
        //east
        if (x < width)
        {
            Cell cell = getCell(x + 1, y);
            if (cell.isPassable())
                neighboors.Add(getCell(x + 1, y));
        }
        //southeast
        if (x < width && y > 0)
        {
            Cell cell = getCell(x + 1, y - 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x + 1, y - 1));
        }
        //south
        if (y > 0)
        {
            Cell cell = getCell(x, y - 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x, y - 1));
        }
        //southwest
        if (x > 0 && y > 0)
        {
            Cell cell = getCell(x - 1, y - 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x - 1, y - 1));
        }
        //west
        if (x > 0)
        {
            Cell cell = getCell(x - 1, y);
            if (cell.isPassable())
                neighboors.Add(getCell(x - 1, y));
        }
        //northwest
        if (x > 0 && y < height)
        {
            Cell cell = getCell(x - 1, y + 1);
            if (cell.isPassable())
                neighboors.Add(getCell(x - 1, y + 1));
        }

        return neighboors;
    }
}

public class Cell
{
    public int x;
    public int y;
    public Cell parent;
    public int type; // the terrain type modifier
    private int cost; // cost from start cell to this cell
    private int hValue; // cost from this cell to the end cell

    public Cell(int x, int y) : this(x, y, 0)
    {
    }

    public Cell(int x, int y, int type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
        this.parent = null;
        this.cost = 0;
        this.hValue = 0;
    }

    public int getCost()
    {
        int c = 10;
        if (parent != null)
        {
            if (isDiagonal(parent))
                c = 12;
            c += parent.getCost();
        }
        return c;
    }
		
    public int getHValue(Cell to)
    {
        return (Mathf.Abs(to.x - x) + Mathf.Abs(to.y - y));
    }

    public bool isDiagonal(Cell from)
    {
        if (from == null) return false;
        return (from.x == x + 1 && from.y == y + 1) || //north east
               (from.x == x + 1 && from.y == y - 1) || //south east
               (from.x == x - 1 && from.y == y - 1) || //south west
               (from.x == x - 1 && from.y == y + 1); //north west
    }


    public int getScore(Cell to)
    {
        return x == to.x && y == to.y ? 0 : getCost() + getHValue(to);
    }

    public override string ToString()
    {
        if (parent != null)
            return "[" + x + " : " + y + "] - cost: " + cost + " parent: [" + parent.x + " : " + parent.y + "] type: " +
                   type;
        return "[" + x + " : " + y + "] - cost: " + cost + " type: " + type;
    }

    public bool isPassable()
    {
        return type != 0;
    }
}