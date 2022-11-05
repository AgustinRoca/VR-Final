using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Peson : MonoBehaviour
{
    public enum actionType
    {
        enteringShop1,
        enteringShop2,
        enteringShop3,
        enteringShop4,
        enteringShop5,
        enteringShop6,
        idle,
        leavingShop1,
        leavingShop2,
        leavingShop3,
        destroy,
        sitting1,
        sitting11,
        sitting12,
        sitting13,
        sitting14,
        sitting15,
        sitting2,
        sitting21,
        sitting22,
        sitting23,
        sitting24,
        sitting25,
        sitting26,
        sitting3,
        sitting31,
        sitting32,
        sitting33,
        sitting34,
        sitting35,
        sitting36,
        sitting37,
        sitting38,
        sitting4,
        sitting5,
        sitting6,
        sitting7,
    };
    public enum afterOrderType
    {
        leaveShop, 
        sit1,
        sit2,
        sit3,
       // sit4,
        //sit5,
        //sit6,
        //sit7
    }


    // Adjust the speed for the application.
    [SerializeField]private float speed;
    [SerializeField]private float speedR;
    [SerializeField] private Material[] materials;

    bool personInFront;

    public actionType action;
    Vector3 target;
    Quaternion rotateTarget;
    bool sitting;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(-3.4f, -1.33f, 11);
        action = actionType.enteringShop2;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        getTarget(action);
        personInFront = false;
        sitting = false;
        setRandomClothes();
    }
    
    private void setRandomClothes()
    {
        SkinnedMeshRenderer renderer;
        int random1, random2;
        Material[] mats;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Contains("Pants") || child.gameObject.name.Contains("Torso"))
            {
                renderer = child.GetComponent<SkinnedMeshRenderer>();
                mats = renderer.materials;
                random1 = Random.Range(0, materials.Length);
                // el color de la piel tiene que conicidir con el original
                if (!child.gameObject.name.Contains("Torso"))
                {
                    random2 = Random.Range(0, materials.Length);
                    mats[this.gameObject.name.Contains("Male")? 0:1] = materials[random2];
                }
                mats[this.gameObject.name.Contains("Male") ? 1 : 0] = materials[random1];
                renderer.materials = mats;

            }
        }
    }


    void Update()
    {
        
        if (((action != actionType.idle && !personInFront )|| action == actionType.leavingShop1 ) && !sitting)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTarget, speedR* Time.deltaTime);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                // Swap the position of the cylinder.
                action++;
                getTarget(action);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Female") || other.gameObject.name.Contains("Male"))
        {
            personInFront = true;
            gameObject.GetComponent<Animator>().SetInteger("state", 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Female") || other.gameObject.name.Contains("Male"))
        {
            personInFront = false;
            gameObject.GetComponent<Animator>().SetInteger("state", 0);
        }
    }

    public void nextAction(int randomAfterOrder)
    {

        switch ((afterOrderType)randomAfterOrder)
        {
            case afterOrderType.leaveShop:
                action = actionType.leavingShop1;
                break;
            case afterOrderType.sit1:
                action = actionType.sitting1;
                break;
            case afterOrderType.sit2:
                action = actionType.sitting2;
                break;
            case afterOrderType.sit3:
                action = actionType.sitting3;
                break;
            //case afterOrderType.sit4:
            //    action = actionType.sitting4;
            //    break;
            //case afterOrderType.sit5:
            //    action = actionType.sitting5;
            //    break;
            //case afterOrderType.sit6:
            //    action = actionType.sitting6;
            //    break;
            //case afterOrderType.sit7:
            //    action = actionType.sitting7;
            //    break;

        }
        getTarget(action);
        personInFront = false;
    }
   IEnumerator eat()
    {
        sitting = true;
        yield return new WaitForSeconds(60*2f);
        sitting = false;
    }

    void getTarget(actionType action)
    {
        switch (action)
        {
            case (actionType.enteringShop1):
                target =  new Vector3(-3.4f, -1.33f, 11);
                break;
            case (actionType.enteringShop2):
                target =  new Vector3(-3.4f, -1.33f, 0.24f);
                break;
            case (actionType.enteringShop3):
                target =  new Vector3(3.4f, -1.33f, 0.24f);
                rotateTarget=  Quaternion.Euler(0, 90 , 0);
                break; 
            case (actionType.enteringShop4):
                target =  new Vector3(3.4f, -1.33f, 4.35f);
                rotateTarget= Quaternion.Euler(0, 0 , 0);
                break;
            case (actionType.enteringShop5):
                target = new Vector3(0.4f, -1.33f, 4.35f);
                rotateTarget = Quaternion.Euler(0, -90, 0);
                break;
            case (actionType.enteringShop6):
                target = new Vector3(0.4f, -1.33f, 8f);
                rotateTarget =Quaternion.Euler(0, 0, 0);
                break;
            case (actionType.idle):// waiting for order
                gameObject.GetComponent<Animator>().SetInteger("state", 1);
                //add new order to list of orders
                GameObject.Find("orderTray").GetComponent<TrayManager>().addOrder();
                break;
            case (actionType.leavingShop1):
                gameObject.GetComponent<Animator>().SetInteger("state", 0);
                target = new Vector3(3f, -1.33f, 8f);
                rotateTarget = Quaternion.Euler(0, 90, 0);
                break;
            case (actionType.leavingShop2):
                target = new Vector3(3f, -1.33f, 0.5f);
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                break;
            case (actionType.leavingShop3):
                target = new Vector3(50f, -1.33f, 0.5f);
                rotateTarget = Quaternion.Euler(0, 90, 0);
                break;
            case (actionType.destroy):
                Destroy(gameObject);
                break;
            case (actionType.sitting1):
                gameObject.GetComponent<Animator>().SetInteger("state", 0); //camina
                target = new Vector3(3.9f, -1.33f, 7.77f);
                rotateTarget = Quaternion.Euler(0, 90, 0);
                break;
            case (actionType.sitting11):
                target = new Vector3(3.9f, -1.33f, 7.14f);
                rotateTarget = Quaternion.Euler(0, 90, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                gameObject.GetComponent<BoxCollider>().enabled = false;
                break;
            case (actionType.sitting12):

                target = new Vector3(3.48f, -1.53f, 7.14f);
                rotateTarget = Quaternion.Euler(7.927f, 90, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 2); //sit
                break;
            case (actionType.sitting13):
                StartCoroutine(eat());
                break;
            case (actionType.sitting14):// stand up
                target = new Vector3(3.9f, -1.33f, 7.14f);
                rotateTarget = Quaternion.Euler(0, 90, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                break;
            case (actionType.sitting15):
                target = new Vector3(3.9f, -1.33f, 0f);
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 0); //standup and walk? quizas?
                this.action = actionType.leavingShop2; // hace que haga el mismo recorrido para destruirme
                break;
            case (actionType.sitting2):

                gameObject.GetComponent<Animator>().SetInteger("state", 0); //camina
                GameObject.Find("people").GetComponent<CustomerManager>().addAfterOrderTypes((int)afterOrderType.sit1);
                target = new Vector3(3.8f, -1.33f, 7.76f);
                rotateTarget = Quaternion.Euler(0, 90, 0);// se pone al lado de la silla
                break;
            case (actionType.sitting21):
                target = new Vector3(4.413f, -1.33f, 7.72f);//se pone en frente de la silla
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                gameObject.GetComponent<BoxCollider>().enabled = false;
                break;
            case (actionType.sitting22)://se sienta a comer
                target = new Vector3(4.413f, -1.53f, 8.12f);
                rotateTarget = Quaternion.Euler(12.39f, 180, -4.77f);
                gameObject.GetComponent<Animator>().SetInteger("state", 2); //sit
                break;
            case (actionType.sitting23)://se sienta a comer
                StartCoroutine(eat());
                break;
            case (actionType.sitting24):// stand up
                target = new Vector3(4.413f, -1.33f, 7.72f);//se pone en frente de la silla
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                break;
            case (actionType.sitting25):
                target = new Vector3(2.95f, -1.33f, 7.72f);
                rotateTarget = Quaternion.Euler(0, -90, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 0); //camina
                break;
            case (actionType.sitting26)://sale as afuera
                GameObject.Find("people").GetComponent<CustomerManager>().addAfterOrderTypes((int)afterOrderType.sit2);
                target = new Vector3(4.15f, -1.33f, 0.11f);
                rotateTarget = Quaternion.Euler(0, 180, 0);
                this.action = actionType.leavingShop2; // hace que haga el mismo recorrido para destruirme
                break;
            case (actionType.sitting3):
                gameObject.GetComponent<Animator>().SetInteger("state", 0); //camina
                target = new Vector3(2.72f, -1.33f, 7.91f);
                rotateTarget = Quaternion.Euler(0, 90, 0);// se poneen el pasillo
                break;
            case (actionType.sitting31):
                target = new Vector3(2.72f, -1.33f, 5.04f);//se pone en el pasillo a la altura de la silla
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                break;
            case (actionType.sitting32):
                target = new Vector3(4.06f, -1.33f, 5.04f);//se pone al lado de la silla
                rotateTarget = Quaternion.Euler(0, 90, 0);
                break;
            case (actionType.sitting33):
                target = new Vector3(5.19f, -1.33f, 5.04f);//se pone en frente de la silla
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                break;
            case (actionType.sitting34)://se sienta a comer]\'
                target = new Vector3(5.19f, -1.62f, 5.29f);//se pone en frente de la silla
                rotateTarget = Quaternion.Euler(4.795f, 180, -0.53f);
                gameObject.GetComponent<Animator>().SetInteger("state", 2); //sit
                break;
            case (actionType.sitting35)://se sienta a comer
                StartCoroutine(eat());
                break;
            case (actionType.sitting36):
                target = new Vector3(5.19f, -1.33f, 5.04f);//se pone en frente de la silla
                rotateTarget = Quaternion.Euler(0, 180, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 1); //idle
                break;
            case (actionType.sitting37):
                target = new Vector3(3.9f, -1.33f, 5.04f);
                rotateTarget = Quaternion.Euler(0, -90, 0);
                gameObject.GetComponent<Animator>().SetInteger("state", 0); //camina
                break;
            case (actionType.sitting38)://sale as afuera
                GameObject.Find("people").GetComponent<CustomerManager>().addAfterOrderTypes((int) afterOrderType.sit3);
                target = new Vector3(4.15f, -1.33f, 0.11f);
                rotateTarget = Quaternion.Euler(0, 180, 0);
                this.action = actionType.leavingShop2; // hace que haga el mismo recorrido para destruirme
                break;





        }

    }

}
