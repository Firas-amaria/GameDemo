using UnityEngine;

public class CipherProcessor : MonoBehaviour
{
    public static CipherProcessor Instance;

    private void Awake()
    {
        Instance = this;

    }

    // ================================
    // Caesar Cipher
    // ================================
    public string Caesar(string input, int key)
    {
        char[] result = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                result[i] = (char)(((c - offset + key + 26) % 26) + offset);
            }
            else
            {
                result[i] = c;
            }
        }

        return new string(result);
    }

    // ================================
    // Vigenère Cipher
    // ================================
    public string Vigenere(string input, string keyword)
    {
        if (string.IsNullOrEmpty(keyword)) return input;

        char[] result = new char[input.Length];
        keyword = keyword.ToLower();
        int keywordIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (char.IsLetter(c))
            {
                int shift = keyword[keywordIndex % keyword.Length] - 'a';
                char offset = char.IsUpper(c) ? 'A' : 'a';
                result[i] = (char)(((c - offset + shift + 26) % 26) + offset);
                keywordIndex++;
            }
            else
            {
                result[i] = c;
            }
        }

        return new string(result);
    }
}
