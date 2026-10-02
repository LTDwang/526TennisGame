using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileResetTimer : MonoBehaviour
{
    [SerializeField]
    GameObject timerOne;
    [SerializeField]
    GameObject timerTwo;
    [SerializeField]
    TiltBubblePowerUp powerUp;
    [SerializeField]
    Image imageOne;
    [SerializeField]
    Image imageTwo;

    static TileResetTimer instance;
    static public TileResetTimer Instance {  get { return instance; } }

    float totalTileTime;
    float timeLeftOne;
    float timeLeftTwo;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        totalTileTime = powerUp.TiltHoldDuration;        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftOne -= Time.deltaTime;
        timeLeftTwo -= Time.deltaTime;
        imageOne.fillAmount = timeLeftOne / totalTileTime;
        imageTwo.fillAmount = timeLeftTwo / totalTileTime;
        if(timeLeftOne <= 0.0001f )
            timerOne.SetActive(false);
        if(timeLeftTwo <= 0.0001f )
            timerTwo.SetActive(false);
    }

    public void SetTimerOne(float  time)
    {
        timerOne.SetActive (true);
        timeLeftOne = time;
    }
    public void SetTimerTwo(float time)
    {
        timerTwo.SetActive(true);
        timeLeftTwo = time;
    }
}
