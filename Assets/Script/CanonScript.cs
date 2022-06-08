using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanonScript : MonoBehaviour
{
  
    public GameObject cannonball;
    [SerializeField] float cannonballSpeed=1f;
    public Transform pof;
    public Transform barrel;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Texture2D crossHair;
    private Slider slider;
    [SerializeField] Image slider1Fill;
    private int ballQuantity = 10;
    private bool isEndGame = false;
    private TMPro.TextMeshProUGUI ballCount;
    void Start()
    {
        ballCount = GameObject.Find("BallCount").GetComponent<TMPro.TextMeshProUGUI>();
        Vector2 hotSpot = new Vector2(crossHair.width/2, crossHair.height/2);
        Cursor.SetCursor(crossHair, hotSpot, CursorMode.ForceSoftware);
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        SetValueSlider();


    }
    private void SetValueSlider()
    {
        slider.maxValue = 50;
        slider.minValue = 0;
        slider.value = 0;
    }
    private void ResetBallValue()
    {
        cannonballSpeed = 0;
    }
    private void ResetSliderValue()
    {
        slider.value = 0;
        slider1Fill.color = Color.white;
    }
    void Update()
    {


        if(isEndGame == false)
        {
            PlayGame();
        }
       

        
        if (ballQuantity == 0)
        {
            RetartGame();
        }


    }
    private void PlayGame()
    {
        worldPosition = GetWorldPosition();
        barrel.LookAt(worldPosition);
        if (Input.GetButton("Fire1"))
        {
            cannonballSpeed += 10 * Time.deltaTime;
            slider.value = cannonballSpeed;
            slider1Fill.color = Color.Lerp(Color.green, Color.red, slider.value / 50);
            Debug.Log(cannonballSpeed);
            if (cannonballSpeed >= 50)
            {
                cannonballSpeed = 50f;
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            FireCannonball(cannonballSpeed);
            ResetBallValue();
            ResetSliderValue();
            ballQuantity--;
        }
        ballCount.text = "Ball Left: " + ballQuantity;
    }
    private void RetartGame()
    {
        isEndGame = true;
        ballCount.text = "Press R to replay";
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentScene();
        }
    }
  private Vector3 GetWorldPosition()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            return hitData.point;
        }
        return hitData.point; 
    }
    private void FireCannonball(float cannonballSpeed)
    {
        GameObject ball = Instantiate(cannonball, pof.position, Quaternion.identity);
        Rigidbody rb = ball.AddComponent<Rigidbody>();
        rb.velocity = cannonballSpeed * pof.forward;
        StartCoroutine(RemoveCannonball(ball));
    }
   
    IEnumerator RemoveCannonball(GameObject ball)
    {
        yield return new WaitForSeconds(4f); 
        Destroy(ball);
    }
    public void RestartCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        RoundScript.score = 0;
    }
}
