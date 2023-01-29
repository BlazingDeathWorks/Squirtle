using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform parent;
    public GameObject block;
    public string mysteryWord;
    public List<GameObject> blocks = new List<GameObject>();
    public int index = 0;
    public string guess = "";
    public string symbols;
    public Color correct, incorrect, exists;

    // Start is called before the first frame update
    void Awake()
    {
        CreateMysteryWord();
        for(int i = 0; i < mysteryWord.Length; i++)
        {
            GameObject instance = Instantiate(block, transform.position, Quaternion.identity);
            instance.transform.SetParent(parent);
            instance.transform.localScale = Vector3.one;
            blocks.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
    }

    public void CreateMysteryWord()
    {
        string[] lines = File.ReadAllLines(@"C:\Users\lijet\OneDrive\Documents\GitHub\Squirtle\Assets\Pokemon Names.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"D:\Users\Game Dev Storage\Game Dev Games\Squirtle\Assets\Pokemon Names.txt");

        mysteryWord = lines[Random.Range(0, lines.Length)].ToUpper();

        Debug.Log(mysteryWord);
    }

    public void InputSystem()
    {
        if (Input.anyKeyDown)
        {
            string inputString = (Input.inputString);

            if(inputString == "\n"|| inputString == "\r")
            {
                CheckWord();
                HighLightBlock();
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
                blocks[index].GetComponentInChildren<Text>().text = "";

                guess = guess.Substring(0, guess.Length - 1);
                return;
            }

            if (index >= blocks.Count)
            {
                return;
            }

            blocks[index].GetComponentInChildren<Text>().text = inputString.ToUpper();
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
        for(int i = 0; i < blocks.Count; i++)
        {
            Image backGround = blocks[i].GetComponentInChildren<Image>();

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
}