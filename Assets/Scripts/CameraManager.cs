using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = player.transform.position.y;
        if (player.transform.position.y < 0.08f)
        {
            y = 0.08f;
        }
        transform.position = new Vector3(player.transform.position.x, y, -10);
    }
}
