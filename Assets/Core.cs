using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    GameObject selectionHighlight;
    Vector3 Selection = new Vector3();
    bool wayPointSet = false;
    bool wayPointReached = true;
    public bool unitSelected = false;
    public bool playerActive = false;
    public GameObject selectedUnit;
    public GameObject targetUnit;
    public int[,] gridValues;
    public bool targetSelected = false; 
    GameObject heroes;
    int moveSpeed = 10;
    List<Transform> units = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        selectionHighlight = GameObject.Find("Selection");
        heroes = GameObject.Find("Heroes");
        foreach(Transform child in heroes.transform)
        {
            if (child.tag == "Hero")
                units.Add(child);
        }
        TurnStart();
    }
    void TurnStart()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (unitSelected)
            if ((Mathf.Abs(selectedUnit.transform.position.x - Selection.x) < 1) && (Mathf.Abs(selectedUnit.transform.position.y - Selection.y) < 1))
                wayPointReached = true;
        if ((Input.GetButtonDown("Fire1"))&&(playerActive==true)&&(unitSelected))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log($"mousePos = {mousePos}");
            Selection = new Vector3(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y), 0);
            selectionHighlight.transform.position = Selection;
            if (!wayPointSet && unitSelected)
            {
                wayPointSet = true;
                wayPointReached = false;
            }
        }
        if ((wayPointSet) && (!wayPointReached) && (targetSelected == true))
        {
            Selection = Vector3.MoveTowards(Selection, selectedUnit.transform.position, 1F);
            Move(selectedUnit, Selection, moveSpeed);
            Attack();
            playerActive = false;
            TurnStart();
        }
        if ((wayPointSet) && (!wayPointReached) && (targetSelected == false))
        {
            Move(selectedUnit, Selection, moveSpeed);
            playerActive = false;
            TurnStart();
        }        
    }
    void Move(GameObject character, Vector3 destination, int movement)
    {
        if ((Mathf.Abs(Vector3.Distance(character.transform.position, destination)) < movement))
        {
            character.transform.position = new Vector3(Mathf.Round(destination.x), Mathf.Round(destination.y), 0);
        }
        wayPointSet = false;
        unitSelected = false;
    }
    public void Attack()
    {
        Debug.Log($"{selectedUnit.name} attacks {targetUnit.name}");
        targetSelected = false;   
    }
}