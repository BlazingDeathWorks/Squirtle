using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Transform parent;
    public GameObject block;
    public string mysteryWord;

    // Start is called before the first frame update
    void Start()
    {
        CreateMysteryWord();
        for(int i = 0; i < mysteryWord.Length; i++)
        {
            Instantiate(block, transform.position, Quaternion.identity).transform.SetParent(parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            string inputString = (Input.inputString);

            if(string.IsNullOrEmpty(inputString))
            {
                return;
            }
        }
    }

    public void CreateMysteryWord()
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\lijet\OneDrive\Documents\GitHub\Squirtle\Assets\Pokemon Names.txt");

        mysteryWord = lines[Random.Range(0, lines.Length)];

        Debug.Log(mysteryWord);
    }
}