using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    public float Speed = 5f;
    public Transform A;
    public Transform B;
    private Vector3 Finish;
    // Start is called before the first frame update
    void Start()
    {
        Finish = A.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Finish, Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position,Finish)<0.1f)
        {
            if(transform.position == A.position)
            {
                Finish = B.position;
            }
            else
            {
                Finish = A.position;

            }
        }
    }
}
