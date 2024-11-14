using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovelGame : MonoBehaviour
{
    private string playerName = "Mira"; // Heroine's name
    public int playerHealth = 100;
    private Vector3 playerPosition = Vector3.zero; // Vector variable to simulate position change
    private List<string> allies = new List<string> { "Arin the Mage", "Lyra the Archer" };
    private List<string> enemies = new List<string> { "Dark Knight", "Rogue Demon" };
    
    public Text dialogueText; // UI text to display dialogue
    public Button option1Button; // UI button for option 1
    public Button option2Button; // UI button for option 2

    private int storyStage = 0; // Tracks the current stage of the story
    private bool inBattle = false; // Flag to track if a battle is occurring

    void Awake()
    {
        Debug.Log("Awake: Preparing Mira's journey...");
        Debug.Log($"{playerName} feels an ancient power awakening within her.");
        
        option1Button.onClick.AddListener(ChooseOption1);
        option2Button.onClick.AddListener(ChooseOption2);
    }

    void Start()
    {
        ShowDialogue("Welcome, " + playerName + ". You feel an ancient power stirring within.");
        ProgressStory();
    }

    void Update()
    {
        if (inBattle)
        {
            playerPosition += Vector3.up * Time.deltaTime; // Simulates power buildup
        }
    }

    void FixedUpdate()
    {
        if (inBattle)
        {
            playerPosition += Physics.gravity * Time.deltaTime; // Gravity effect during battle
        }
    }

    void ShowDialogue(string message)
    {
        dialogueText.text = message;
    }

    void ProgressStory()
    {
        switch (storyStage)
        {
            case 0:
                ShowDialogue("You enter a dark forest. Shadows seem to follow your every step.");
                SetChoices("Keep walking", "Look around cautiously");
                break;
            case 1:
                EncounterCharacter();
                break;
            case 2:
                Battle("Dark Knight");
                break;
            case 3:
                ShowDialogue("You have survived the encounter and continue on your journey.");
                EndGame();
                break;
            default:
                ShowDialogue("The journey has ended.");
                break;
        }
    }

    void SetChoices(string option1Text, string option2Text)
    {
        option1Button.GetComponentInChildren<Text>().text = option1Text;
        option2Button.GetComponentInChildren<Text>().text = option2Text;
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);
    }

    void ChooseOption1()
    {
        switch (storyStage)
        {
            case 0:
                ShowDialogue("You decide to keep walking, trusting your instincts.");
                storyStage++;
                break;
            case 1:
                ShowDialogue("Arin the Mage offers to teach you about your powers.");
                playerHealth += 10; // Reward for making a friend
                Debug.Log($"{playerName}'s health increased to {playerHealth}");
                storyStage++;
                break;
        }
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        ProgressStory();
    }

    void ChooseOption2()
    {
        switch (storyStage)
        {
            case 0:
                ShowDialogue("You look around cautiously, feeling the weight of the forest’s silence.");
                storyStage++;
                break;
            case 1:
                ShowDialogue("You encounter a Dark Knight. He blocks your path with a menacing glare.");
                storyStage++;
                break;
        }
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        ProgressStory();
    }

    void EncounterCharacter()
    {
        ShowDialogue("A figure emerges from the shadows...");
        storyStage++;
        SetChoices("Greet the stranger", "Draw your weapon");
    }

    void Battle(string enemy)
    {
        ShowDialogue($"You engage in battle with the {enemy}!");
        inBattle = true;
        int enemyHealth = 30;

        while (enemyHealth > 0 && playerHealth > 0)
        {
            int action = Random.Range(1, 3);

            if (action == 1)
            {
                ShowDialogue($"You strike the {enemy}!");
                enemyHealth -= 10;
                Debug.Log($"{enemy} health: {enemyHealth}");
            }
            else if (action == 2)
            {
                ShowDialogue($"You defend against the {enemy}'s attack!");
                playerHealth -= 5;
                Debug.Log($"{playerName}'s health: {playerHealth}");
            }
        }

        if (enemyHealth <= 0)
        {
            ShowDialogue($"You have defeated the {enemy}!");
            inBattle = false;
            storyStage++;
            ProgressStory();
        }
        else if (playerHealth <= 0)
        {
            ShowDialogue("You have fallen... The journey ends here.");
            inBattle = false;
            EndGame();
        }
    }

    void UseDemonicPower()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log($"{playerName} channels her power, releasing a surge of energy!");
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No Rigidbody component found on this GameObject.");
        }
    }

    void EndGame()
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        ShowDialogue("Thank you for playing.");
    }
}