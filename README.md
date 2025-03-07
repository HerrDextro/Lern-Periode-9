# Lern-Periode 9
23.4 bis 25.6.2024

## Grob-Planung
Aktuell geht es mir sehr gut in der Schule, ich habe ein 5er Schnitt welche sich warscheinlich noch verbessern wird, und ein Paar Fächer wo ich das letzte Semester nicht mein Potenzial erreicht habe.
Dieses Jahr war ich in alle Module so etwa gleich stark, ich habe für Informatik leider ein 5er Schnitt  da ich ein paar Fehler gemacht habe.7
Für dieses Modul wäre es gut, ein Projekt zu machen wo man viel mit der Struktur von viele verschiedene Objekte und die Speicherung von Daten arbeiten muss. Ich wollte ursprünglich unser SmartPainter mit Winforms Graphics box nachmachen, aber habe mich diese Woche dagegen entschieden da Alex es schon mit C++ macht und ich eher nicht eine noch schnellere Version brauche.

## 14.02.2025: Explorativer Wegwerf-Prototyp
- [x] Winforms Graphics box
- [x] 60 FPS

Heute habe ich mithifle von viel ChatGPT ein Picturebox in Winforms gemacht welche individuell Pixel updaten kann (farbe, X, Y). Der Code wurde komplett con KI gereinigt aber manchmal gibt es immer noch ein "object already in use" exception, es passiert aber nicht oft genug um es sinnvoll zu debuggen. 


## 21.2: Explorativer Wegwerf-Prototyp
- [x] Class/Data structure festlegen
- [x] Minimale Variablen festlegen
- [x] Main Menu
- [x] CreateGame (nur difficulty, name)


Heute habe ich auf Papier eine Programmarchitektur für ein Videospiel entwickelt, aber es ist alles nur abtract also gibt es kein echter Game.Render oder so, es ist nur für der Struktur. Ich habe festgelegt, welche Variablen wo sind und wie sie hin und her fliessen, und wie ich sie am besten speicher/bearbeiten könnte. Dann habe ich ein sehr simples proof of Concept Wegwerfprogramm gemacht, welche nur ein 2 optionen hatte zum Game erstellen und nur 3 Sachen als gameData gespeichert hat. Die Struktur hat aber Sinn gemacht und ich habe die Architektur leicht ausgebreitet für das Prototy mit der Kern-Funktionalität. Da ich einfach vergesse zu committen habe ich Google tasks auf mein Handy heruntergeladen und ein recurrent Task eingestellt, welche mit daran erinnert.

## 28.2: Kern-Funktionalität
- [x] Dateien für die vollständige Programmarchitektur hinzufügen
- [x] JSON Datei für die Factions und Playstyles inklusive class dafür
- [x] Display choices(and description) und CreateGame ausbauen mit Faction, playstyle und gamename
- [ ] SaveGame mit JSON savefile (JSON writer) (nicht gemacht weil umwechsel auf SQLite, welche Architekturänderung fordert)
Optional:


Heute habe ich die Wegwerfversion so umgebaut dass sie jetzt komplett mit dem Modell übereinstimmt (nicht mit wenigere Features so wie das Wegwerfmodell) und statt .txt ein JSON Datei wo die Beschreibungen stehen. Ich hatte probleme mit dem deserlializing davon, aber könnte es schlussendlich beheben und erfolgreich auslesen und darstellen. Die CreateGame Methode hat jetzt auch noch die zusätzliche Parameter, die ich nur auswählen könnte indem ich die Factions und Playstyles anzeige und dann eine Option zur Auswahl gebe. Mit diese Features, die ich jetzt hinzugefügt habe wird es immer wichtiger mehr exception handling und Inputverarbeitung zu machen, da es oft eingaben gibt die case-sensitive sind (ZB Factions) und eingaben die nur 'int' akzeptieren, und keine verarbeitung haben für strings. Da es jetzt aber wichtiger ist zuerst die vollständige Kernfunktionalität zu haben mache ich das erst beim "polieren".


## 7.3: Kern-Funktionalität
- [x] DBManager class mit InitalizeDB, DBInsert methoden.
- [x] SQLite gameSave Database erstellen
- [x] SQLite gameSave zum Debuggen lesen (debug class und methode)
- [ ] (Kleiner) ItemList (warscheinlich in JSON) mit starter items, items class und Function welche starter items bestimmt (Item.cs, CreateInventory)

Heute habe ich SQLite in mein programm implementiert. Ich begann damit, unter der "systems" folder ein neues DBManager file und class. Da drinnen habe ich ein InitDB und eine DBInsert methode gemacht. Das InitDB erstellt und initialisiert eine neue Datenbank für das Spiel. Ich bin noch nicht genau sicher (darum muss ich das nächste Mal wieder ein Modell machen und ein bisschen nachdenken) ob ich statische Sachen wie Items usw in eine statische separate Database mache und dann game spezifische Datenbanken erstelle oder einfach eine statische und eine dynamische Datenbank(und wir dann einfach mit ein GameID schaffen und alles zusammen in der dynamische setzen) Das DBInsert fügt Daten in die Datenbank ein, es nimmt als Parameter eine Tabellenname und dann ein Dictionary mit Keys und Values. Ich weiss noch nicht ob ich das ändern sollte für grössere Inserts.
Danach habe ich auch ein debug folder erstellt mit der Klasse "Debug" drinnen. Dort ist die "SelectAll" Methode und auch noch das "TestJSON", welches vorher im Factionloader war. Ich habe mich entschieden das so zu machen da es zu oft sehr komische Probleme mit der Datenbank hatte und ich in der zukunft auch mehr JSON files hinzufügen wird welche viel grösser sind, und dann viel einfach corrupted werden.

☝️ Vergessen Sie nicht, bis einen ersten Code auf github hochzuladen

## 14.3: Architektur ausbauen
- [ ] Hinzufügen in InitDB: Erstellung Tabellen "World", "NPC"
- [ ] Datenmodell für DB (UML und future proof)
- [ ] (Kleiner) ItemList (warscheinlich in JSON) mit starter items, items class und Function welche starter items bestimmt (Item.cs, CreateInventory)
- [ ] CreateInventoryMethode

      


## 21.3: Architektur ausbauen
- [ ] Generation von Locations in World.cs
## 28.3: Auspolieren
## 4.4: Auspolieren & Abschluss
