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
Auch noch: Ich habe meine AP's für heute ein bisschen verändert. Der Grund dafür ist es dass das SQLite viele Probleme hatte und sehr lange gebraucht hat, und die AP's auch kein Sinn machten, da ich erst eigentlich andere Systeme brauche. Zusätzlich waren die AP's eher "Architektur ausbauen" statt "Kernfunktionalität"

☝️ Vergessen Sie nicht, bis einen ersten Code auf github hochzuladen

## 14.3: Architektur ausbauen
- [x] InitDB splitten in InitDB, CreateDB, und richtige DB verwenden (nach GameName)
- [x] Hinzufügen in InitDB: Erstellung Tabellen "World", "NPC"
- [x] Datenmodell für DB (UML und future proof)
- [x] (Kleiner) ItemList (warscheinlich in JSON) mit starter items, items class und Function welche starter items bestimmt (Item.cs, CreateInventory)
- [ ] CreateInventoryMethode

Heute habe ich zuerst das InitDB repariert indem es jetzt auch wirklich eine neue Datenbank erstellt beim CreateGame statt einfach neue Tabellen (führt zu Konflikte, da table "Player schon existiert)) und ich habe ein Datenmodell erstellt, weil ich sonst relativ schnell verwirrt werde. Auch habe ich ein basic Itemlist erstellt mit alle Items die benötigt sind für die CreateInventory-Methode, es sind so viele weil ich Variation brauche, um verschiedene Items zu haben für verschiedene Playstyles (zb ein Bandit Raider bekommt schlechte Sachen und ein Elite Commander das beste was es gibt.) Ich habe heute festgelegt, wie die DB jetzt strukturiert werden sollte um Tabellen so spezialisiert möglich zu machen damit ich die Items reinmachen kann. (Die Items brauchen noch parameter, ich muss die noch irgendwie erstellen.) Ich habe die CreateInventory nicht gemacht da ich mit dem Erstellen des DB mit der richtige Struktur nicht fertig bin(das nicht committed, da es komplett broken ist), es hat ziemlich lange gedauert das alles auszudenken.

Ich habe noch ein Bild von das neue DB system und auch eines des Datenmodell eingefügt damit ich sie später hier anschauen kann.
<img src="https://github.com/user-attachments/assets/44a6f29c-bcc4-48df-88da-b8ba154ade2e" width="480">
<img src="Screenshot 2025-03-14 114955.png" width="480">
NOTE: Sachen wie "ammo types werden nicht sofort in der DB gespeichert sobald sie in-game gewechselt werden (ZB: Shotgun allowed ammo types: buckshot, slug, dragons breath, in game wird shotgun von buckshot zu slug gewechselt) aber sie werden im in-memory gamestate verwaltet und nur gespeichert OnGameSave.
AUCH NOTE: ich möchte gerne (bitte) 3 AP's machen statt 4, aber dafür wird ich sie viel besser ausformulieren, und sie werden die ganze ILA Stunden dauern.



## 21.3: Architektur ausbauen
- [ ] Statische DB mit Itemlist
- [ ] Tables Hinzufügen in DB, InitDB Methode ausbreiten um mehrere Tabellen zu erstellen
- [ ] CreateInventoryMethode
- [ ] Generation von Locations in World.cs

Heute habe ich das Datenmodell komplett verändert, da es vorher eigentlich kein Sinn gemacht hat und jetzt ist es so ausgedacht, dass es völlig strukturiert, normalisiert und future proof ist. Es werden warscheinlich doch ein Paar Änderungen kommen, aber nicht sehr grosse. Ich habe entschieden es so zu machen, dass alle Items welche im World existieren in ihre entsprechende Items Tabelle sind (es gibt mehrere damit die Item attribute einigermasse gleich sind für alle Items, und es so nicht viele NULL cells gibt) und nur die Parameter besitzen, welche im Spiel verändert werden können.

ADD LATER: knife damage coeff thoughts, createinv method based on playtstyle

<img src="Screenshot 2025-03-21 100934.png" width="480">

Es muss schon darauf geachtet werden dass MS SQl und SQLite nicht genau gleich sind und deswegen das Datenmodell nicht 100% passen wird, und das Test DB welches im MS SQL erstellt wurde also auch keine exakte kopie ist. Sobald es komplett richtig funktioniert im SQL Server wird ich es umschreiben für SQLite (double > real etc)

## 28.3: Auspolieren
## 4.4: Auspolieren & Abschluss
