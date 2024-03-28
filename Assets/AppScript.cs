using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour
{
    public GameObject gem;

    private GameObject firstGem;
    private GameObject secondGem;
    private int gemCount;
    private int animCount;
    private bool doDestroy;
    private List<List<GameObject>> grid = new List<List<GameObject>>();

    public void gemClicked(string gemName)
    {
        if (animCount == 0)
        {
            if (firstGem == null)
            {
                firstGem = GameObject.Find(gemName);
            }
            else if(firstGem.name != gemName)
            {
                secondGem = GameObject.Find(gemName);
            }
            
            if(firstGem != null && secondGem != null){
                Vector3 firstGemPos = firstGem.transform.localPosition;
                Vector3 secondGemPos = secondGem.transform.localPosition;
                Vector3 firstGemPos_new = new Vector3(secondGemPos.x, secondGemPos.y, 0);
                Vector3 secondGemPos_new = new Vector3(firstGemPos.x, firstGemPos.y, 0);
                //gem[firstGemPos_new.x][firstGemPos_new.y] = firstGem;
                //gem[secondGemPos_new.x][secondGemPos_new.y] = secondGem;
                //swap gens
            }
        };
    }

    public void animComplete()
    {
        animCount++; //count completed animations
        if (animCount == 64)
        {
            animCount = 0;
            markGems();
        }
    }

    void Start()
    {
        gemCount = 0;
        for (int x = 0; x < 8; x++)
        {
            List<GameObject> column = new List<GameObject>();
            for (int y = 0; y < 8; y++)
            {
                GameObject prefab = createGem(x, y);
                column.Add(prefab);
            }
            grid.Add(column);
        }
        markGems();
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
            int yPos = 7;
            for (int y = 0; y < 8; y++)
            {
                GameObject gem = grid[x][y];
                if (gem.tag == "GemDestroy")
                {
                    yPos++;
                    grid[x].RemoveAt(y);
                    Destroy(gem);

                    GameObject prefab = createGem(x, yPos);
                    grid[x].Add(prefab);
                }
            }
        }
        animateGems();
    }

    void animateGems()
    {
        animCount = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                GameObject gem = grid[x][y];
                GemScript gemScript = gem.GetComponent<GemScript>();
                int duration = (Mathf.FloorToInt(gem.transform.localPosition.y) - y);
                gemScript.AnimateGem(y, duration * 0.25f);
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
