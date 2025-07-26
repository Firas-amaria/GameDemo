using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SavedNotesManagerTests
{
    private SavedNotesManager manager;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject();
        manager = go.AddComponent<SavedNotesManager>();
        manager.disableFileIO = true;

        manager.noteDatabase = new SavedNoteDatabase(); // Inject fake DB
    }

    [Test]
    public void SaveNewNote_AddsNoteToList()
    {
        manager.SaveNewNote("Hello world");

        List<string> notes = manager.GetAllNotes();
        Assert.Contains("Hello world", notes);
    }

    [Test]
    public void SaveNewNote_IgnoresEmptyString()
    {
        manager.SaveNewNote("");
        manager.SaveNewNote("   ");
        manager.SaveNewNote(null);

        Assert.AreEqual(0, manager.GetAllNotes().Count);
    }

    [Test]
    public void DeleteNoteAt_ValidIndex_RemovesNote()
    {
        manager.SaveNewNote("Note 1");
        manager.SaveNewNote("Note 2");

        manager.DeleteNoteAt(0);

        List<string> notes = manager.GetAllNotes();
        Assert.AreEqual(1, notes.Count);
        Assert.AreEqual("Note 2", notes[0]);
    }

    [Test]
    public void DeleteNoteAt_InvalidIndex_DoesNothing()
    {
        manager.SaveNewNote("Note 1");

        manager.DeleteNoteAt(10); // invalid
        manager.DeleteNoteAt(-1); // invalid

        List<string> notes = manager.GetAllNotes();
        Assert.AreEqual(1, notes.Count);
    }

    [Test]
    public void GetAllNotes_ReturnsCurrentNoteList()
    {
        manager.SaveNewNote("A");
        manager.SaveNewNote("B");

        List<string> notes = manager.GetAllNotes();
        Assert.AreEqual(2, notes.Count);
        Assert.AreEqual("A", notes[0]);
        Assert.AreEqual("B", notes[1]);
    }
}
