using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class Person
{
    private string id;
    private string name;

    // it's a class
    public Person()
    {
        id = "";
        name = "";
    }

    public string ID   // property
    {
        get { return id; }   // get method
        set { id = value; }  // set method
    }
    
    public string Name   // property
    {
        get { return name; }   // get method
        set { name = value; }  // set method
    }
}

public class FirstRunLogic : MonoBehaviour
{

    private List<Person> _persons = new List<Person>();

    private ListView personsListView;
    
    private Button generateItemButton;
    private Button clearItemsButton;

    private Label itemsCountLabel;

    private void Start()
    {
        //document ui setup
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        //bind the label with the list count
        itemsCountLabel = root.Q<Label>("items-count");
        itemsCountLabel.text = "items count: " + _persons.Count.ToString();

        generateItemButton = root.Q<Button>("generate-item-button");
        generateItemButton.Focus();
        generateItemButton.clicked += GenerateItem;

        clearItemsButton = root.Q<Button>("clear-items-button");
        clearItemsButton.clicked += ClearItems;

        personsListView = root.Q<ListView>("personsListView");
        
        //personsListView.

        //initial set of random people
        for (int i = 0; i < 5; i++)
        {
            GenerateItem();
        }
    }

    public void ClearItems()
    {
        _persons.Clear();
        Debug.Log(_persons.Count);
    }

    /// <summary>
    /// Generate a random person and add it to the list of persons in FirstRunLogic instance
    /// </summary>
    public void GenerateItem()
    {
        Person p = new Person();
        
        // get random number from 1000 to 9999
        Random rnd = new Random();
        int number  = rnd.Next(1000, 10000);

        p.ID = number.ToString();
        p.Name = GenerateName(rnd.Next(3,10));
        
        _persons.Add(p);
        
        itemsCountLabel.text = "items count: " + _persons.Count.ToString();
        
        Debug.Log(p.ID+ " - " + p.Name);

    }
    
    /// <summary>
    /// Generate a random name and returns it
    /// </summary>
    private string GenerateName(int len)
    { 
        Random r = new Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string Name = "";
        Name += consonants[r.Next(consonants.Length)].ToUpper();
        Name += vowels[r.Next(vowels.Length)];
        int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len)
        {
            Name += consonants[r.Next(consonants.Length)];
            b++;
            Name += vowels[r.Next(vowels.Length)];
            b++;
        }

        return Name;
    }
}
