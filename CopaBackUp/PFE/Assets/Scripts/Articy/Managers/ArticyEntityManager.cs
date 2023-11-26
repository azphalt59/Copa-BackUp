using Articy.Unity;
using Articy.Copacetic;
using Articy.Unity.Interfaces;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArticyEntityManager : MonoBehaviour
{
    public static ArticyEntityManager Instance;
    private void Awake()
    {
        Instance = this;
        Init();
    }

    private List<Entity> _allEntities;
    private List<DefaultMainCharacterTemplate> _mainCharacters;

    [SerializeField] private List<ArticyReference> _entityReferences;

    public static Dictionary<Entity, GameObject> EntityToGameObject;
    public static Dictionary<GameObject, Entity> GameObjectToEntity;


    private void Init()
    {
        _allEntities = ArticyDatabase.GetAllOfType<Entity>();

        _mainCharacters = ArticyDatabase.GetAllOfType<DefaultMainCharacterTemplate>();



        EntityToGameObject = new Dictionary<Entity, GameObject>();
        GameObjectToEntity = new Dictionary<GameObject, Entity>();

        // thoses are pretty complex but only ran once and in (theory) small quantity
        foreach (ArticyReference er in _entityReferences)
        {
            Entity entity = er.reference.GetObject<Entity>();

            if(entity != null)
            {
                if (_allEntities.Contains(entity))
                {
                    EntityToGameObject.Add(entity, er.gameObject);
                    GameObjectToEntity.Add(er.gameObject, entity);
                }
            }
            else
            {
                Debug.LogError($"Error : {er.gameObject} does not reference an entity");
            }
        }


        string all = "";
        foreach (Entity e in _allEntities)
        {
            all += e.DisplayName + '\n';
        }
        Debug.Log("All entities : \n" + all);

        all = "";
        foreach (Entity e in EntityToGameObject.Keys)
        {
            all += $"E.{e.DisplayName} -> GO.{EntityToGameObject[e]}\n";
        }
        Debug.Log("All Respresentations : \n" + all);
    }
}
