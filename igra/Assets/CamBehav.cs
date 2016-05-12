using UnityEngine;
using System.Collections;

public class CamBehav : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 4.0f;

    // Use this for initialization
    void Start () {
        /* GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
         cube.AddComponent<Rigidbody>();
         cube.transform.position = new Vector3(4.5f, 0.45f, 12.65f);*/

    }	

    void Update()
    {
        Vector3 pos = player.transform.position;
        Vector3 rot = player.transform.rotation.eulerAngles;

        pos.y = cameraHeight;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(new Vector3(65, rot.y, 0));


        /* Ovo je u biti tree placer, snima producirani kod u naznaceni fajl koji se onda stavi u TreePlacer.cs Start() */
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Pozicija: " + hit.point);
                Vector3 p = hit.point;
                p.y += 0.01f;
                

                char letter = (char)Random.Range('a', 'p');
                GameObject cube = (GameObject)Instantiate(Resources.Load("Low_poly_styled_trees/Assets/Prefabs/Trees/tree_" + letter));
                cube.transform.position = p;
                cube.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                
                string toWrite = "tree = (GameObject)Instantiate(Resources.Load(\"Low_poly_styled_trees/Assets/Prefabs/Trees/tree_" + letter + "\"));";
                toWrite += "\r\n";
                toWrite += "tree.transform.position = new Vector3(" + p.x + "f," + p.y + "f," + p.z + "f);";
                toWrite += "\r\n";
                toWrite += "tree.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);\r\n";

                System.IO.File.AppendAllText("C:\\trees.txt", toWrite);

            }
        }

    }
}
