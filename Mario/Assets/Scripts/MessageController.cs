using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    protected Dictionary<char, Sprite> letters;
    public GameObject prefabLetter;
    protected string message;

    // Start is called before the first frame update
    void Awake()
    {
        letters = new Dictionary<char, Sprite>();
        string path = "letters";

        Sprite[] lettersSprite = Resources.LoadAll<Sprite>(path);

        for (int i = 97; i <= 122; i++)
        {
            foreach(Sprite s in lettersSprite)
            {
                if(s.name.Contains("("+(i-96)+")"))
                {
                    letters.Add(System.Convert.ToChar(i), s);
                }
            }
        }

        path = "numbers";

        Sprite[] numbersSprite = Resources.LoadAll<Sprite>(path);
        foreach (Sprite s in numbersSprite)
        {
            letters.Add(System.Convert.ToChar(s.name), s);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string message)
    {
        this.message = message;
    }

    public string GetMessage()
    {
        return message;
    }

    public void Draw()
    {
        float offset = 0;
        foreach(char c in message)
        {
            if(c != ' ')
            {
                prefabLetter.GetComponent<SpriteRenderer>().sprite = letters[c];
                prefabLetter.transform.position =
                    new Vector2(this.transform.position.x + offset,
                        this.transform.position.y);
                GameObject letter = GameObject.Instantiate(prefabLetter);
                letter.transform.parent = this.gameObject.transform;

            }
            offset += 0.45f;
        }
    }

    public void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
