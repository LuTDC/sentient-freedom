using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Player player;

    private float offset = 6f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - offset, player.transform.position.y + 5, player.transform.position.z - offset);
    }
}
