using System.Collections.Generic;
using System.Windows.Input;

namespace SuperMarioWPF;

public class GameKeyboard
{
    private readonly Dictionary<Key, KeyStates> keyStates;

    public GameKeyboard()
    {
        keyStates = new Dictionary<Key, KeyStates>();
    }

    public void Set(Key k, KeyStates s)
    {
        keyStates[k] = s;
    }

    public KeyStates Get(Key k)
    {
        if (keyStates.ContainsKey(k))
            return keyStates[k];
        return keyStates[k] = KeyStates.None;
    }
}