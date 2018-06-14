# Projet 2A : Game With A Purpose

## Requis

* Unity3D (version **2017** au moins, **Personal**) : [Télécharger ici](https://store.unity.com)
* NodeJS (version **v8.9.4**) : [Télécharger ici](https://nodejs.org/en/download/)
* GitHub : [Notre répertoire ici](https://github.com/theo-gautier/Projet2A)

On considère à partir de maintenant que le répertoire Git a été cloné.

## Lancer le serveur

Le serveur est indispensable pour jouer. Il faut donc le lancer.

1) Se placer dans le répertoire `Projet2A/Server/`
2) Importer les modules : `npm install`
3) Lancer le serveur : `node index.js`

N.B. Le serveur se lancera sur le port `3131`. Attention, ce port doit être libre. Si ce n'est pas le cas vous pouvez le modifier, mais à la fois dans le serveur (`index.js`) et dans le client (`Assets/Scripts/DataController.cs`)

## Lancer le jeu

Pour lancer le jeu, il faut ouvrir la scène `LogMenu1.0` se trouvant dans `Projet2A/Assets/Scenes/`
Par la suite, le bouton **Play** permet de lancer le jeu. Pour avoir le jeu en plein écran, vérifiez que le bouton **Maximize On Play** est coché dans la fenêtre Game.

## Jouer au jeu

Pour se connecter ou créer un compte, il faut savoir que lors de la création, un mot de passe trop court est refusé. Ainsi, on ne peut créer un compte si le mot de passe ne fait pas au moins 4 caractères, et possède quelques attributs – caractères spéciaux, présence de majuscules et minuscules. 
Un mot de passe comme Player1234 est suffisamment fort pour créer un compte. Il est possible de vérifier la création de personnage avec **Postman**, en suivant la route `localhost:3131/players/nomperso.` 

Il est nécessaire de créer un appartement avant de pouvoir tester le mode « Trouver ». A la fin du temps imparti, le joueur doit quitter lui-même la scène, en cliquant sur le bouton Return.

Actuellement à chaque lancement du serveur, une nouvelle base de donnée est créée.