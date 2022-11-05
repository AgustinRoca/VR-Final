using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    [SerializeField] private GameObject handReticle;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject markCenter;
    [SerializeField] private int maxCastDistance = 100;
    [SerializeField] private float delta = 1f;
    [SerializeField] private float setDiffY = 1f;
    private Camera camera;
    private GameObject objectSelected;
    private bool isHolding;
    [SerializeField] private Rigidbody rb;
    private int layerMask;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        isHolding = false;
        hand.SetActive(false);
        layerMask = (1 << 3);

        markCenter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool isPointing;
        bool clicked = Input.GetButtonDown("Fire1") || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began);
        isPointing = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxCastDistance, layerMask);
        if (isHolding)
        {
            
            Holding(hit, isPointing, clicked);
        }
        else
        {

            NotHolding(hit, isPointing, clicked);
        }
       
    }
    void Holding(RaycastHit hit, bool isPointing, bool clicked)
    {
        if (isPointing)
        {
            // poner el objeto donde se podria posar
            objectSelected.transform.position = new Vector3(hit.point.x, hit.point.y + setDiffY, hit.point.z);
            // endereso el objeto para que quede parado sobre la superficie
            Vector3 eulerRotation = objectSelected.transform.rotation.eulerAngles;
            objectSelected.transform.rotation = initialRotation;

            markCenter.SetActive(false);
        }
        else
        {
            // poner el objeto en mi mano
            objectSelected.transform.position = hand.transform.position;
            objectSelected.transform.position += camera.transform.up * 0.1f;
            objectSelected.transform.position += camera.transform.forward * 0.1f;
            objectSelected.transform.position -= 0.2f * camera.transform.right;
            // Para que no parezca que rota sobre mi mano
            objectSelected.transform.rotation = camera.transform.rotation;

            // marco donde esta el centro
            markCenter.SetActive(true);
        }
        if (clicked)
        {
            // soltar el objeto
            EnableRagdoll();
            isHolding = false;
            //desactivo la mano que sostiene un objeto
            hand.SetActive(false);
            // paso a buscar objetos que son seleccionables
            layerMask = (1 << 3);
        }
    }

    void NotHolding(RaycastHit hit, bool isPointing, bool clicked)
    {
        if (isPointing)
        {

            markCenter.SetActive(false);

            if (clicked)
            {
                print("hit" + hit.collider.gameObject.name);
                // obtengo el objeto al cual estoy mirando
                print(hit.collider.gameObject.name);
                if(hit.collider.gameObject.tag == "onTray")
                {
                    objectSelected = GameObject.Instantiate(hit.collider.gameObject);
                    objectSelected.tag = "Untagged";
                    changeSize(objectSelected);
                    StartCoroutine(disappear(hit.collider.gameObject));
                }
                else
                {
                    objectSelected = hit.collider.gameObject;
                }

                print("object selectes" + objectSelected.name);

                initialRotation = objectSelected.transform.rotation;

                if (objectSelected.name.Contains("pCylinder128") || objectSelected.name.Contains("pCylinder121")) // si es el boton de la cafetera, sirvo cafe
                {
                    objectSelected.GetComponent<fillMug>().fill();
                    if(objectSelected.name.Contains("pCylinder121")){
                        objectSelected.transform.parent.gameObject.transform.parent.gameObject.GetComponent<LeverManager>().pullLever(1);
                    } else {
                        objectSelected.transform.parent.gameObject.transform.parent.gameObject.GetComponent<LeverManager>().pullLever(2);
                    }
                    return;
                } 
                else if(objectSelected.name.Contains("Cylinder")) // si es la radiola prendo o apago
                {
                    objectSelected.GetComponent<RadioController>().switchOnOff();
                    return;
                }
                Transform cameratransform = camera.transform;
                // agarro el objeto
                objectSelected.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
                isHolding = true;
                // activo la mano que no es reticle
                hand.SetActive(true);
                // desactivo la mano que es reticle
                handReticle.SetActive(false);
                // paso a buscar objetos en el cual puedo posar mi objeto
                layerMask = (1 << 6);
                //le saco la fisica
                DisableRagdoll();
                if (objectSelected.name.Contains("mug"))
                {
                    setDiffY = 0.05f; 
                }
                else if (objectSelected.name.Contains("cupcake"))
                {
                    setDiffY = -0.16f;
                }
                else if (objectSelected.name.Contains("chocolateCake"))
                {
                    setDiffY = 0.02f;
                }
                else if (objectSelected.name.Contains("plate"))
                {
                    objectSelected.GetComponent<Collider>().isTrigger = true;
                    setDiffY = 0.1f;
                }
                else
                {
                    setDiffY = 0f;
                }
            }
            else
            {
                // muevo mi mano reticle a lo que podria agarrar
                handReticle.transform.position = hit.point;
                handReticle.SetActive(true);
            }

        }
        else
        {
            // como no hay nada que agarrar desactivo mi mano
            handReticle.SetActive(false);
            
            // marco donde esta el centro
            markCenter.SetActive(true);
        }
       
    }

    void EnableRagdoll()
    {
        rb = objectSelected.GetComponent<Rigidbody>();
        // rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.useGravity = true;
    }
    void DisableRagdoll()
    {
        rb = objectSelected.GetComponent<Rigidbody>();
        // rb.isKinematic = true;
        rb.detectCollisions = false;
        rb.useGravity = false;

    }

    private void changeSize(GameObject obj)
    {
        if (obj.name.Contains("cupcake"))
            objectSelected.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        else if (obj.name.Contains("cupcake"))
            objectSelected.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else if(obj.name.Contains("chocolateCake"))
            objectSelected.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        else if(obj.name.Contains("donut"))
            objectSelected.transform.localScale = new Vector3(5f, 5f, 5f);
        else if(obj.name.Contains("plate"))
            objectSelected.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private IEnumerator disappear(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(60f);
        obj.SetActive(true);
    }
}
