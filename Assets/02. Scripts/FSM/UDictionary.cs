using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UKeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    // ������
    public UKeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

// ISerializationCallbackReceiver : ��ųʸ��� Serializable�ϰ� ����� ���� �ʿ��� �������̽�.
[Serializable]
public class UDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    // ��ųʸ� �����ϱ� ���� ������.
    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }
    // �ν�����â�� ����Ʈ�� �����Ͽ� �Է¹޴´�.
    [SerializeField] private List<UKeyValuePair<TKey, TValue>> _list;
    private Dictionary<TKey, TValue> _dictionary;
    
    // ��ųʸ� ����
    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
    public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

    // Serialize : �� �����͸� ����Ƽ �ν�����â�� ����ִ� �Լ�
    public void OnBeforeSerialize()
    {
    }

    // Deserialize : ����Ƽ �ν����� â���� ���� �Է��� ���� ������ �����͸� ������ �Լ�.
    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        // �Է¹��� ������ ��ųʸ��� ����.
        foreach (var pair in _list)
        {
            _dictionary.Add(pair.key, pair.value);
        }
    }
}