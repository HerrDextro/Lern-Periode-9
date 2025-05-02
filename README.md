# Lern-Periode 9
23.4 bis 25.6.2024

Grob-Planung
Aktuell läuft es in der Schule sehr gut für mich. Ich habe einen 5er-Schnitt, der sich wahrscheinlich noch verbessern wird, und ein paar Fächer, in denen ich im letzten Semester mein Potenzial nicht ausgeschöpft habe.
Dieses Jahr war ich in allen Modulen etwa gleich stark. In Informatik habe ich leider nur einen 5er-Schnitt, da mir ein paar Fehler unterlaufen sind.
Für dieses Modul wäre ein Projekt sinnvoll, bei dem man viel mit der Strukturierung verschiedener Objekte und der Speicherung von Daten arbeitet. Ich wollte ursprünglich unseren SmartPainter mit WinForms Graphics Box nachbauen, habe mich aber diese Woche dagegen entschieden, da Alex ihn bereits in C++ umsetzt – und ich nicht zwingend eine noch schnellere Version benötige.

14.02.2025: Explorative Wegwerf-Prototyp
 WinForms Graphics Box

 60 FPS

Heute habe ich mit viel Hilfe von ChatGPT eine PictureBox in WinForms erstellt, die einzelne Pixel (Farbe, X, Y) individuell aktualisieren kann. Der Code wurde vollständig von der KI bereinigt, aber gelegentlich tritt noch eine „object already in use“-Exception auf. Diese passiert jedoch nicht oft genug, um sie gezielt zu debuggen.

21.2: Explorative Wegwerf-Prototyp
 Klassen-/Datenstruktur festlegen

 Minimale Variablen festlegen

 Hauptmenü

 CreateGame (nur Schwierigkeitsgrad, Name)

Heute habe ich auf Papier eine Programmarchitektur für ein Videospiel entworfen. Es ist alles noch abstrakt, also gibt es keinen echten Game.Render oder Ähnliches – es dient nur der Struktur. Ich habe festgelegt, welche Variablen wo verwendet werden, wie sie fließen und wie sie am besten gespeichert bzw. bearbeitet werden könnten.
Dann habe ich ein sehr simples Proof-of-Concept-Wegwerfprogramm erstellt, das nur zwei Optionen zum Erstellen eines Spiels bietet und drei Datenpunkte als gameData speichert. Die Struktur hat jedoch Sinn ergeben, und ich habe die Architektur für den Prototyp mit Kernfunktionalität leicht erweitert.
Da ich oft vergesse zu committen, habe ich Google Tasks auf mein Handy heruntergeladen und einen wiederkehrenden Task eingestellt, der mich daran erinnert.

28.2: Kernfunktionalität
 Dateien für vollständige Programmarchitektur hinzufügen

 JSON-Datei für Fraktionen und Spielstile inkl. zugehöriger Klasse

 Anzeige der Auswahlmöglichkeiten (inkl. Beschreibung) & Erweiterung von CreateGame mit Fraktion, Spielstil und Spielname

 SaveGame mit JSON-Datei (nicht umgesetzt wegen Umstieg auf SQLite, was Architekturänderung erfordert)
Optional:

Heute habe ich die Wegwerfversion so umgebaut, dass sie nun vollständig dem Modell entspricht (nicht mehr mit weniger Features wie zuvor). Statt einer .txt-Datei wird nun eine JSON-Datei verwendet, die die Beschreibungen enthält. Ich hatte anfangs Probleme beim Deserialisieren, konnte diese aber schlussendlich beheben und die Daten erfolgreich auslesen und anzeigen.
Die CreateGame-Methode besitzt nun zusätzliche Parameter, die durch Anzeige der Fraktionen und Spielstile auswählbar sind.
Mit diesen neuen Features wird es immer wichtiger, mehr Exception Handling und Eingabeverarbeitung umzusetzen, da es oft case-sensitive Eingaben (z. B. Fraktionen) oder Eingaben gibt, die nur int erlauben und Strings nicht verarbeiten können.
Da momentan jedoch die vollständige Kernfunktionalität im Fokus steht, mache ich das erst beim späteren „Polieren“.

