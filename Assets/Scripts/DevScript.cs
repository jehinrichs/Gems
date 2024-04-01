using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevScript : MonoBehaviour
{
    public GameObject sphere;

    private GameObject dev;
    private GameObject app;

    public void printGrid(){
        float xOffset = 8f;
        float yOffset = -0.15f;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                AppScript appScript = app.GetComponent<AppScript>();
                List<List<GameObject>> grid = appScript.grid;
                string colorName = grid[x][y].GetComponent<GemScript>().colorName;
                GameObject prefab = createSphere((float) x + xOffset, (float) y + yOffset, colorName);
            }
        }
    }

    void Start()
    {
        dev = GameObject.Find("Dev");
        app = GameObject.Find("App");
    }

    GameObject createSphere(float xPos, float yPos, string colorName)
    {
        Color color;
        string[] colorNames = new string[] { "red", "green", "blue", "yellow", "magenta", "cyan", "white" };
        Color[] colorCodes = { new Color(1, 0, 0, 1), new Color(0, 1, 0, 1), new Color(0, 0, 1, 1), new Color(1, 1, 0, 1), new Color(1, 0, 1, 1), new Color(0, 1, 1, 1), new Color(1, 1, 1, 1) };
        GameObject prefab = Instantiate(sphere, new Vector3(xPos, yPos, 0), Quaternion.identity);
        prefab.transform.parent = dev.transform; //parent prefab to gems gameobject

        for (int i = 0; i < colorNames.Length; i++){
            if(colorNames[i] == colorName){
                color = colorCodes[i];
                Renderer renderer = prefab.GetComponentInChildren<Renderer>();
                renderer.material.color = color;
                break;
            }
        }

        return prefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
