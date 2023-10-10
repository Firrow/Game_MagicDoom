using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //NOTE LIGHT UP : Fonction spawnant les ennemis
    /*Choisis une coordonnée dans un cercle autour du joueur, multipliée par spawnRadius pour que 
    l'ennemi n'apparaisse pas au contact du joueur
    spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
    
         //Fonction spawnant les ennemis
    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPos = player.transform.position; //Récupère la position du joueur
        /*Choisis une coordonnée dans un cercle autour du joueur, multipliée par spawnRadius pour que 
        l'ennemi n'apparaisse pas au contact du joueur*/
        //spawnPos += Random.insideUnitCircle.normalized* spawnRadius;

        //Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        //yield return new WaitForSeconds(timeE);//Attends un délai avant de rappeler la fonction
        //BufferEnemy();//Appel de la méthode Tampon
    //}

    /*Méthode qui sert de tampon évitant que la coroutine SpawnAnEnnemy tourne en continue
    permettant ainsi de changer les valeurs des variables utilisées dans la coroutine 
    pendant le laps de temps durant lequel elle est à l'arrêt*/
    /*private void BufferEnemy()
    {
        StartCoroutine(SpawnAnEnemy());//Relance la coroutine
    }*/
}
