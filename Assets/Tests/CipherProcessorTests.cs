using NUnit.Framework;
using UnityEngine;

public class CipherProcessorTests
{
    private CipherProcessor processor;

    [SetUp]
    public void Setup()
    {
        var go = new GameObject();
        processor = go.AddComponent<CipherProcessor>();
    }

    // -------------------------------
    // Caesar Cipher Tests
    // -------------------------------

    [Test]
    public void CaesarCipher_Shift3_EncryptsCorrectly()
    {
        string result = processor.Caesar("abc", 3);
        Assert.AreEqual("def", result);
    }

    [Test]
    public void CaesarCipher_ShiftWithWraparound_Works()
    {
        string result = processor.Caesar("xyz", 3);
        Assert.AreEqual("abc", result);
    }

    [Test]
    public void CaesarCipher_NegativeShift_ShiftsBackward()
    {
        string result = processor.Caesar("def", -3);
        Assert.AreEqual("abc", result);
    }

    [Test]
    public void CaesarCipher_PreservesNonLetters()
    {
        string result = processor.Caesar("a b.c!", 1);
        Assert.AreEqual("b c.d!", result);
    }

    [Test]
    public void CaesarCipher_EmptyInput_ReturnsEmpty()
    {
        string result = processor.Caesar("", 5);
        Assert.AreEqual("", result);
    }

    // -------------------------------
    // Vigenère Cipher Tests
    // -------------------------------

    [Test]
    public void VigenereCipher_BasicEncryption_Works()
    {
        string result = processor.Vigenere("attackatdawn", "lemon");
        Assert.AreEqual("lxfopvefrnhr", result);
    }

    [Test]
    public void VigenereCipher_HandlesUpperAndLowerCase()
    {
        string result = processor.Vigenere("HelloWorld", "key");
        Assert.AreEqual("RijvsUyvjn", result);
    }

    [Test]
    public void VigenereCipher_WrapsKeywordCorrectly()
    {
        string result = processor.Vigenere("aaaaa", "abc");
        Assert.AreEqual("abcab", result);
    }

    [Test]
    public void VigenereCipher_EmptyInput_ReturnsEmpty()
    {
        string result = processor.Vigenere("", "key");
        Assert.AreEqual("", result);
    }

    [Test]
    public void VigenereCipher_NullOrEmptyKey_ReturnsUnchanged()
    {
        Assert.AreEqual("hello", processor.Vigenere("hello", ""));
        Assert.AreEqual("hello", processor.Vigenere("hello", null));
    }
}
