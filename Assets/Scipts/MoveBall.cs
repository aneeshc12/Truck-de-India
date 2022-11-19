using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Ray ray;
    RaycastHit hitData;
    Rigidbody rb;
    public float thrust = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) GetMoveCommand();
    }

    void GetMoveCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);

        Vector3 forceDir = hitData.point - transform.position;
        forceDir.y = 0;
        forceDir.Normalize();

        rb.AddForce(forceDir * thrust);

        Debug.Log(forceDir);
    }
}
