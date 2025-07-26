using NUnit.Framework;
using UnityEngine;
using System.Linq;

public class ProgressManagerTests
{
    private ProgressManager manager;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject();
        manager = go.AddComponent<ProgressManager>();

        manager.disableFileIO = true;
        manager.data = new ProgressData();  // Inject fake progress
    }

    [Test]
    public void UnlockBook_AddsBookToList()
    {
        manager.UnlockBook("room1_slot1");
        Assert.Contains("room1_slot1", manager.data.unlockedBooks);
    }

    [Test]
    public void UnlockBook_DoesNotAddDuplicate()
    {
        manager.UnlockBook("room1_slot1");
        manager.UnlockBook("room1_slot1"); // try again

        int count = manager.data.unlockedBooks.Count(b => b == "room1_slot1");
        Assert.AreEqual(1, count);
    }

    [Test]
    public void UnlockCipher_AddsCipherToList()
    {
        manager.UnlockCipher("Caesar");
        Assert.Contains("Caesar", manager.data.unlockedCiphers);
    }

    [Test]
    public void UnlockCipher_DoesNotAddDuplicate()
    {
        manager.UnlockCipher("Caesar");
        manager.UnlockCipher("Caesar");

        int count = manager.data.unlockedCiphers.Count(c => c == "Caesar");
        Assert.AreEqual(1, count);
    }

    [Test]
    public void SetCurrentRoom_UpdatesRoomValue()
    {
        manager.SetCurrentRoom("Room 2");
        Assert.AreEqual("Room 2", manager.data.currentRoom);
    }

    [Test]
    public void MarkInteractionHintSeen_AddsNewHint()
    {
        manager.MarkInteractionHintSeen("hint1");

        Assert.IsTrue(manager.data.interactionHintsShown.Any(h => h.id == "hint1" && h.seen));
    }

    [Test]
    public void MarkInteractionHintSeen_UpdatesExistingHint()
    {
        manager.data.interactionHintsShown.Add(new HintEntry { id = "hint1", seen = false });

        manager.MarkInteractionHintSeen("hint1");

        var updated = manager.data.interactionHintsShown.First(h => h.id == "hint1");
        Assert.IsTrue(updated.seen);
    }

    [Test]
    public void HasSeenInteractionHint_ReturnsCorrectly()
    {
        manager.MarkInteractionHintSeen("hint1");

        bool result = manager.HasSeenInteractionHint("hint1");

        Assert.IsTrue(result);
    }

    [Test]
    public void HasSeenInteractionHint_ReturnsFalseIfNotSeen()
    {
        bool result = manager.HasSeenInteractionHint("neverSeenHint");

        Assert.IsFalse(result);
    }
}
