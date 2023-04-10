using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 NowPosition;
    private Vector3 CursorPosition;
    public RotateToCursor RotateToCursor;
    public CursorPointer CursorPointer;
    private PlayerMovement movement;
    public float RocketSpeed;
    private float Slope;
    public bool Fire;
    private float ActiveTime = 0;
    public float MaxActiveTime;
    public float reloadTime = 0.5f;
    public float MaxReloadTime = 0.5f;
    public Rigidbody2D rb;
    private float SlopeY;
    public Animator animator;
    public Animator animatorSelf;
    public PlayerController PlayerController;
    public Shot Shot;
    public CircleCollider2D collider;
    public SpriteRenderer sprite;
    
    void Awake() {
        movement = new PlayerMovement();
    }
    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        if (movement != null) movement.Disable();
    }
    void Start()
    {
        movement.ControllerGround.RocketLauncher.performed += OnLaunch;

    }
    void StopRocket () {
        Shot.rb.velocity = Vector3.zero;
        reloadTime = 0f;
        sprite.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(animatorSelf.GetCurrentAnimatorStateInfo(0).IsName("Explosion"));
        if ((animatorSelf.GetCurrentAnimatorStateInfo(0).IsName("Explosion") == true)) {
            animatorSelf.SetBool("ExplosionActivate", false);
            animator.SetBool("Exploding", false);
            // animatorSelf.ResetTrigger("ExplosionActivate");
        }
        if ((Shot.rb.velocity == Vector2.zero) && (!(animatorSelf.GetCurrentAnimatorStateInfo(0).IsName("Explosion")))) {
            Debug.Log(string.Format("YEAAHHH BABY", animatorSelf.GetCurrentAnimatorStateInfo(0).IsName("Explosion")));
            sprite.enabled = false;
            collider.enabled = false;
        }
        Shot.transform.rotation = RotateToCursor.transform.rotation;
        transform.position = Shot.transform.position;
        if ((reloadTime <= MaxReloadTime)) {
            reloadTime += Time.deltaTime;
        }
        if ((ActiveTime >= MaxActiveTime)) {
            Fire = false;
            ActiveTime = 0f;
            StopRocket();
            // transform.position = PlayerController.transform.position;
        }
        if (Fire) {
            // Debug.Log("Let's be honest");
            ActiveTime += Time.deltaTime;
        }
        
    }
    void OnLaunch (InputAction.CallbackContext context) {
        if ((reloadTime > MaxReloadTime) && (animatorSelf.GetCurrentAnimatorStateInfo(0).IsName("Explosion") == false)) {
            sprite.enabled = true;
            collider.enabled = true;
            reloadTime = 0f;
            ActiveTime = 0f;
            // Debug.Log("s");
            rb.velocity = Vector3.zero;
            Shot.transform.position = PlayerController.transform.position;
            
            // Vector3 rotation = RotateToCursor.transform.rotation.eulerAngles;
            // transform.eulerAngles = rotation;
            
            Shot.rb.AddRelativeForce(Vector2.right*RocketSpeed, ForceMode2D.Impulse);
            // rb.AddForce(RotateToCursor.transform.forward * RocketSpeed, ForceMode2D.Force);
            Fire = true;
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D col) {
        if ((col.gameObject.tag != "Player")) {
            sprite.enabled = true;
            animator.SetBool("Exploding",true);
            Debug.Log("NOW! DO ITTTTTT!!!!!!!!!!!!!!!!");
            // animatorSelf.SetBool("ExplosionActivate",true);
            
            animatorSelf.SetTrigger("ExplosionActivate");
            
            ActiveTime = MaxActiveTime;
            StopRocket();
            // Activate Rocket Explosion Hitbox!
            
        } 
    }
}
