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
    /*Choisis une coordonn�e dans un cercle autour du joueur, multipli�e par spawnRadius pour que 
    l'ennemi n'apparaisse pas au contact du joueur
    spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
    
         //Fonction spawnant les ennemis
    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPos = player.transform.position; //R�cup�re la position du joueur
        /*Choisis une coordonn�e dans un cercle autour du joueur, multipli�e par spawnRadius pour que 
        l'ennemi n'apparaisse pas au contact du joueur*/
        //spawnPos += Random.insideUnitCircle.normalized* spawnRadius;

        //Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        //yield return new WaitForSeconds(timeE);//Attends un d�lai avant de rappeler la fonction
        //BufferEnemy();//Appel de la m�thode Tampon
    //}

    /*M�thode qui sert de tampon �vitant que la coroutine SpawnAnEnnemy tourne en continue
    permettant ainsi de changer les valeurs des variables utilis�es dans la coroutine 
    pendant le laps de temps durant lequel elle est � l'arr�t*/
    /*private void BufferEnemy()
    {
        StartCoroutine(SpawnAnEnemy());//Relance la coroutine
    }*/
}
