using System.Collections;
using UnityEngine;

public class Abelha : MonoBehaviour {

	private int polen, fadiga, fome, saude;
	private Tarefa tarefaAtual;
    private SortedList tarefas;
    private AStarFinder finder;

	// Use this for initialization
	void Awake ()
	{
	    finder = AStarFinder.getInstance();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void teste()
    {
        int[,] map = new int[,]
        {
            {1, 1,  1,  1,  1,  1},
            {1, 1,  1,  0,  0,  1},
            {1, 1,  1,  0,  1,  1},
            {1, 1,  1,  0,  1,  1},
            {1, 1,  1,  0,  0,  1},
            {1, 1,  1,  1,  1,  1},
            /*{0, 1, 2, 3, 4, 5},
            {6, 7, 8, 9, 10, 11},
            {12, 13, 14, 15, 16, 17},
            {18, 19, 20, 21, 22, 23},
            {24, 25, 26, 27, 28, 29},
            {30, 31, 32, 33, 34, 35},*/
        };
        finder.setMatrix(map);
        finder.getPath(0,1,3,4);
    }

	public void AdicionarTarefa(Tarefa tarefa){
		
	}
}
