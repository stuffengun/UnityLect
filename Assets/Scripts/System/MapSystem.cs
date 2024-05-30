using Unity.VisualScripting;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject wall;
    public GameObject sheep;

    private int width = 125, height = 125;
    private int seed = 555;

    private int[,] map;
    private System.Random prng;


    void Init(float fillPercent)
    {
        map = new int[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                if (prng.NextDouble() < fillPercent)
                    map[i, j] = 1;



    }

    void GenerateThings()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                if (map[i, j] == 1)
                    ThingSystem.Instance.InstantiateThing(wall, new Vector2Int(i, j));
                //벽이없는곳에 랜덤생성
                else if (prng.NextDouble()<0.005)
                {
                    //양 소환
                    ThingSystem.Instance.InstantiateThing(sheep, new Vector2Int(i, j));
                }
    }

    void Smooth()
    {
        Vector2Int[] neighbor =
        {
            new Vector2Int(0,0),new Vector2Int(1,0),new Vector2Int(-1,0),
        new Vector2Int(0,1),new Vector2Int(0,-1),new Vector2Int(1,1)   ,
        new Vector2Int(1,-1),new Vector2Int(-1,1), new Vector2Int(-1,-1)
        };



        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int count = 0;
                foreach (Vector2Int dir in neighbor)
                {
                    Vector2Int neighborPos = new Vector2Int(i, j) + dir;
                    if(0<=neighborPos.x && neighborPos.x<width && 0<=neighborPos.y&&neighborPos.y<height)
                    {
                        if (map[neighborPos.x,neighborPos.y]==0)
                            count++;
                    }

                }
                map[i,j] = count>=5? 0:1;

            }
        }
    }


    float[,] NewNoise(int scale)
    {
        float[,] noise = new float[width,height];
        float offsetX = prng.Next(-55555, 55555);
        float offsetY = prng.Next(-55555, 55555);

        for (int i = 0;i < width;i++)
        {
            for (int j = 0;j < height;j++)
            {
                float sampleX=(i+offsetX)/scale;
                float sampleY = (j + offsetY) / scale;
                noise[i,j]=Mathf.PerlinNoise(sampleX, sampleY);
            }
        }
        return noise;
    }


    void AddNoise()
    {
        float[,] noise = NewNoise(20);
        for (int i=0;i<width;i++)
            for (int j=0;j<height; j++)
            {
                if (noise[i,j]>0.6)
                {
                    map[i, j] = 1;
                }
            }
    }

    private void Start()
    {
        map = new int[width, height];
        prng = new(seed);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                map[i, j] = 0;
            }
        }

        //셀룰러
        Init(0.48f);
        for (int i = 0; i < 5; i++)
            Smooth();
        GenerateThings();

        //노이즈
        // AddNoise();
        // 

    }

}
