using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoBehaviour
{
    // Declare and initialize a new List of GameObjects called currentCollisions.
    private List <GameObject> currentCollisions = new List <GameObject> ();
    private GameObject plate;
    private GameObject completed;
    [SerializeField]private ToDoListManager toDoListManager;
    private bool win;
    private List<GameObject> objectsOnPlate = new List<GameObject>();
    private List<GameObject> objectsOutOfPlate = new List<GameObject>();
    [SerializeField] private List<string> objectsOnPlateName = new List<string>();
    [SerializeField] private List<string> objectsOutOfPlateName = new List<string>();
    private List<bool> objectsOnPlatebool = new List<bool>();
    private List<bool> objectsOutOfPlatebool = new List<bool>();
    [SerializeField]private int ordersLeft;
    private string[] foodNames = new string[] { "cherry cupcake", "blue cupcake", "black cupcake", "ddl cupcake", "oreo cupcake", 
                                                "light pink donut", "pink donut", "blue donut", "yellow donut",
                                                "chocolateCake"};
    private GameObject[] foodGOs;

    private void Start()
    {
       // por definicion objectsOnplateBool ya esta seteada toda en false
        win = false;
        completed = GameObject.Find("order completed");
        completed.SetActive(false);
        ordersLeft = 0;
        GameObject.Find("screen").GetComponent<material>().changeMaterial(0);
        GameObject.Find("todoList").GetComponent<material>().changeMaterial(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (!win && ordersLeft>0)
        {
            // we forget if anything is on tray
            setToFalseOnPlate(objectsOnPlatebool);
            setToFalseOutOfPlate(objectsOutOfPlatebool);
            objectsOnPlate.Clear();
            objectsOutOfPlate.Clear();

            // Print the entire list to the console.
            foreach (GameObject gObject in currentCollisions)
            {
                
                for (int i = 0; i < objectsOutOfPlateName.Count; i++)
                {
                    //print("is " + objectsOutOfPlateName[i] + " " + gObject + " out of plate?");
                    if (gObject.name.Contains(objectsOutOfPlateName[i]))
                    {
                        //print("yes");
                        if (objectsOnPlateName[i].Contains("mug"))
                        {
                            GameObject coffeeGO = gObject.transform.GetChild(1).gameObject;
                            if (coffeeGO.GetComponent<fillThisMug>().isFilled())
                            {
                                objectsOutOfPlatebool[i] = true;
                                //print("out of plate position " + i + " is " + objectsOutOfPlatebool[i]);
                                //print(objectsOutOfPlateName[i]);
                                objectsOutOfPlate.Add(gObject);
                            }
                        }
                        else
                        {
                            objectsOutOfPlatebool[i] = true;
                            //print("out of plate position " + i + " is " + objectsOutOfPlatebool[i]);
                            //print(objectsOutOfPlateName[i]);
                            objectsOutOfPlate.Add(gObject);
                        }
                        
                    }
                }

                if (gObject.name.Contains("plate"))
                {
                    plate = gObject;
                    //print("plate");
                    foreach (GameObject objOnPlate in gObject.GetComponent<PlateManager>().objectsOnPlate())
                    {
                        
                        for (int i = 0; i < objectsOnPlateName.Count; i++)
                        {
                            //print("is " + objectsOnPlateName[i] +" " +objOnPlate+ " on plate?");
                            if (objOnPlate.name.Contains(objectsOnPlateName[i]))
                            {
                                objectsOnPlate.Add(objOnPlate);
                                objectsOnPlatebool[i] = true;
                            }
                            else
                            {
                                //print("no");
                            }
                        } 
                        

                    }
                }
            }
            if (isArrayTrue(objectsOnPlatebool) && isArrayTrue(objectsOutOfPlatebool))
            {
                win = true;
                StartCoroutine(Win());
            }
        }
        
        
        
    }

    private bool isArrayTrue(List<bool> array)
    {
        for (int i = 0; i < array.Count; ++i)
        {
            if (!array[i])
            {
                return false;
            }
        }

        return true;
    }

    private void setToFalseOnPlate(List<bool> array)
    {
        array.Clear();
        for (int i = 0; i < objectsOnPlateName.Count ; i++)
        {
            array.Add(false);
        }
            
    }

    private void setToFalseOutOfPlate(List<bool> array)
    {
        array.Clear();
        for (int i = 0; i < objectsOutOfPlateName.Count; i++)
        {
            array.Add(false);
        }

    }
    void OnTriggerEnter (Collider col) {
            currentCollisions.Add(col.gameObject);
    }
 
    void OnTriggerExit (Collider col) {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove (col.gameObject);
    }
    IEnumerator Win()
    {
        completed.SetActive(true);

        removeOrder();
        yield return new WaitForSeconds(1);

        foreach (GameObject o in objectsOnPlate)
        {
            GameObject.Destroy(o);
        }
        GameObject.Destroy(plate);
        foreach (GameObject o in objectsOutOfPlate)
        {
            GameObject.Destroy(o);
        }
        // Make person leave
        GameObject.Find("people").GetComponent<CustomerManager>().orderCompleted();
        yield return new WaitForSeconds(1); 
        win = false;
        currentCollisions.Clear();
        completed.SetActive(false);
        
    }

    public void addOrder()
    {
        ordersLeft++;
        if (ordersLeft == 1)
        {
            objectsOnPlateName.Clear();
            objectsOnPlateName.AddRange(getRandomOrder());
        }
        foodGOs = toDoListManager.changeFood(objectsOnPlateName[0], objectsOnPlateName[1]);
        GameObject.Find("screen").GetComponent<material>().changeMaterial(1);
        GameObject.Find("todoList").GetComponent<material>().changeMaterial(1);
        
    }
    public void removeOrder()
    {
        ordersLeft--;
        GameObject.Find("screen").GetComponent<material>().changeMaterial(0);
        GameObject.Find("todoList").GetComponent<material>().changeMaterial(0);
        foreach(GameObject foodGO in foodGOs){
            Destroy(foodGO);
        }
    }

    public List<string> getRandomOrder(){
        List<string> ans = new List<string>();
        int i1 = Random.Range(0, foodNames.Length);
        int i2;
        do{
            i2 = Random.Range(0, foodNames.Length);
        } while(i1 == i2);
        ans.Add(foodNames[i1]);
        ans.Add(foodNames[i2]);
        return ans;
    }

}
