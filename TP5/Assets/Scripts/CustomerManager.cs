using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> customers;
    [SerializeField] private List<GameObject> currentCustomers;
    private List<afterOrderType> afterOrderTypesLeft;

    public enum afterOrderType
    {
        leaveShop,
        sit1,
        sit2,
        sit3,
        //sit4,
        //sit5,
        //sit6,
        //sit7
    }
    [SerializeField] private float max_time = 20;
    private void Start()
    {
        fillAllAfterOrderTypes();
        StartCoroutine(randomPerson());
        max_time = 20;
    }




    IEnumerator randomPerson()
    {
        float randomTime = Random.Range(2, max_time);
        int randomP;
        
        randomP = Random.Range(0, customers.Count);
        addCustomer(randomP);
        
        yield return new WaitForSeconds(60f);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(randomPerson());
    }

    public void addCustomer(int randomPerson)
    {
        GameObject c= Instantiate(customers[randomPerson],  new Vector3(-3.4f, -1.33f, 11), Quaternion.Euler(0, 180, 0));
        currentCustomers.Add(c);
    }
    private void fillAllAfterOrderTypes()
    {
        afterOrderTypesLeft = System.Enum.GetValues(typeof(afterOrderType)).Cast<afterOrderType>().ToList();
    }

    public void orderCompleted()
    {

        int randomAfterOrder = Random.Range(0, afterOrderTypesLeft.Count());
        
        
        currentCustomers[0].GetComponent<Peson>().nextAction((int)afterOrderTypesLeft[randomAfterOrder]);
        afterOrderTypesLeft.Remove(afterOrderTypesLeft[randomAfterOrder]);
        if (afterOrderTypesLeft.Count() == 0)
        {
            afterOrderTypesLeft.Add(afterOrderType.leaveShop);
        }
        //currentCustomers[0].GetComponent<BoxCollider>().enabled = false;
        currentCustomers.Remove(currentCustomers[0]);
    }
    public void addAfterOrderTypes(int afterOrder)
    {
        afterOrderTypesLeft.Add((afterOrderType)afterOrder);
    }
}
