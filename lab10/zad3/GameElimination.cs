namespace GameElimination
{
    using System;

    using ASD.Graphs;

    public partial class Program
    {
        /// <summary>
        /// Procedura określająca czy drużyna jest wyeliminowana z rozgrywek
        /// </summary>
        /// <param name="teamId">indeks drużyny do sprawdzenia</param>
        /// <param name="teams">lista zespołów</param>
        /// <param name="predictedResults">wyniki gwarantujące zwycięstwo sprawdzanej drużyny</param>
        /// <returns></returns>
        public static bool IsTeamEliminated(int teamId, Team[] teams, out int[,] predictedResults)
        {
            int teamsCount = teams.Length,
                pairsCount = (teamsCount - 1) * (teamsCount - 2) / 2;

            // Vertices:
            // 0 - start, 1 - finish,
            // 2, ..., (2 + pairsCount - 1) - pairs
            // (2 + pairsCount), ..., (pairsCount + teamsCount) - team vertices
            Graph eliminationsGraph = new AdjacencyMatrixGraph(true, 2 + pairsCount + (teamsCount - 1));
            int i = 0;

            Team selectedTeam = null;
            int selectedTeamIndex = -1;
            for (i = 0; i < teamsCount; i++)
            {
                Team team = teams[i];
                if (team.Id == teamId)
                {
                    selectedTeam = team;
                    selectedTeamIndex = i;
                    break;
                }
            }

            // Team not found
            if (selectedTeam == null)
            {
                predictedResults = null;
                return true;
            }


            int[] teamVertices = new int[teamsCount - 1];
            i = 0;
            foreach (Team team in teams)
            {
                if (team.Id == teamId)
                    continue;

                // Add i -> t edge
                eliminationsGraph.AddEdge(2 + pairsCount + i, 1, Math.Max(selectedTeam.NumberOfWins + selectedTeam.NumberOfGamesToPlay - team.NumberOfWins, 0));
                teamVertices[i++] = team.Id;
            }

            i = 0;
            Tuple<int, int>[] pairVertices = new Tuple<int, int>[pairsCount];
            foreach (Team team1 in teams)
            {
                if (team1.Id == teamId)
                    continue;

                int team1Vertex = -1;
                for (int j = 0; j < teamsCount - 1; j++)
                {
                    if (teamVertices[j] == team1.Id)
                    {
                        team1Vertex = j;
                        break;
                    }
                }

                foreach (Team team2 in teams)
                {
                    if (team2.Id == teamId)
                        continue;
                    if (team2.Id <= team1.Id)
                        continue;

                    int team2Vertex = -1;
                    for (int j = 0; j < teamsCount - 1; j++)
                    {
                        if (teamVertices[j] == team2.Id)
                        {
                            team2Vertex = j;
                            break;
                        }
                    }

                    // Add s -> i-j edge
                    eliminationsGraph.AddEdge(0, 2 + i, team1.NumberOfGamesToPlayByTeam[team2Vertex]);

                    // Add i-j -> i and i-j -> j edges
                    eliminationsGraph.AddEdge(2 + i, 2 + pairsCount + team1Vertex, double.PositiveInfinity);
                    eliminationsGraph.AddEdge(2 + i, 2 + pairsCount + team2Vertex, double.PositiveInfinity);
                    pairVertices[i++] = new Tuple<int, int>(team1.Id, team2.Id);
                }
            }

            double flowValue = eliminationsGraph.FordFulkersonDinicMaxFlow(0, 1, out Graph flow, MaxFlowGraphExtender.BFPath);
            bool allSourceEdgesSaturated = true;
            foreach (Edge e in flow.OutEdges(0))
            {
                if (e.Weight < eliminationsGraph.GetEdgeWeight(0, e.To))
                {
                    allSourceEdgesSaturated = false;
                    break;
                }
            }

            if (!allSourceEdgesSaturated)
            {
                predictedResults = null;
                return true;
            }

            predictedResults = new int[teamsCount, teamsCount];

            // Compute predicted results for the selected team
            for (int otherTeamIndex = 0; otherTeamIndex < teamsCount; otherTeamIndex++)
            {
                if (otherTeamIndex == selectedTeamIndex)
                    continue;

                int otherTeamVertex = -1;

                for (int j = 0; j < teamsCount - 1; j++)
                {
                    if (teamVertices[j] == teams[otherTeamIndex].Id)
                    {
                        otherTeamVertex = j;
                        break;
                    }
                }

                otherTeamVertex += 2 + pairsCount;

                int matchesWon = (int)(eliminationsGraph.GetEdgeWeight(otherTeamVertex, 1) - flow.GetEdgeWeight(otherTeamVertex, 1)),
                    matchesLost = (int)selectedTeam.NumberOfGamesToPlayByTeam[otherTeamIndex] - matchesWon;

                predictedResults[selectedTeamIndex, otherTeamIndex] = matchesWon;
                predictedResults[otherTeamIndex, selectedTeamIndex] = matchesLost;
            }

            // Compute predicted results for all other teams
            for (i = 0; i < pairsCount; i++)
            {
                int team1Id = pairVertices[i].Item1,
                    team2Id = pairVertices[i].Item2;
                int team1Index = -1,
                    team2Index = -1;

                for (int j = 0; j < teamsCount; j++)
                {
                    if (teams[j].Id == team1Id)
                        team1Index = j;
                    else if (teams[j].Id == team2Id)
                        team2Index = j;

                    if (team1Index != -1 && team2Index != -1)
                        break;
                }

                int team1Vertex = -1,
                    team2Vertex = -1;
                for (int j = 0; j < teamsCount - 1; j++)
                {
                    if (teamVertices[j] == team1Id)
                        team1Vertex = j;
                    else if (teamVertices[j] == team2Id)
                        team2Vertex = j;

                    if (team1Vertex != -1 && team2Vertex != -1)
                        break;
                }

                predictedResults[team1Index, team2Index] = (int)flow.GetEdgeWeight(2 + i, 2 + pairsCount + team1Vertex);
                predictedResults[team2Index, team1Index] = (int)flow.GetEdgeWeight(2 + i, 2 + pairsCount + team2Vertex);
            }
            return false;
        }
    }
}
