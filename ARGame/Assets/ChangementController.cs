using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Image1_Personnages
 * Image2_Boardgame
 * Image3_Croissant
 * Image3_Paris
 * Image4_Drakar
 * Image4_NorthernLight
 * Image5_ChampTulipe
 * Image6_Beer
 * Image7_China
 * 
 * */
public class ChangementController : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    public Text txt1;
    public Text txt2;
    public Sprite Image1_Personnages;
    public Sprite Image2_Boardgame;
    public Sprite Image3_Croissant;
    public Sprite Image3_Paris;
    public Sprite Image4_Drakar;
    public Sprite Image4_NorthernLight;
    public Sprite Image5_ChampTulipe;
    public Sprite Image6_Beer;
    public Sprite Image7_China; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MyMethod());
          
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MyMethod()
    {
        // Parameter of the text ! 3
        txt1.color = Color.white;
        txt2.color = Color.white;
        //txt1.fontSize = 18;
        // Introduction of the game 


        // First presentation of the character 
        Image1.GetComponent<SpriteRenderer>().sprite = Image1_Personnages;
        txt1.text = "It’s summer holiday for the grandson. Grandma is taking her grandson on a world trip. ";
        txt2.text = "";
        yield return new WaitForSeconds(2);

        // Second presentation of the board game 
        Image1.GetComponent<SpriteRenderer>().sprite = Image2_Boardgame;
        txt1.text = "The path of this adventure is draw, follow it to discover the next...";
        txt2.text = "";
        yield return new WaitForSeconds(2);

        // Third : Presentation France
        Image1.GetComponent<SpriteRenderer>().sprite = Image3_Paris;
        
        txt1.text = "The journey starts from Paris in France.";
        txt2.text = "They will visit Eiffel tower and eat croissant there.";
        yield return new WaitForSeconds(2);
        Image1.GetComponent<SpriteRenderer>().sprite = Image3_Croissant;
        yield return new WaitForSeconds(2);

        // Fourth : Presentation Norway
        Image1.GetComponent<SpriteRenderer>().sprite = Image4_Drakar;       
        txt1.text = "After Paris, both will head to Norway and check on the Vikings heritage. ";
        txt2.text = "They will also enjoy the northern light during the stay in Norway.";
        yield return new WaitForSeconds(2); 
        Image1.GetComponent<SpriteRenderer>().sprite = Image4_NorthernLight;
        yield return new WaitForSeconds(2);
        // Fifth : Presentation Netherlands
        Image1.GetComponent<SpriteRenderer>().sprite = Image5_ChampTulipe;
        //Image2.SetActive(false);
        txt1.text = "Fragrance of the tulips welcomes all travellers including grandma and grandson in Netherland. ";
        txt2.text = "";
        yield return new WaitForSeconds(2);

        // Six : Presentation Germany
        Image1.GetComponent<SpriteRenderer>().sprite = Image6_Beer;
        txt1.text = "Last stop in Europe will be the industrial house of Europe, Germany, both are enjoying... ";
        txt2.text = "Grandma takes also a bit German fine beer, but for grandson it’s too early now.";
        yield return new WaitForSeconds(2);

        // Seven : Presentation China
        Image1.GetComponent<SpriteRenderer>().sprite = Image7_China;
        txt1.text = "Flying in the sky from Europe to China, the Great Wall went into their eyes.";
        txt2.text = "Landed on the land, a group of different style of architectures immersed the two visitors from far.";
        yield return new WaitForSeconds(2);

        // Eight : Let Enjoys


    }
}
