using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour
{
    public GameObject gem;
    private int gemCount;
    private bool doDestroy;

    public void GemClicked(string gemName)
    {
        //Debug.Log(gemName);
        markGems();
    }

    List<List<GameObject>> grid = new List<List<GameObject>>();

    void Start()
    {
        gemCount = 0;
        for (int x = 0; x < 8; x++)
        {
            float xPos = x;
            List<GameObject> column = new List<GameObject>();

            for (int y = 0; y < 8; y++)
            {
                int yPos = y;
                GameObject prefab = createGem(xPos, yPos);
                column.Add(prefab);
            }
            grid.Add(column);
        }

        //updateGrid();
        //printGrid();
    }

    GameObject createGem(float xPos, float yPos)
    {
        int index = Random.Range(0, 7);
        string[] colorNames = new string[] { "red", "green", "blue", "yellow", "magenta", "cyan", "white" };
        Color[] colorCodes = { new Color(1, 0, 0, 1), new Color(0, 1, 0, 1), new Color(0, 0, 1, 1), new Color(1, 1, 0, 1), new Color(1, 0, 1, 1), new Color(0, 1, 1, 1), new Color(1, 1, 1, 1) };
        GameObject prefab = Instantiate(gem, new Vector3(xPos, yPos, 0), Quaternion.identity);
        Renderer renderer = prefab.GetComponentInChildren<Renderer>();
        renderer.material.color = colorCodes[index];
        prefab.GetComponentInChildren<GemScript>().colorName = colorNames[index];
        prefab.name = "gem_" + gemCount;
        gemCount++;
        return prefab;
    }

    void markGems()
    {
        doDestroy = false;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject gem = grid[x][y]; GemScript gemScript = gem.GetComponentInChildren<GemScript>(); string gemColor = gemScript.colorName;

                if (x < 6)
                {
                    GameObject gemX_1 = grid[x + 1][y]; GemScript gemX_1_Script = gemX_1.GetComponentInChildren<GemScript>(); string gemX_1_Color = gemX_1_Script.colorName;
                    GameObject gemX_2 = grid[x + 2][y]; GemScript gemX_2_Script = gemX_2.GetComponentInChildren<GemScript>(); string gemX_2_Color = gemX_2_Script.colorName;
                    if (gemColor == gemX_1_Color && gemColor == gemX_2_Color)
                    {
                        gem.tag = "GemDestroy";
                        gemX_1.tag = "GemDestroy";
                        gemX_2.tag = "GemDestroy";
                        doDestroy = true;
                    }
                }

                if (y < 6)
                {
                    GameObject gemY_1 = grid[x][y + 1]; GemScript gemY_1_Script = gemY_1.GetComponentInChildren<GemScript>(); string gemY_1_Color = gemY_1_Script.colorName;
                    GameObject gemY_2 = grid[x][y + 2]; GemScript gemY_2_Script = gemY_2.GetComponentInChildren<GemScript>(); string gemY_2_Color = gemY_2_Script.colorName;
                    if (gemColor == gemY_1_Color && gemColor == gemY_2_Color)
                    {
                        gem.tag = "GemDestroy";
                        gemY_1.tag = "GemDestroy";
                        gemY_2.tag = "GemDestroy";
                        doDestroy = true;
                    }
                }
            }
        }
        if (doDestroy) { destroyGems(); }
    }

    void destroyGems()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject gem = grid[x][y];
                if (gem.tag == "GemDestroy")
                {
                    grid[x].RemoveAt(y);
                    Destroy(gem);

                    GameObject prefab = createGem(x, y + 8);
                    grid[x].Add(prefab);

                    //GemScript gemScript = gem.GetComponent<GemScript>();
                    //gemScript.AnimateGem(new Vector3(x, y, 0));
                }
            }
        }


        //GameObject[] destroyArray = GameObject.FindGameObjectsWithTag("GemDestroy");

        //foreach (GameObject gem in destroyArray)
        //{
        //int posX = Mathf.RoundToInt(gem.transform.localPosition.x); 
        //int posY = Mathf.RoundToInt(gem.transform.localPosition.y);

        //Destroy(gem);

        //GameObject prefab = createGem(posX, posY + 8);
        //grid[posX].Insert(posY, prefab);
        //}

        animateGems();
        //markGems();
        //printGems();     
    }

    void animateGems()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                GameObject gem = grid[x][y];
                GemScript gemScript = gem.GetComponent<GemScript>();
                int duration = (Mathf.RoundToInt(gem.transform.localPosition.y) - y); Debug.Log(duration);
                gemScript.AnimateGem(y, duration);
            }
        }
    }


    void printGems()
    {
        string printString = "\n ";
        for (int y = 7; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                GameObject gem = grid[x][y];
                if (gem) { printString += gem.name + " "; }
            }
            printString += " \n ";
        }
        print(printString);
    }

    void Update()
    {
       
    }
}
