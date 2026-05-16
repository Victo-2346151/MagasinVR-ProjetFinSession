# Musée VR — Villes du Maroc

## Projet Final — Environnements Immersifs

**Noms :** Rachid Touil — Achraf Errihani 
**Session :** Hiver 2026  
**Cours :** Environnements Immersifs — Cégep de Victoriaville  
**Genre :** Simulation / Visite virtuelle

---

## Description

Visite virtuelle d'un musée en réalité virtuelle pour Meta Quest 2.  
Le joueur explore 2 salles et découvre 9 villes du Maroc à travers des photos et des descriptions.  
La visite est complétée quand le joueur a consulté toutes les villes.

---

## Plateforme

- Unity 6000.3.5f1
- Meta Quest 2
- XR Interaction Toolkit / OpenXR

---

## Contrôles

- **Joystick gauche** : se déplacer
- **Joystick droit** : tourner
- **Téléportation** : pointer vers le sol + joystick
- **Bouton Description** : afficher la description de la ville
- **Bouton Audio** : écouter la description en voix off
- **Bouton Fermer** : fermer la description

---

## Villes présentées

1. Marrakech
2. Fès
3. Chefchaouen
4. Casablanca
5. Essaouira
6. Rabat
7. Merzouga
8. Agadir
9. Tanger

---

## Utilisation de l'IA (Claude AI)

Dans le cadre de ce projet, l'intelligence artificielle Claude AI a été utilisée comme assistant technique. Voici les utilisations retenues :

### Ajustements UI et dimensions
- Calcul des dimensions et positions des Canvas World Space (Scale, Width, Height, Position)
- Positionnement du CanvasHUD comme enfant de la caméra
- Ajustement des dimensions du CanvasPopup pour la lisibilité en VR

### Solutions techniques hors cours
- `Resources.FindObjectsOfTypeAll<Canvas>()` — pour trouver le CanvasPopup désactivé dans la scène
- `GameObject.Find("MusiqueAmbiance")` — pour trouver la musique d'ambiance depuis les tableaux
- `Invoke("RemontrerMusique", descriptionAudio.length)` — pour remonter la musique après la voix off
- `Invoke("AfficherCanvasFin", 2f)` — délai avant l'affichage du canvas de fin


### Structure et architecture
- Répartition des tâches entre les membres de l'équipe
- Suggestions pour la gestion Git sans conflits
- Generation des Summury et commenataires de code

---

## Sources des images

Toutes les images utilisées dans ce projet proviennent de **Wikimedia Commons** et sont dans le domaine public ou sous licence libre.

---

## Sources audio

Les fichiers audio de description des villes ont été générés avec un outil de synthèse vocale Gemini.  
