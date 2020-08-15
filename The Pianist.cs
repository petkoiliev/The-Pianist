using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ThePianist
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Dictionary<string, string>> pieces = new Dictionary<string, Dictionary<string, string>>();


            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();

                var tokens = input.Split("|", StringSplitOptions.RemoveEmptyEntries);
                var piece = tokens[0];
                var composer = tokens[1];
                var key = tokens[2];
                if (!pieces.ContainsKey(piece))
                {
                    pieces.Add(piece, new Dictionary<string, string>() {
                { "composer",composer},
                { "key",key}
                    });
                }
               
            }         
            string command = Console.ReadLine();
            while (command != "Stop")
            {
                var tokens = command.Split("|", StringSplitOptions.RemoveEmptyEntries);
                var toDo = tokens[0];
                if (toDo=="Add")
                {
                    var piece = tokens[1];
                    var composer = tokens[2];
                    var key = tokens[3];
                    if (pieces.ContainsKey(piece))
                    {
                        Console.WriteLine($"{piece} is already in the collection!");
                    }
                    else
                    {
                        pieces.Add(piece, new Dictionary<string, string>() {
                        { "composer",composer},
                        { "key",key}
                    });
                        Console.WriteLine($"{piece} by {composer} in {key} added to the collection!");
                    }
                }

                else if (toDo=="Remove")
                {
                    var pieceToRemove = tokens[1];
                    if (pieces.ContainsKey(pieceToRemove))
                    {
                        pieces.Remove(pieceToRemove);
                        Console.WriteLine($"Successfully removed {pieceToRemove}!");

                    }
                    else
                    {
                        Console.WriteLine($"Invalid operation! {pieceToRemove} does not exist in the collection.");
                    }

                }

                else if (toDo=="ChangeKey")
                {

                    var piece = tokens[1];
                    var newKey = tokens[2];

                    if (pieces.ContainsKey(piece))
                    {
                        pieces[piece]["key"] = newKey;
                        Console.WriteLine($"Changed the key of {piece} to {newKey}!");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid operation! {piece} does not exist in the collection.");
                    }

                }

                command = Console.ReadLine();
            }

            var ordered = pieces.OrderBy(x=>x.Key).ThenBy(x=>x.Value["composer"]);

            foreach (var piece in ordered)
            {
                Console.WriteLine($"{piece.Key} -> Composer: {piece.Value["composer"]}, Key: {piece.Value["key"]}");
            }
           
        }
    }
}
