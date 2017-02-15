using ASD.Graph;
using System.Collections.Generic;

namespace lab7
{
    public static class GardenPlannerExtender
    {

        /// <summary>
        /// Znajduje wszystkie optymalne ustawienia sza�as�w w ogrodzie opisanym przez graf.
        /// </summary>
        /// <param name="graph">Graf reprezentuj�cy ogr�d</param>
        /// <returns>Lista rozwi�za�. Ka�de rozwi�zanie to tablica zawieraj�ca numery
        /// wierzcho�k�w, w kt�rych stoj� sza�azy. Ka�dy numej mo�e wyst�pi� co najwy�ej raz.</returns>
        public static List<int[]> GardenPlanner(this IGraph graph)
        {
            var gph = new GardenPlannerHelper(graph);
            // wywo�aj metod� Plan dla obiektu gph
            // zwr�� wynik
            return new List<int[]>();
        }

        private sealed class GardenPlannerHelper
        {
            private IGraph graph;
            //
            // Tu mo�esz dopisa� deklaracje potrzebnych sk�adowych
            //

            internal GardenPlannerHelper(IGraph _graph)
            {
                graph = _graph;
                //
                // Tu mo�esz dopisa� potrzebne inicjalizacje
                //
            }

            internal void Plan(/* tu mozesz dopisa� potrzebne parametry, typ wyniku te� mozesz zmieni� */)
            {
                //
                //  Tu implementacja algorytmu z powrotami
                //
            }

            //
            //  Tu mo�esz dopisa� potrzebne metody pomocnicze
            //

            }

        }

    }



