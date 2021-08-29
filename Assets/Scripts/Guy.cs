using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

public class Guy : MonoBehaviour{
    [SerializeField] Transform gameOverPoint;
    [SerializeField] Boolean sendMessages;

    private float speed = 0.1f;
    private Rigidbody2D guy1rb;
    private Rigidbody2D guy2rb;
    private Guy guy1;
    private Guy guy2;
    private float cameraLimitX = 6.7f;
    private int active_player = 1;
    Boolean primeiroEntrou = false;
    public Animator animator;
    private Boolean estaPuxando = false;
    private Boolean noAr = false;
    private Boolean juntos = true;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
    }

    void Start()
    {   
        animator.SetBool("ApuxandoV",true);
        guy1rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        guy2rb = GameObject.FindGameObjectWithTag("Player2").GetComponent<Rigidbody2D>();
        guy1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Guy>();
        guy2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Guy>();
    }

    void OnMessage(int fromDeviceID, JToken data)
    {
        Debug.Log("message from" + fromDeviceID + ", data: " + data);
        active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber (fromDeviceID);
        Debug.Log("Active player " + active_player + " sent a message");
        if (data["force"] != null && data["x"] != null && data["y"] != null)
        {
            float force = float.Parse(data["force"].ToString());
            float x = float.Parse(data["x"].ToString());
            float y = float.Parse(data["y"].ToString());
            Pular(x, y, force*0.1F);
            if(sendMessages){
                AirConsole.instance.Broadcast("MUDAR");
            }
            Debug.Log("MUDAR");
        }
    }

    void Update()
    {
        rellocatingPlayer();

        if(playerBelowGameOverPoint())
        {
            cameraStopsFollowingPlayer();   
        }
        if(guy1rb.velocity == new Vector2(0.0f, 0.0f) && active_player == 0 ){
            guy2.Puxar();
           animator.SetBool("gelotogente",true);
        }
        if(guy2rb.velocity == new Vector2(0.0f, 0.0f) && active_player == 1 ){
            guy1.Puxar();
            estaPuxando = true;
        }

        if((guy1.transform.position == guy2.transform.position)/* &&(active_player== 0)*/){
          
         juntos=true;
         Debug.Log("estamos juntinhos");

        }

        animator.SetBool("estapuxando",estaPuxando);
        animator.SetBool("juntos",juntos);
    }

    private bool playerBelowGameOverPoint()
    {
        return this.transform.position.y < this.gameOverPoint.position.y;
    }

    private void cameraStopsFollowingPlayer()
    {
        var camera = FindObjectOfType<MainCamera>();
        camera.triggerGameOver();  
    }


    private void yellowPullAnimation(){
       animator.SetBool("ApuxandoV",true);

    }

    void OnConnect(int fromDeviceID)
    { //quando alguem se conecta, e n eh o primeiro, ele entra em estado de espera
        Debug.Log(fromDeviceID +" controller is connected");
        Debug.Log(AirConsole.instance.GetControllerDeviceIds ().Count + " players connected");
        if(AirConsole.instance.GetControllerDeviceIds ().Count >= 2){
            AirConsole.instance.SetActivePlayers (2);
        }
        if (primeiroEntrou)
        {
            if(sendMessages){
                AirConsole.instance.Message(fromDeviceID,"MUDAR");
            }
        } else
        {
            primeiroEntrou = true;
        }
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onConnect -= OnConnect;
        }
    }

    /// <summary>
	/// If the game is running and one of the active players leaves, we reset the game.
	/// </summary>
	/// <param name="device_id">The device_id that has left.</param>
	void OnDisconnect (int device_id) {
		int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber (device_id);
		if (active_player != -1) {
			if (AirConsole.instance.GetControllerDeviceIds ().Count >= 2) {
				AirConsole.instance.SetActivePlayers (2);
			} else {
				AirConsole.instance.SetActivePlayers (0);
			}
		}
	}

    private void Pular(float x, float y, float force)
    {
        Vector2 direcao = new Vector2(x * force*-1, y*force);
        animator.SetBool("noar",true);
       
        if(active_player == 0){
            guy1rb.AddForce(direcao, ForceMode2D.Impulse);
        } else if (active_player == 1){
            guy2rb.AddForce(direcao, ForceMode2D.Impulse);
        }
    }

    private void rellocatingPlayer()
    {
        Vector2 myPosition = guy1rb.transform.position;
        if(active_player == 1){
            myPosition = guy2rb.transform.position;
        }
        if (myPosition.x < -cameraLimitX)
        {
            this.transform.position = new Vector2(cameraLimitX, myPosition.y);
        }

        if (myPosition.x > cameraLimitX)
        {
            this.transform.position = new Vector2(-cameraLimitX, myPosition.y);
        }
    }

    public void Puxar(){
        Debug.Log("PUXADA ATOMICA");
        if(active_player == 0 ){
            Debug.Log("player que esta sendo puxado é o 2");
            guy2rb.transform.position = Vector2.MoveTowards(guy2rb.transform.position, guy1rb.transform.position, speed);
        }
        else if (active_player == 1){
            Debug.Log("player que esta sendo puxado é o 1");
            guy1rb.transform.position = Vector2.MoveTowards(guy1rb.transform.position, guy2rb.transform.position, speed);
        }
    }

     private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Debug.Log("destruir");
        }
    }
    
}