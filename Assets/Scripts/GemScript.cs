using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public Texture2D cursorTexture;

    [HideInInspector]
    public string colorName;

    [HideInInspector]
    public AppScript appScript;

    public void AnimateGem(float yPos, float duration)
    {
        StartCoroutine(LerpPosition(yPos, duration, AnimComplete));
    }  

    void Start()
    {
        appScript = GameObject.Find("App").GetComponent<AppScript>();
    }

    IEnumerator LerpPosition(float yPos, float duration, Action AnimCompleted)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, yPos, startPosition.z);

        while (time < duration)
        {
            float speed = time/duration;
            //speed = Mathf.Sin(speed * Mathf.PI * 0.5f); //Ease In - https://chicounity3d.wordpress.com/2014
            speed = 1f - Mathf.Cos(speed * Mathf.PI * 0.5f); //Ease Out
            transform.position = Vector3.Lerp(startPosition, targetPosition, speed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        AnimCompleted();
    }

    void AnimComplete(){
        AppScript appScript = GameObject.Find("App").GetComponent<AppScript>();
        appScript.animComplete();
    }

    void OnMouseDown()
    {
        appScript.gemClicked(name);
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
