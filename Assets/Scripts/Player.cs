using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 1;
    public int health = 100;

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float fallSpeed = -1.0f;

    private CharacterController characterController;

    private float xMove;
    private float yMove;
    private float zMove;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Movement
        xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Movement
        characterController.Move(new Vector3(xMove, yMove, zMove));

        // Gravity
        characterController.Move(new Vector3(0.0f, fallSpeed * Time.deltaTime, 0.0f));
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 120), "Player Health");

        if(GUI.Button(new Rect(20, 40, 80, 20), "+"))
        {
            health += 10;
        }

        GUI.Label(new Rect(20, 70, 80, 20), "Health: " + health.ToString());

        if (GUI.Button(new Rect(20, 100, 80, 20), "-"))
        {
            health -= 10;
        }

        GUI.Box(new Rect(10, 140, 100, 90), "Save Settings");

        if(GUI.Button(new Rect(20, 170, 80, 20), "Save Player"))
        {
            SaveSystem.SavePlayer(this);
        }

        if(GUI.Button(new Rect(20, 200, 80, 20), "Load Player"))
        {
            PlayerData data = SaveSystem.LoadPlayer();

            level = data.level;
            health = data.health;

            characterController.enabled = false;
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;
            characterController.enabled = true;
        }
    }

}
