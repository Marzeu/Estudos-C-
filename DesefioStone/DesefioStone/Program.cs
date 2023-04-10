using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

namespace DesefioStone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputFile = @"C:\temp\teste.txt";

            var lines = File.ReadAllLines(inputFile);

            int[,] matriz = new int[5, 5];
            int x = 0;

            List<string> map = new List<string>();
            //int n = 0;

            foreach (var line in lines)
            {
                for (int y = 0; y < 5; y++)
                {
                    matriz[x, y] = Convert.ToInt16(line.Split(" ")[y]);
                }
                x++;
                map.Add(line.Replace(" ", string.Empty));
            }

            var start = new Tile();
            start.Y = map.FindIndex(x => x.Contains("3"));
            start.X = map[start.Y].IndexOf("3");

            var finish = new Tile();
            finish.Y = map.FindIndex(x => x.Contains("4"));
            finish.X = map[finish.Y].IndexOf("4");

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option. 
                    var tile = checkTile;
                    Console.WriteLine("Retracing steps backwards...");
                    while (true)
                    {
                        Console.WriteLine($"{tile.X} : {tile.Y}");

                        if (map[tile.Y][tile.X] == '0')
                        {
                            var newMapRow = map[tile.Y].ToCharArray();
                            newMapRow[tile.X] = '*';
                            map[tile.Y] = new string(newMapRow);
                        }

                        tile = tile.Parent;

                        if (tile == null)
                        {
                            Console.WriteLine("Map looks like :");
                            //map.ForEach(x => Console.WriteLine(x));
                            Console.WriteLine("Done!");
                            return;
                        }  
                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(map, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }

                map.Clear();
                int[,] matriz2 = new int[5, 5];
                int[,] matrizAux = new int[5, 5];

                for (int xi = 0; xi < matriz.GetLength(0); xi++)
                {
                    string linha = "";
                    for (int yi = 0; yi < matriz.GetLength(1); yi++)
                    {
                        matriz2[xi, yi] = matriz[xi, yi] == 1 ? virarBranca(matriz, xi, yi) : virarVerde(matriz, xi, yi);
                        linha += matriz2[xi, yi].ToString();
                    }
                    map.Add(linha);
                }

                matrizAux = matriz;
                matriz = matriz2;
                matriz2 = matrizAux;
            }

            Console.WriteLine("No Path Found!");

















            //for (int xi = 0; xi < matriz.GetLength(0); xi++)
            //{
            //    for (int yi = 0; yi < matriz.GetLength(1); yi++)
            //    {
            //        matriz[xi, yi] = matriz[xi, yi] == 1 ? virarVerde(matriz, xi, yi) : virarBranca(matriz, xi, yi);
            //    }
            //}

            // Para esta fase, o modelo de propagação é o seguinte: branca 0, verde 1
            // As células brancas transformam-se em verdes, se possuírem número de células adjacentes verdes maior do que 1 e menor do que 5.Do contrário, permanecem brancas.
            // As células verdes permanecem verdes se possuírem número de células adjacentes verdes maior do que 3 e menor do que 6.Do contrário, transformam - se em brancas.
            // Duas células são consideradas adjacentes se possuem uma fronteira, seja na lateral, acima, abaixo ou diagonalmente.No exemplo abaixo, a célula branca no centro possui, portanto, 8 células brancas adjacentes.

            //interações
            //virando verde: As células brancas transformam-se em verdes, se possuírem número de células adjacentes verdes maior do que 1 e menor do que 5.Do contrário, permanecem brancas.

            //printMatrix(matriz);
        }


        private static List<string> printMatrix(int[,] matrix)
        {
            List<string> map = new List<string>();

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write(matrix[x, y]);
                    map.Append(matrix[x, y].ToString());
                }
                Console.WriteLine();
            }
            return map;
        }

        private static int virarVerde(int[,] matrix, int x, int y)
        {
            //As células brancas transformam-se em verdes, se possuírem número de células adjacentes verdes maior do que 1 e menor do que 5.Do contrário, permanecem brancas.
            int count = 0;

            if (matrix[x, y] == 3) return 3;
            if (matrix[x, y] == 4) return 4;

            if (matrix.GetLength(0) > x + 1)
            {
                if (matrix[x + 1, y] == 1) count++;
            }

            if (matrix.GetLength(0) > x + 1 && matrix.GetLength(1) > y + 1)
            {
                if (matrix[x + 1, y + 1] == 1) count++;
            }

            if (matrix.GetLength(0) > x + 1 && y != 0)
            {
                if (matrix[x + 1, y - 1] == 1) count++;
            }

            if (y != 0)
            {
                if (matrix[x, y - 1] == 1) count++;
            }

            if (matrix.GetLength(1) > y + 1)
            {
                if (matrix[x, y + 1] == 1) count++;
            }

            if (x != 0 && y != 0)
            {
                if (matrix[x - 1, y - 1] == 1) count++;
            }

            if (x != 0)
            {
                if (matrix[x - 1, y] == 1) count++;
            }

            if (x != 0 && matrix.GetLength(1) > y + 1)
            {
                if (matrix[x - 1, y + 1] == 1) count++;
            }

            return (count > 1 && count < 5) ? 1 : 0;
        }

        private static int virarBranca(int[,] matrix, int x, int y)
        {
            //As células verdes permanecem verdes se possuírem número de células adjacentes verdes maior do que 3 e menor do que 6.Do contrário, transformam - se em brancas.
            int count = 0;

            if (matrix[x, y] == 3) return 3;
            if (matrix[x, y] == 4) return 4;

            if (matrix.GetLength(0) > x + 1)
            {
                if (matrix[x + 1, y] == 1) count++;
            }

            if (matrix.GetLength(0) > x + 1 && matrix.GetLength(1) > y + 1)
            {
                if (matrix[x + 1, y + 1] == 1) count++;
            }

            if (matrix.GetLength(0) > x + 1 && y != 0)
            {
                if (matrix[x + 1, y - 1] == 1) count++;
            }

            if (y != 0)
            {
                if (matrix[x, y - 1] == 1) count++;
            }

            if (matrix.GetLength(1) > y + 1)
            {
                if (matrix[x, y + 1] == 1) count++;
            }

            if (x != 0 && y != 0)
            {
                if (matrix[x - 1, y - 1] == 1) count++;
            }

            if (x != 0)
            {
                if (matrix[x - 1, y] == 1) count++;
            }

            if (x != 0 && matrix.GetLength(1) > y + 1)
            {
                if (matrix[x - 1, y + 1] == 1) count++;
            }

            return (count > 3 && count < 6) ? 1 : 0;
        }

        private static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => map[tile.Y][tile.X] == '0' || map[tile.Y][tile.X] == '4')
                    .ToList();
        }
    }
}
