using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used to control the Canvas drawing the hearts
public class UIController : MonoBehaviour
{
    // Used to reference this one and only instance 
    public static UIController instance;

    // Used to draw full hearts 
    public Image heart1, heart2, heart3;

    // Used to reference full, half and empty hearts
    public Sprite heartFull, heartHalf, heartEmpty;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to update the UI display
    public void UpdateHealthDisplay()
    {
        // Switch case depending on players current health
        // Update the image needed for that amount of health
        switch(PlayerHealthController.instance.currentHealth)
        {
            // All full hearts
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;

                break;

            // Two full hearts and a half
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;

                break;
            
            // Two full hearts and an empty one
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;

                break;

            // One Full heart, half a heart , and an empty heart
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;

                break;

            // One full heart, with two empty hearts
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            // One half a heart,  with two emptu hearts
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            // three empty hearts
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;
                
            // default is all empty hearts
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;
        }
    }
}
