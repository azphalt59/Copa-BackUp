using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    //[SerializeField] private float playerEnergy = 100f;
    //[SerializeField] private Slider energySlider;
    //private float playerMaxEnergy;

    //[Header("Energy Cost")]
    //[Tooltip("Coût en énergie pour traversé un mur")][SerializeField] private float wallEnergyCost = 25f;
    //[Tooltip("Coût en énergie par seconde de la possession")][SerializeField] private float possessionEnergyCost = 5f;
    //[Tooltip("Régénération par seconde d'energie en état libre")][SerializeField] private float energyRegeneration = 2.5f;
    //[Tooltip("Régénération par seconde d'energie en état libre")][SerializeField] private float energyIntangibleCost = 2.5f;

    //private void Start()
    //{
    //    playerMaxEnergy = playerEnergy;
    //    energySlider.maxValue = playerMaxEnergy;
    //    energySlider.value = playerMaxEnergy;
    //}
    //private void Update()
    //{
    //    energySlider.value = playerEnergy;
    //}

    //public void RemoveEnergy(float amount)
    //{
    //    if (playerEnergy - amount <= 0)
    //    {
    //        playerEnergy = 0;
    //        OutOfEnergy();
    //    }
    //    else
    //    {
    //        playerEnergy -= amount;
    //    }
    //}
    //public void EnergyRegenerationOvertime(float amount)
    //{
    //    if (playerEnergy >= playerMaxEnergy && amount > 0)
    //        return;
    //    if (playerEnergy <= 0 && amount < 0)
    //        return;

    //    playerEnergy += amount * Time.deltaTime;
    //}
    //public void AddEnergy(float amount)
    //{
    //    if(playerEnergy + amount >= playerMaxEnergy)
    //    {
    //        playerEnergy = playerMaxEnergy;
    //        FullOfEnergy();
    //    }
    //    else
    //    {
    //        playerEnergy += amount;
    //    }
    //}

    //public float GetPlayerEnergy()
    //{
    //    return playerEnergy;
    //}
    //private void OutOfEnergy()
    //{
    //    Debug.Log("Out of energy");
    //}
    //private void FullOfEnergy()
    //{
    //    Debug.Log("Full of Energy");
    //}
    //public float GetWallEnergyCost()
    //{
    //    return wallEnergyCost;
    //}
    //public float GetPossessionEnergyCost()
    //{
    //    return -possessionEnergyCost;
    //}
    //public float GetEnergyRegeneration()
    //{
    //    return energyRegeneration;
    //}
    //public float GetIntangibleEnergyCost()
    //{
    //    return -energyIntangibleCost;
    //}
}
