using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    public Rocket RocketPos;
    public PlayerController PlayerController;
    public float LaunchSpeed;
    private float NoLaunchTime = 0f;
    public float MaxNoLaunchTime = 0.5f;
    private bool CanLaunch = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(NoLaunchTime);
        if ((NoLaunchTime > MaxNoLaunchTime)) {
            CanLaunch = true;
        }
        else {
            NoLaunchTime+=Time.deltaTime;
        }  
    }
    void OnTriggerEnter2D(Collider2D col) {
        if ((col.gameObject.tag == "Rocket")) {
            // Debug.Log("ss");
            // Debug.Log(PlayerController.transform.position.y-RocketPos.transform.position.y);
            // RocketPos.collider.enabled = false;
            if (CanLaunch) {
                NoLaunchTime = 0f;
                PlayerController.rb.AddForce(Vector2.up * Mathf.Abs(0.7f-(PlayerController.transform.position.y-RocketPos.transform.position.y))*LaunchSpeed, ForceMode2D.Impulse);
                PlayerController.rb.AddForce(Vector2.right * (Mathf.Abs(0.7f-(PlayerController.transform.position.x-RocketPos.transform.position.x)))*LaunchSpeed, ForceMode2D.Impulse);
            }
            
        }
        
    }
}
