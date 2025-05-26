using System.Collections.Generic;

[System.Serializable]
public class BookDataModel
{
    public string slotKey;
    public string title;
    public string genre;
    public string author;
    public string description;
    public List<string> pages;
}

[System.Serializable]
public class BookDatabase
{
    public List<BookDataModel> books;
}
