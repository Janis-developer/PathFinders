using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{

    /// <summary>
    /// Find the shortest path between two given points in an unweighted graph.
    /// I keep the program simple, no exception checking, no extra classses,
    /// the point is to demonstrate the algorithm clearly
    /// </summary>
    class PathFinder
    {
        static Dictionary<string, IEnumerable<string>> graph = new Dictionary<string, IEnumerable<string>>
        {
            { "a", new List<string> {"z", "q", "s"} },
                { "z", new List<string> {"a", "w", "o"} },
                { "q", new List<string> {"s", "a"} },
                { "s", new List<string> {"a", "q", "o", "f"} },
                    { "w", new List<string> {"z", "p"} },
                    { "o", new List<string> {"f", "s", "z"} },
                    { "f", new List<string> {"s", "e"} },
                       { "e", new List<string> {"f", "p", "r"} },
//                  { "f", new List<string> {"s"} },
//                      { "e", new List<string> {"p", "r"} },
                        { "p", new List<string> {"w", "e", "r"} },
                            { "r", new List<string> {"e", "p"} },
                        { "notconnected", new List<string> {} },
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, find the shortest path between two given points in a graph!");

            ShowMeTheWay();

            Console.ReadKey();
        }

        static void ShowMeTheWay()
        {
            IEnumerable<string> path;

            path = FindPath("a", "e", graph);

            PrintPath(path);
        }

        private static void PrintPath(IEnumerable<string> path)
        {
            if (path == null)
            {
                Console.Write("Not connected.");
            }
            else
            {

                foreach (var node in path)
                {
                    Console.Write(node);

                    if( path.Last<string>() == node )
                    {
                        Console.Write("  = path found");
                    }
                    else
                    {
                        Console.Write(" -> ");
                    }

                }

                Console.Write("!");
            }
        }

        private static IEnumerable<string> FindPath(string start, string finish, Dictionary<string, IEnumerable<string>> graph)
        {
            //
            // to keep logic clean, in order to demonstarte search algorothm clearly, 
            // lets pass correctly formed graph, 
            // containing start and finish (not necessery connected thought!)
            // and all connections are bidirectional and correectly described (for both points)
            //

            List<string> path = null;

            if (graph == null || graph.Count < 1)
                return null;

            if(start == finish)
            {
                path = new List<string>() { start };
                return path;
            }
            //if only two, than already may or may not be connected

            Dictionary<string, string> cameFrom = new Dictionary<string, string>()
            {
                { start, start },
            };

            List<string> currentCheck = new List<string>() { start };

            bool cameToFinish = false;

            do
            {
                List<string> nextCheck = new List<string>();

                foreach (var node in currentCheck)
                {
                    foreach (var connectedNode in graph[node])
                    {
                        if (cameFrom.ContainsKey(connectedNode))
                        {
                            //visited this node already from another
                        }
                        else
                        {
                            //first time visit
                            cameFrom[connectedNode] = node; //mark that have came from current node
                            nextCheck.Add(connectedNode);
                        }

                        if (connectedNode == finish)
                        {
                            //come to the finish
                            cameToFinish = true;
                            break;
                        }

                    }

                    if (cameToFinish)
                    {
                        //come to the finish
                        break;
                    }
                }

                currentCheck = nextCheck;

            } while (currentCheck.Count > 0);

            if (cameToFinish)
            {
                //means - connected
                //so, fill-in path from cameFrom

                // start from finish and go backwards
                path = new List<string>() { finish };

                // add in reverse order
                string current = finish;
                do
                {
                    string prev = cameFrom[current];
                    path.Add(prev);
                    current = prev;
                } while (current != start);

                path.Reverse();
            }

            //if all posible tarversed but didn't cameToFinish, than empty path is returned

            return path;
        }
    }
}
