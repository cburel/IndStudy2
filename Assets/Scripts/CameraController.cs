using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;  // for linking player to camera

    // Update is called once per frame
    private void Update()
    {
        // follow player's x, y position
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
