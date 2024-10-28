

## erster Algorithmus hat nicht gereicht:

Im Beispiel:
1951    2311    3079
2729    1427    2473
2971    1489    1171

Hat er rechts oben die 1427 platziert, was aber zu keiner Lösung führt.


## Lösungsansätze

- rekursiver BFS
- Voraussetzung: Puzzle State muss sauber kopierbar sein
  (eingesetzte Teile inkl Rotation, Übrige Teile)
- generell scheisse mit OOP, z.B. muss man immer drauf
  achten dass man geklonte Puzzleteile nicht aus Listen entfernen kann etc

- Brute Force
- alle kombinationen durchgehen, abbrechen sobald was nicht passt
  Effizienz vermutlich kacke, 
  das ist so als ob man beim Maze immer wieder von vorne losläuft
- sobald man in eine Sackgasse kommt, anstatt zu backtracken


