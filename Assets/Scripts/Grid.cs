using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<SpawnableObject> gamePrefabs;
    [SerializeField] private Block block;
    [SerializeField] private float margin; // отсутп

    private Cell[,] basePositions; // двумерный массив клеток (базовые позиции)
    private const int maxCountColumns = 13; // колонны
    private const int maxCountRows = 8; // строки

    private UnityEvent TurnEnd = new UnityEvent();

    void Start()
    {
        //Player.LastBullet.AddListener(ShiftDown);
        //Player.LastBullet.AddListener(SpawnLine); // спаун подписываем на эвент
        AddBasePosition();
        SpawnLine();
    }

    void Update()
    {
    }

    public void SpawnLine()
    {
        List<Cell> freePositions = new List<Cell>();

        for (int pos = 0; pos < maxCountColumns; pos++)
            freePositions.Add(basePositions[0, pos]);// в каждую позицию добавляем свободную позицию

        int blockCount = Random.Range(1, maxCountColumns);// от 1 до 12вкл

        for (; blockCount > 0; blockCount--)
        {
            int indexFreePos = Random.Range(0, freePositions.Count);
            Cell freeCell = freePositions[indexFreePos];

            SpawnableObject spawned = Instantiate(GetRandomByWeight(gamePrefabs), freeCell.transform.position, Quaternion.identity);
            TurnEnd.AddListener(spawned.OnTurnEnd);

            freeCell.CurrentObj = spawned.gameObject;
            freePositions.RemoveAt(indexFreePos);
        }
        freePositions.Clear();
    }

    public void ShiftDown()
    {
        for (int i = maxCountRows - 1; i > 0; i--) // с последней строки
        {
            for (int j = 0; j < maxCountColumns; j++) //
            {
                Cell from = basePositions[i - 1, j];
                Cell to = basePositions[i, j];

                GameObject gameObj = from.CurrentObj;

                if (gameObj != null)
                {
                    gameObj.transform.position = to.transform.position;
                    to.CurrentObj = gameObj;
                    from.CurrentObj = null;
                }
            }
        }
    }

    public void OnTurnEnd()
    {
        TurnEnd.Invoke();
    }

    private void AddBasePosition()
    {
        basePositions = new Cell[maxCountRows, maxCountColumns];

        float x = transform.position.x; // расположение сетки по Х 
        float y = transform.position.y; // расположение сетки по У
        float width = block.transform.localScale.x + margin; // ширина = размер блока по Х + отступ
        float height = block.transform.localScale.y + margin; // высота = размер блока по У + отступ

        for (int i = 0; i < basePositions.GetLength(0); i++) // проход по строкам
        {
            for (int j = 0; j < basePositions.GetLength(1); j++) // проход по столбцам 
            {
                GameObject gameObject = new GameObject(); // создаем ИО
                gameObject.transform.SetParent(transform); // созданый объект делаем дочерним
                Cell cellComp = gameObject.AddComponent<Cell>(); // к созданному объекту добавляем компанент Клетка
                cellComp.transform.position = new Vector2(x, y); // ставим на позицию по Х и У

                basePositions[i, j] = cellComp;// базовая позиция заполняется клеткой 
                x += width;
            }
            y -= height;
            x = transform.position.x;
        }
    }

    private SpawnableObject GetRandomByWeight(List<SpawnableObject> chances)
    {
        float sumOfWeights = 0;

        foreach (var c in chances)
            sumOfWeights += c.ChanceSpawn;

        sumOfWeights *= Random.value;

        SpawnableObject selected = null;

        foreach (var chance in chances)
        {
            if (sumOfWeights < chance.ChanceSpawn)
            {
                selected = chance;
                break;
            }
            sumOfWeights -= chance.ChanceSpawn;
        }

        return selected;
    }
}
