﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public DefaultRoomSection[] prefabs; // секции с двумя контактными точками
    public DefaultRoomSection[] start; // стартовые секции
    public DefaultRoomSection[] stop; // конечные секции
    public int MaximumSections = 20; // макс. возможное число секций
    public float sectionSize = 1; // размер секции, все стороны должны быть равны
    private System.Random random = new System.Random();
    private DefaultRoomSection current, previous;
    private int index;

    void Awake()
    {
        Generate();
    }

    void InstSection(DefaultRoomSection[] arr)
    {
        current = Instantiate(arr[Random.Range(0, arr.Length)]) as DefaultRoomSection;
        current.gameObject.name = "Section_" + index;
        current.transform.parent = transform;

        if (previous)
        {
            var point = previous.endPoint[random.Next(0, previous.endPoint.Length)];
            //foreach(var point in previous.endPoint)
            //{
                current.transform.forward = point.forward;
                current.transform.position += point.position - current.startPoint.position;
            //}
        }
    }

    void Generate()
    {
        InstSection(start);
        previous = current;

        DefaultRoomSection tmp = null;

        for (int i = 0; i < MaximumSections; i++)
        {
            index = i;
            
            if (!Check())
            {
                tmp = current;
                InstSection(prefabs);
            }
            else
            {
                Destroy(current.gameObject);
                previous = tmp;
                InstSection(stop);
                return;
            }

            previous = current;
        }

        InstSection(stop);
    }

    private bool Check() // проверка, есть ли на пути ранее созданные секции
    {
        foreach (var point in current.endPoint)
        {
            Vector3 position = point.position + point.forward * sectionSize / 2;
            Collider[] colliders = Physics.OverlapSphere(position, sectionSize /4);
            foreach (Collider hit in colliders) if (hit) return true;
            
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
