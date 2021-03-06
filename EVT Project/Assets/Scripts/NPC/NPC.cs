﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class NPC : MonoBehaviour
{
    CitizenStats citizenStats = new CitizenStats();
    public int typeSelector;
    public int enemyConverter;
    // Se Define la estructura de los tipos de NPC
    public enum NPCType
    {
        enemy1,
        enemy2,
        boy,
        girl
    }

    public void Start()
    {
        Selector(); // Se llama desde el Start al Selector del tipo de NPC
        citizenStats.currentHealth = citizenStats.health;
    }

    void Selector()
    {
        typeSelector = Random.Range(0, 4); // De manera aleatoria saca un indice que servira para dar formato al NPC
        NPCType npcType = new NPCType();
        switch (typeSelector)
        {// Cada NPC tendrá diferentes caracteristicas de acuerdo con el indice obtenido en el typeSelector
            case 0:
                npcType = NPCType.enemy1;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                EnemyText.enemyCounter[0]++;
                break;
            case 1:
                npcType = NPCType.enemy2;
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                EnemyText.enemyCounter[1]++;
                break;
            case 2:
                npcType = NPCType.boy;
                gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                gameObject.tag = "Boy";
                citizenStats.currentHealth = Random.Range(25, 150);
                break;
            case 3:
                npcType = NPCType.girl;
                gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                gameObject.tag = "Girl";
                citizenStats.currentHealth = Random.Range(20, 130);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Los Enemigos pueden Convertir a los Ciudadanos en Enemigos, estos tienen diferentes caracteristicas, reflejadas a continuacion
        if (other.tag == "NPC" && (gameObject.tag == "Boy" || gameObject.tag == "Girl"))
        {
            citizenStats.currentHealth -= 5;
            if (citizenStats.currentHealth <= 0)
            {
                gameObject.tag = "ConvertedNPC";
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                EnemyText.enemyCounter[2] += 1;
            }
        }
    }
}

public sealed class CitizenStats : Stats
{
    public CitizenStats()
    {
        health = 100;
    }
}
