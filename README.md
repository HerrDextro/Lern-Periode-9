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
- [x] Statische DB mit Itemlist
- [x] Tables Hinzufügen in DB, InitDB Methode ausbreiten um mehrere Tabellen zu erstellen
- [ ] CreateInventoryMethode
- [ ] Generation von Locations in World.cs

Heute habe ich das Datenmodell komplett verändert, da es vorher eigentlich kein Sinn gemacht hat und jetzt ist es so ausgedacht, dass es völlig strukturiert, normalisiert und future proof ist. Es werden warscheinlich doch ein Paar Änderungen kommen, aber nicht sehr grosse. Ich habe entschieden es so zu machen, dass alle Items welche im World existieren in ihre entsprechende Items Tabelle sind (es gibt mehrere damit die Item attribute einigermasse gleich sind für alle Items, und es so nicht viele NULL cells gibt) und nur die Parameter besitzen, welche im Spiel verändert werden können. Auch habe ich im Debug.cs im Switch case eine neue Option namens "SQLCreateDB" gemacht damit ich die statische DB erstellen kann. Vordass ich das gemacht habe, habe ich der DB in SQL Server gemacht und schon mal ein basic Itemlist eingefügt um es ein bisschen zu testen und meine Ideen für JOIN Abfragen (welche das C# Programm nacher machen wird) auszuprobieren. Ich habe mir auch das Damage System ausgedacht, da ich die Attribute dafür schon einfügen möchte. Ich habe aber jetzt zum testen einfach ein "simpleDMG".

ADD LATER: knife damage coeff thoughts, createinv method based on playtstyle

<img src="Screenshot 2025-03-21 100934.png" width="480">

Es muss schon darauf geachtet werden dass MS SQl und SQLite nicht genau gleich sind und deswegen das Datenmodell nicht 100% passen wird, und das Test DB welches im MS SQL erstellt wurde also auch keine exakte kopie ist. Sobald es komplett richtig funktioniert im SQL Server wird ich es umschreiben für SQLite (double > real etc)

## 26.3:

Heute schreibe ich ein kleiner Beitrag: Ich hatte sehr viele Probleme mit der SQLite implementation, obwohl es am Anfang alles super funktioniert hatte habe ich es ziemlich schnell alles gebrochen. Es hat mir einige Zeit gedauert es zu fixen, da ich mir kein ChatGPT erlaubt habe. Das ist weil ich wirklich verstehen muss wie jedes einzelnes Ding für SQLite funktioniert. Es funktioniert jetzt (und jetzt noch) alles wieder super, aber ich kann ganz ehrlich nicht sagen, dass ich mit dem Block "Architektur" fertig bin. Deswegen mache ich heute und morgen nochmals ein paar extra Architekturblöcke, wo ich die basisfunktion überall schaffe, und die CreateInventory und das Location-generation alles einfach funktioniert, obwohl es einpach nur wenige testItems/testLocations sein werden...

## 28.3: Auspolieren
- [ ] Inputverarbeitung bei "Console.ReadLine();" mit exception handling und die möglichkeit, nochmals ein Input zu geben
- [x] [dbName].db erstellen funktioniert immer, geht aber manchmal nicht im resources Folder (obwohl es so angegeben wird von SQLite??) sondern im bin > debug > net8.0
- [x] Console.Clear(); damit nicht die alte Outputs/inputs immer sichtbar sind

Heute habe ich:
- InvConfig.json erstellt (noch nicht in CreateInventory angewendet)
- Classes für das neue JSON (für deserializing)
- In Faction.cs ein modulärer JSON Deserializer gemacht um der "Proprietäre" von GameOptions.json zu ersetzen, es verwendet als return type ein dynamisches (generic <T>) um mehrere json files zu ünterstützen, es funktioniert aber noch nicht ganz...
- Die DB inserts fertig fur CreateInventory
- Das komische DB in falsche Speicherort-Problem habe ich gelöst, indem ich die SetDatabasePath methode verbessert habe

NOTE: Ich bin in Debug.cs und faction.cs dran, die JSONDeserializers funktionierend zu kriegen und muss die richtige class und type struktur herausfinden


## 4.4: Auspolieren & Abschluss
- [ ] relative paths für resources
- [x] CreateInventoryMethode verbunden mit invConfig.json

# Reflexion 04.04.2024

In diese Lernperiode wollte ich darauf fokussieren, mein Gedankengang beim programmieren zu verbessern. Was ich damit meine: Die Übertragung von Logik aus mein Gehirn zum Computer, also das programmieren, geht ziemlich gut obwohl es manchmal lange dauert, eine Lösung für ein problem zu finden. Ich habe aber am meisten Mühe mit der Prorgammarchitektur und entscheidungen treffen, wie etwas funktionieren wird. Ich habe mich deswegen dafür entschieden, ein Game-Backend zu machen, da ich hier viele entscheidungen treffen muss über wie Daten fliessen, gespeichert werden, wie die Datenbank strukturiert werden muss und dann zu entscheiden, welches von den 300 mögliche Lösungen ich wählen wurde. Meine Vorgehensweise war ein bisschen unkonventionell, da ich vom Boden auf geschafft habe und immer sehr weit vorausgedacht habe. Normalerweise hätte ich mit so etwas zuerst mal eine kleine Testdatenbank erstelt, dann ein Tool um Daten abzurufen und zu schreiben. Dieses mal ging ich so vor: 
- Ich mache ein "Game"
- Was braucht ein "Game"
- Zuerst: loading screen (Create Game, Load Game, Settings)
- Um ein Game zu haben, muss man den erstellen können (CreateNewGame Methode)
- Was braucht ein sehr simples Game alles? (Gammename(für LoadGame), Player Name, Difficulty, und Faction und Playstyle da das zu mein Game-style passt)
- Wie speichere ich das alles? -> SQLite & JSON für descriptions and configs)
- Erstelle Test-DB mit "Player" Tabelle mit die oben genannten Parameter
- Teste alles
- Erstelle grosses DB

Jetzt ist die Zeit vorbei, aber das DB funktioniert, was mein Ziel war. Ich habe gelernt, wie ich nachdenken kann, und wie ich Lösungen für Probleme finden kann. Auch habe ich, was ich zwar nicht hier im Repo habe, es geübt Optionenauswertungen zu machen für die verschiedene Lösungen (ZB: Was nutze ich für die config files? JSON, INI?)
Daneben müsste ich mein OOP verständnis verbessern, da ich classes meistens nur also container for Methodengruppen verwendet habe, und sie jetzt als Objecte verwenden müsste weil ich nur so das JSON deserializen kann.
