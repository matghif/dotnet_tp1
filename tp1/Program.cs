var layout = """
+--+--------+
| /  |      |
| +--+--+   |
|    |k     |
+--+ | +--+ |
   |k       |
+ +-------/ |
|           |
+-----------+
""";
var labyrinth = new TP1.Labyrinth(layout);

Console.WriteLine($"Labyrinthe de l’atelier ({labyrinth.Width} x {labyrinth.Height})");
Console.WriteLine(labyrinth.Render());
Console.WriteLine("Légende : - mur horizontal, | mur vertical, + angle de mur, / porte verrouillée, k clé, espace salle vide.");
