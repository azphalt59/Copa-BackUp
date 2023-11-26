using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_PossessionSwitcher : MonoBehaviour
{
    public PossessionController Controller;
    public List<PhysicPossessable> Possessables;
    private int _possessedIndex;

    // Start is called before the first frame update
    void Start()
    {
        if(Controller != null)
        {
            if (Controller.IsPossessing)
            {
                _possessedIndex = -1;

                for(int i = 0; i < Possessables.Count; i++)
                {
                    if(Possessables[i] == Controller.Possession)
                    {
                        _possessedIndex = i;
                        break;
                    }
                }
            }
            else if (Possessables != null && Possessables.Count > 0)
            {
                Controller.Possession = Possessables[0];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _possessedIndex = (_possessedIndex + 1) % Possessables.Count;
            Controller.Possession = Possessables[_possessedIndex];
        }
    }
}
