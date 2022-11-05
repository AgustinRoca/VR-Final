using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoListManager : MonoBehaviour
{
    public GameObject[] changeFood(string food1, string food2){
        GameObject food1GO = Instantiate(Resources.Load("Prefabs/showcase/food/" + food1), new Vector3 (0,0,0), Quaternion.identity) as GameObject;
        food1GO.GetComponent<BoxCollider>().enabled = false;
        Rigidbody rigidbody = food1GO.transform.GetComponent<Rigidbody>();
        Destroy(rigidbody);
        food1GO.transform.parent = GameObject.Find("todo-list generator").transform;
        food1GO.transform.localPosition = new Vector3(6.2f,-0.7f,-7.2f);
        food1GO.transform.localPosition += getLocalPositionOffset(food1, food1GO);
        food1GO.transform.localScale *= 5f/3f;
        GameObject food2GO = Instantiate(Resources.Load("Prefabs/showcase/food/" + food2), new Vector3 (0,0,0), Quaternion.identity) as GameObject;
        food2GO.GetComponent<BoxCollider>().enabled = false;
        rigidbody = food2GO.transform.GetComponent<Rigidbody>();
        Destroy(rigidbody);
        food2GO.transform.parent = GameObject.Find("todo-list generator").transform;
        food2GO.transform.localPosition = new Vector3(6.2f,-0.7f,-8.1f);
        food2GO.transform.localPosition += getLocalPositionOffset(food2, food2GO);
        food2GO.transform.localScale *= 5f / 3f;

        print("hi im here");
        return new GameObject[2] {food1GO, food2GO};

    }

    private Vector3 getLocalPositionOffset(string foodName, GameObject obj){
        switch(foodName){
            case "blue donut":
                return new Vector3(-0.5f,-0.2f,-0.6f);
            case "light pink donut":
                return new Vector3(0.5f,-0.2f,-0.4f);
            case "pink donut":
                return new Vector3(0.5f,-0.2f,0.6f);
            case "yellow donut":
                return new Vector3(-0.5f,-0.2f,0.9f);
            case "black cupcake":
                return new Vector3(-0.2f,-2f,0.3f);
            case "blue cupcake":
                return new Vector3(-0.2f,-2.4f,0.3f);
            case "cherry cupcake":
                return new Vector3(-0.6f,-2f,0.4f);
            case "ddl cupcake":
                return new Vector3(-0.2f,-2f,0.3f);
            case "oreo cupcake":
                return new Vector3(-0.8f,-2f,0.3f);
            case "chocolateCake":
                obj.transform.eulerAngles = new Vector3(
                    obj.transform.eulerAngles.x-90,
                    obj.transform.eulerAngles.y+90,
                    obj.transform.eulerAngles.z
                );
                //obj.transform.rotation = new Quaternion(-90,0,0, 0);
                return new Vector3(0,0,0);

            default:
                return new Vector3(0,0,0);
            
        }
    }
}
