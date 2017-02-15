namespace GameElimination
{
    using System;

    using ASD.Graph;

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
            predictedResults = null;
            
            return true;
        }
    }
}
