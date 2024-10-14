using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard 
{
    Dictionary<string,object> Black_Board_Data = new Dictionary<string,object>();
    public delegate void OnBlackBoardValueChange(string key,  object value);    
    public event OnBlackBoardValueChange onBlackBoardValueChange;
    public void SetOrAddData(string key, object value)
    {
        if (Black_Board_Data.ContainsKey(key))
        {
            Black_Board_Data[key] = value;
        }
        else
        {
            Black_Board_Data.Add(key, value);
        }
        onBlackBoardValueChange?.Invoke(key, value);
    }
    public void RemoveData(string key)
    {
        Black_Board_Data.Remove(key);
        onBlackBoardValueChange?.Invoke(key,null);
    }
    public bool GetData<T>(string key, out T val)
    {
        val = default(T);
        if (Black_Board_Data.ContainsKey(key))
        {

            val = (T)Black_Board_Data[key];
            return true;
        }
        return false; // Key not found
    }
    public bool HasKey(string key)
    {
        return Black_Board_Data.ContainsKey(key);
    }
}
