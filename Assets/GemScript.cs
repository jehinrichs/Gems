using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public Texture2D cursorTexture;

    [HideInInspector]
    public string colorName;

    [HideInInspector]
    public float yDestination;

    [HideInInspector]
    public AppScript appScript;

    public void AnimateGem(float yPos, int duration)
    {
        StartCoroutine(LerpPosition(yPos, duration));
    }  


    void Start()
    {
        appScript = GameObject.Find("App").GetComponent<AppScript>();
    }

    IEnumerator LerpPosition(float yPos, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, yPos, startPosition.z);

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    void OnMouseDown()
    {
        appScript.GemClicked(name);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        

        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name + " " + hit.transform.localPosition.x + " " + hit.transform.localPosition.y);
            }
        }*/
    }
}