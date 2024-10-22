using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] parent;
    public GameObject block;
    public string mysteryWord;
    public List<List<GameObject>> blocks = new List<List<GameObject>>();
    public int index = 0;
    public int blocksIndex = 0;
    public string guess = "";
    public string symbols;
    public Color correct, incorrect, exists;
    public GameObject EndOverlay;
    public GameObject TryAgainScreen;

    // Start is called before the first frame update
    void Awake()
    {
        CreateMysteryWord();
        for (int i = 0; i < parent.Length; i++)
        {
            blocks.Add(new List<GameObject>());
            for (int j = 0; j < mysteryWord.Length; j++)
            {
                GameObject instance = Instantiate(block, transform.position, Quaternion.identity);
                instance.transform.SetParent(parent[i]);
                instance.transform.localScale = Vector3.one;
                blocks[i].Add(instance);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        NoTriesLeft();
    }

    public void CreateMysteryWord()
    {
        //string[] lines = File.ReadAllLines(@"C:\Users\lijet\OneDrive\Documents\GitHub\Squirtle\Assets\Pokemon Names.txt");
        string[] lines = System.IO.File.ReadAllLines("Assets/Pokemon Names.txt");

        mysteryWord = lines[Random.Range(0, lines.Length)].ToUpper();

        Debug.Log(mysteryWord);
    }

    public void InputSystem()
    {
        if (Input.anyKeyDown)
        {
            string inputString = (Input.inputString);


            if ((inputString == "\n" || inputString == "\r"))
            {
                if (guess.Length != mysteryWord.Length)
                {
                    return;
                }

                CheckWord();
                HighLightBlock();
                AllCorrect();
                blocksIndex++;
                index = 0;
                guess = "";
                return;
            }

            if (string.IsNullOrEmpty(inputString))
            {
                return;
            }

            if (inputString == "\b")
            {
                if (index <= 0)
                {
                    return;
                }

                index--;
                blocks[blocksIndex][index].GetComponentInChildren<Text>().text = "";

                guess = guess.Substring(0, guess.Length - 1);
                return;
            }

            if (index >= blocks[blocksIndex].Count)
            {
                return;
            }

            blocks[blocksIndex][index].GetComponentInChildren<Text>().text = inputString.ToUpper();
            index++;

            guess += inputString.ToUpper();
        }
    }

    public void CheckWord()
    {
        symbols = "";

        for(int i = 0; i < mysteryWord.Length; i++)
        {
            if(guess.Substring(i, 1) == mysteryWord.Substring(i, 1))
            {
                symbols += "+";
            }

            else if(mysteryWord.IndexOf(guess.Substring(i, 1)) != -1)
            {
                symbols += "?";
            }

            else
            {
                symbols += "-";
            }
        }
        Debug.Log(symbols);
    }

    public void HighLightBlock()
    {
        for(int i = 0; i < blocks[blocksIndex].Count; i++)
        {
            Image backGround = blocks[blocksIndex][i].GetComponentInChildren<Image>();

            if(symbols.Substring(i,1) == "+")
            {
                //green
                backGround.color = correct;
            }

            else if(symbols.Substring(i,1) == "?")
            {
                //yellow
                backGround.color = exists;
            }

            else if (symbols.Substring(i, 1) == "-")
            {
                //grey
                backGround.color = incorrect;
            }
        }
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void AllCorrect()
    {
        if(guess == mysteryWord)
        {
            EndOverlay.SetActive(true);
        }
    }

    public void NoTriesLeft()
    {
        if(blocksIndex > 4 && guess != mysteryWord)
        {
            TryAgainScreen.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}