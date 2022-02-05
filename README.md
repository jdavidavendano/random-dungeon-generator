# Dungeon Generator


## Descripción
* ### Creación de la mazmorra
  * Un generador de mazmorras aleatorias, utilizamos un algoritmo tipo `random walk` utilizando una seed para generar las habitaciones y después las unimos con pasillos.

* ### Jugabilidad
  * El mapa tiene suelo, paredes y fondo
    * ![image](https://user-images.githubusercontent.com/2703274/152626437-2712307e-c709-403f-b24c-6fdf2b1c9f21.png) 
  * El jugador tiene ataques cuerpo a cuerpo 
    * ![image](https://user-images.githubusercontent.com/2703274/152626368-ded6beb5-94aa-4bd0-81f8-4945dfd79582.png) 
  * Hay dos tipos de enemigos:
    * LogEnemy:
      * ![image](https://user-images.githubusercontent.com/2703274/152626537-e3420d10-c751-4f7d-89cb-f4f6d4adcd12.png)
      * Enemigo con sprite de tronco de madera
      * Requiere ser golpeado dos veces para morir
      * Cada ataque le hace un daño al jugador equivalente a medio corazón
    * OgreBoss:
      * ![image](https://user-images.githubusercontent.com/2703274/152626594-0ea93027-529a-4883-aa6c-8f1fb8ea1a53.png)
      * Enemigo con sprite de ogro
      * Requiere ser golpeado seis veces para morir
      * Cada ataque le hace un daño al jugador equivalente a un corazón
  * Menú de opciones:
    * Se puede introducir una seed para generar la mazmorra (si se usa la seed por defecto (0), se obtendrá una mazmorra con uan seed aleatoria
    * Se puede personalizar la anchura y altura de las habitaciones para la generación de la mazmorra
    * Se puede personalizar la anchura y altura de la mazmorra para su generación 

## Acerca de 

* Versión de Unity:
  * 2020.3.20f1
* Paquetes adicionales:
  * Ninguno
* Los sprites fueron sacados de:
  * Personaje y Enemigo LogEnemy: 
    * https://opengameart.org/content/zelda-like-tilesets-and-sprites 
  * TileSet: 
    * https://pixel-poem.itch.io/dungeon-assetpuck
  * Enemigo OgreBoss:
    * https://drive.google.com/drive/folders/1KAWFL_dIRujTtHdT59ktAY1YJk3bep75  

## Creado por

* Luis Antonio Suárez Bula
* David Pérez Zapata
* Jonathan David Avendaño Ortega
