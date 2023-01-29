using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Transform parent;
    public GameObject block;
    public string mysteryWord;
    public List<GameObject> blocks = new List<GameObject>();

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
        string[] lines = File.ReadAllLines(@"C:\Users\lijet\OneDrive\Documents\GitHub\Squirtle\Assets\Pokemon Names.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"D:\Users\Game Dev Storage\Game Dev Games\Squirtle\Assets\Pokemon Names.txt");

        mysteryWord = lines[Random.Range(0, lines.Length)];

        Debug.Log(mysteryWord);
    }
}