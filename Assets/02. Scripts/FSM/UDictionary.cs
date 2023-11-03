using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    // 생성자
    public UKeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

// ISerializationCallbackReceiver : 딕셔너리를 Serializable하게 만들기 위해 필요한 인터페이스.
[Serializable]
public class UDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    // 딕셔너리 접근하기 위한 접근자.
    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }
    // 인스펙터창에 리스트를 노출하여 입력받는다.
    [SerializeField] private List<UKeyValuePair<TKey, TValue>> _list;
    private Dictionary<TKey, TValue> _dictionary;
    
    // 딕셔너리 랩핑
    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
    public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

    // Serialize : 이 데이터를 유니티 인스펙터창에 띄워주는 함수
    public void OnBeforeSerialize()
    {
    }

    // Deserialize : 유니티 인스펙터 창에서 값을 입력한 것을 가지고 데이터를 만들어내는 함수.
    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        // 입력받은 내용을 딕셔너리에 복제.
        foreach (var pair in _list)
        {
            _dictionary.Add(pair.key, pair.value);
        }
    }
}