using ASD.Graph;
using System.Collections.Generic;

namespace lab7
{
    public static class GardenPlannerExtender
    {

        /// <summary>
        /// Znajduje wszystkie optymalne ustawienia sza³asów w ogrodzie opisanym przez graf.
        /// </summary>
        /// <param name="graph">Graf reprezentuj¹cy ogród</param>
        /// <returns>Lista rozwi¹zañ. Ka¿de rozwi¹zanie to tablica zawieraj¹ca numery
        /// wierzcho³ków, w których stoj¹ sza³azy. Ka¿dy numej mo¿e wyst¹piæ co najwy¿ej raz.</returns>
        public static List<int[]> GardenPlanner(this IGraph graph)
        {
            var gph = new GardenPlannerHelper(graph);
            // wywo³aj metodê Plan dla obiektu gph
            // zwróæ wynik
            return new List<int[]>();
        }

        private sealed class GardenPlannerHelper
        {
            private IGraph graph;
            //
            // Tu mo¿esz dopisaæ deklaracje potrzebnych sk³adowych
            //

            internal GardenPlannerHelper(IGraph _graph)
            {
                graph = _graph;
                //
                // Tu mo¿esz dopisaæ potrzebne inicjalizacje
                //
            }

            internal void Plan(/* tu mozesz dopisaæ potrzebne parametry, typ wyniku te¿ mozesz zmieniæ */)
            {
                //
                //  Tu implementacja algorytmu z powrotami
                //
            }

            //
            //  Tu mo¿esz dopisaæ potrzebne metody pomocnicze
            //

            }

        }

    }



