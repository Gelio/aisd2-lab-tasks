namespace GameElimination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        public Team(int id, string name, int numberOfWins, int[] numberOfGamesToPlayByTeam)
        {
            this.Id = id;
            this.Name = name;
            this.NumberOfWins = numberOfWins;
            this.NumberOfGamesToPlayByTeam = numberOfGamesToPlayByTeam;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int NumberOfWins { get; private set; }

        private int? numberOfGamesToPlay;
        public int NumberOfGamesToPlay
        {
            get
            {
                if (!this.numberOfGamesToPlay.HasValue)
                {
                    this.numberOfGamesToPlay = this.NumberOfGamesToPlayByTeam.Sum();
                }

                return this.numberOfGamesToPlay.Value;
            }
        }

        public int[] NumberOfGamesToPlayByTeam { get; private set; }
    }

    public partial class Program
    {
        public static void Main()
        {
            var teams = GetTeams();

            var expectedResults = new[] { false, false, false, false, false, false, true, false, true, true, true, false, false, false, true, false, true, false, true, true, true, false, true, false, false };

            int index = 0;
            foreach (var team in teams)
            {
                for (int teamId = 0; teamId < 5; teamId++)
                {
                    int[,] predictedResults;

                    var result = IsTeamEliminated(teamId, team, out predictedResults);

                    if (!result)
                    {
                        Console.WriteLine("Drużyna: {0} nadal może wygrać", team[teamId].Name);

                        // wypisywanie wyników
                        //Console.WriteLine("Następujący rozkład to gwarantuje:");

                        //for (int i = 0; i < team.Length; i++)
                        //{
                        //    for (int j = i + 1; j < team.Length; j++)
                        //    {
                        //        Console.WriteLine();
                        //        Console.WriteLine(
                        //            "Drużyna {0} wygrywa z {1} - {2} razy",
                        //            team[i].Name,
                        //            team[j].Name,
                        //            predictedResults[i, j]);
                        //        Console.WriteLine(
                        //            "Drużyna {0} wygrywa z {1} - {2} razy",
                        //            team[j].Name,
                        //            team[i].Name,
                        //            predictedResults[j, i]);
                        //    }
                        //}
                    }
                    else
                    {
                        Console.WriteLine("Drużyna: {0} nie może już wygrać", team[teamId].Name);
                    }

                    Console.WriteLine(result == expectedResults[index++] ? "TEST OK" : "TEST FAILED!!!");
                    Console.WriteLine(VerifyPredictedResults(teamId, team, predictedResults) ? "Predicted Results OK" : "Prdicted Results test failed!!!");

                }
                Console.WriteLine();
            }

        }

        private static IEnumerable<Team[]> GetTeams()
        {
            var teams = new List<Team[]>();

            var set = new Team[5];
            set[0] = new Team(0, "Legia", 75, new[] { 0, 3, 8, 7, 3 });
            set[1] = new Team(1, "Polonia", 71, new[] { 3, 0, 2, 7, 4 });
            set[2] = new Team(2, "Lech", 75, new[] { 8, 2, 0, 0, 0 });
            set[3] = new Team(3, "Ruch", 75, new[] { 7, 7, 0, 0, 0 });
            set[4] = new Team(4, "Wisła", 75, new[] { 3, 4, 0, 0, 0 });

            teams.Add(set);

            set = new Team[10];
            set[0] = new Team(0, "1", 75, new[] { 0, 3, 8, 7, 3, 5, 3, 5, 2, 4 });
            set[1] = new Team(1, "2", 31, new[] { 3, 0, 2, 7, 4, 6, 8, 3, 4, 2 });
            set[2] = new Team(2, "3", 75, new[] { 8, 2, 0, 0, 0, 7, 3, 4, 1, 8 });
            set[3] = new Team(3, "4", 25, new[] { 7, 7, 0, 0, 0, 3, 4, 7, 5, 1 });
            set[4] = new Team(4, "5", 12, new[] { 3, 4, 0, 0, 0, 2, 7, 3, 5, 1 });
            set[5] = new Team(5, "6", 40, new[] { 5, 6, 7, 3, 2, 0, 1, 5, 3, 7 });
            set[6] = new Team(6, "7", 38, new[] { 3, 8, 3, 4, 7, 1, 0, 3, 4, 6 });
            set[7] = new Team(7, "8", 67, new[] { 5, 3, 4, 7, 3, 5, 3, 0, 7, 2 });
            set[8] = new Team(8, "9", 81, new[] { 2, 4, 1, 5, 5, 3, 4, 7, 0, 2 });
            set[9] = new Team(9, "10", 30, new[] { 4, 2, 8, 1, 1, 7, 6, 2, 2, 0 });

            teams.Add(set);

            var r = new Random(1000);

            teams.Add(GenerateSet(r, 10));
            teams.Add(GenerateSet(r, 20));
            teams.Add(GenerateSet(r, 25));


            return teams;
        }

        private static Team[] GenerateSet(Random r, int count)
        {
            var set = new Team[count];
            for (int i = 0; i < set.Length; i++)
            {
                var games = set
                    .Take(i)
                    .Select(x => x.NumberOfGamesToPlayByTeam[i])
                    .Concat(Enumerable.Repeat(0, count - i).Select(x => r.Next(20)))
                    .ToArray();

                games[i] = 0;

                set[i] = new Team(i, i.ToString(), r.Next(20, count * 20), games);
            }

            return set;
        }

        private static bool VerifyPredictedResults(int teamId, Team[] teams, int[,] predictedResults)
        {
            if (predictedResults == null)
            {
                return true;
            }

            int[] numberOfGamesWon = new int[teams.Length];
            for (int i = 0; i < predictedResults.GetLength(0); i++)
            {
                numberOfGamesWon[i] = teams[i].NumberOfWins;
                for (int j = 0; j < predictedResults.GetLength(1); j++)
                {
                    numberOfGamesWon[i] += predictedResults[i, j];

                    if (predictedResults[i, j] + predictedResults[j, i] > teams[i].NumberOfGamesToPlayByTeam[j])
                    {
                        return false;
                    }
                }
            }

            return numberOfGamesWon.Max() == numberOfGamesWon[teamId];
        }
    }
}