7.3: Kernfunktionalität
 DBManager-Klasse mit InitializeDB, DBInsert-Methoden

 SQLite gameSave-Datenbank erstellen

 gameSave zu Debugzwecken auslesen (Debug-Klasse und Methode)

 (Kleine) ItemList (wahrscheinlich in JSON) mit Start-Items, Item-Klasse und Funktion zur Start-Item-Bestimmung (Item.cs, CreateInventory)

Heute habe ich SQLite in mein Programm integriert. Ich begann damit, unter dem „systems“-Ordner eine neue Datei und Klasse DBManager zu erstellen. Darin habe ich InitDB und DBInsert implementiert. InitDB erstellt und initialisiert eine neue Spiel-Datenbank.
Ich bin mir noch nicht sicher (daher werde ich das nächste Mal wieder ein Modell zeichnen und nachdenken), ob ich statische Inhalte wie Items usw. in eine separate, statische Datenbank packe und dann spielspezifische Datenbanken erstelle – oder ob ich einfach eine statische und eine dynamische DB verwende und mit einer GameID alles in die dynamische DB schreibe.
DBInsert fügt Daten in eine angegebene Tabelle ein, indem es ein Dictionary mit Key-Value-Paaren übergibt. Ich überlege, ob ich das für größere Datenmengen ändern muss.
Außerdem habe ich einen „debug“-Ordner mit der Klasse Debug erstellt, in der sich die Methode SelectAll befindet sowie TestJSON, das vorher im FactionLoader war. Ich habe das ausgelagert, da es häufiger merkwürdige DB-Probleme gab und ich künftig mehrere JSON-Dateien einfügen möchte, die potenziell groß und leicht fehleranfällig sind.
Zusätzlich habe ich meine APs für heute leicht angepasst. Grund: Die SQLite-Integration war sehr problematisch und zeitaufwendig, und die ursprünglichen APs ergaben wenig Sinn – da erst noch andere Systeme nötig gewesen wären. Außerdem ging es eher um Architekturaufbau als um Kernfunktionalität.

☝️ Nicht vergessen: ersten Code auf GitHub hochladen!

14.3: Architektur ausbauen
 InitDB aufteilen in InitDB, CreateDB und Auswahl der richtigen DB (basierend auf GameName)

 In InitDB: Erstellung der Tabellen „World“ und „NPC“

 Datenmodell für DB (UML & zukunftssicher)

 (Kleine) ItemList (wahrscheinlich in JSON) mit Start-Items, Item-Klasse und Funktion zur Item-Bestimmung (Item.cs, CreateInventory)

 CreateInventory-Methode

Heute habe ich zuerst InitDB repariert. Es wird jetzt wirklich eine neue Datenbank beim Erstellen eines Spiels erzeugt, statt nur neue Tabellen – das führte nämlich zu Konflikten (z. B. „Table Player already exists“).
Ich habe ein Datenmodell erstellt, da ich sonst schnell den Überblick verliere. Außerdem habe ich eine grundlegende ItemList erstellt mit allen Items, die ich für die CreateInventory-Methode brauche. Es sind viele, da ich Variation für verschiedene Spielstile benötige (z. B. bekommt ein „Bandit Raider“ schlechtere Ausrüstung als ein „Elite Commander“).
Ich habe die Struktur der DB so festgelegt, dass Tabellen möglichst spezialisiert sind, um Items einzubinden. (Diese brauchen noch Parameter – die muss ich noch definieren.)
Die CreateInventory-Methode habe ich nicht implementiert, da ich mit der DB-Struktur noch nicht fertig bin (nicht committed, da sie aktuell nicht funktioniert). Das Ausdenken hat lange gedauert.

Ich habe außerdem ein Bild des neuen DB-Systems und ein UML-Diagramm eingefügt, damit ich sie später hier anschauen kann.
<img src="https://github.com/user-attachments/assets/44a6f29c-bcc4-48df-88da-b8ba154ade2e" width="480">
<img src="Screenshot 2025-03-14 114955.png" width="480">
Hinweis: Dinge wie „ammo types“ werden nicht sofort in der DB gespeichert, wenn sie im Spiel geändert werden (z. B. Shotgun: Buckshot → Slug). Sie werden im Arbeitsspeicher verwaltet und erst beim GameSave persistiert.
Weitere Bitte: Ich möchte gerne nur 3 APs machen (statt 4), diese aber besser ausformuliert und auf die gesamte ILA-Stunde abgestimmt.
