using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    GameObject selectionHighlight;
    Vector3 Selection = new Vector3();
    bool wayPointSet = false;
    bool wayPointReached = true;
    public  bool unitSelected = false;
    public GameObject selectedUnit;
    int moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        selectionHighlight = GameObject.Find("Selection");
    }

    // Update is called once per frame
    void Update()
    {
        if (unitSelected)
            if ((Mathf.Abs(selectedUnit.transform.position.x - Selection.x) < 1) && (Mathf.Abs(selectedUnit.transform.position.y - Selection.y) < 1))
                wayPointReached = true;
        if (Input.GetButtonDown("Fire1"))//������ ����� �� ���� � ��������� ����������, ����� � ������ ��������
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log($"mousePose = {mousePos}");
            Selection = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
            selectionHighlight.transform.position = Selection;
            if (!wayPointSet&&unitSelected)
            {   
                wayPointSet = true;
                wayPointReached = false;
            }
        }
        if (wayPointSet && !wayPointReached)
        {
            Move(selectedUnit, Selection, moveSpeed);
        }
    }
    void Move(GameObject character, Vector3 destination, int movement)//��������� ��������, ����� ��������� �������� ������������ ������, ��������� �������� ��������� � ����������� � ���������� ��������.
    {
        while((Mathf.Abs(Vector3.Distance(character.transform.position, destination))>0.5)&&movement>0)
        {
            if (character.transform.position.x > destination.x)
            {
                Debug.Log("move left");
                character.transform.position = new Vector3(character.transform.position.x - 1F, character.transform.position.y, character.transform.position.z);
                movement -= 1;
            }
            if (character.transform.position.y > destination.y)
            {
                Debug.Log("move down");
                character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y - 1F, character.transform.position.z);
                movement -= 1;
            }
            if (character.transform.position.x < destination.x)
            {
                Debug.Log("move right");
                character.transform.position = new Vector3(character.transform.position.x + 1F, character.transform.position.y, character.transform.position.z);
                movement -= 1; // Было -1, поставил тестово 1
            }
            if (character.transform.position.y < destination.y)
            {
                Debug.Log("move up");
                character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1F, character.transform.position.z);
                movement -= 1;
            }
        }
        if (movement <= 0)
            Debug.Log("Out of Movement");
        character.transform.position = destination;
        wayPointSet = false;
        unitSelected = false;
    }
}