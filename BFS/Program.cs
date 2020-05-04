using System;
using System.Collections.Generic;

namespace BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            int numRows = 5, numCols = 4;
            int[,] area = new int[,]
            {
                { 1, 1, 1, 1 },
                { 0, 1, 1, 1 },
                { 0, 1, 0, 1 },
                { 1, 1, 9, 1 },
                { 0, 0, 1, 1 }
            };

            // Starting point of the truck ([0, 0]).
            int startRow = 0, startCol = 0;
            // Empty row and column queues.
            Queue<int> rowQueue = new Queue<int>();
            Queue<int> colQueue = new Queue<int>();

            // Variables used to track the number of steps taken.
            int distance = 0,
                nodesLeftInlayer = 1,
                nodesInNextLayer = 0;

            // Variable used to track whether we reached the destination 
            // (denoted with 9).
            bool reachedDest = false;

            // numRows x numColumns matrix of false values used to track 
            // whether the node at position [i, j] has been visited.
            bool[,] visited = new bool[numRows, numCols];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    visited[i, j] = false;
                }
            }

            // North, south, east and west direction vectors.
            int[] dirRow = new int[] { -1, +1, 0, 0 };
            int[] dirCol = new int[] { 0, 0, +1, -1 };

            ////////////////////////////////
            // Begining of BFS algorithm. //
            ////////////////////////////////

            // Enqueue the starting point and mark it as visited.
            rowQueue.Enqueue(startRow);
            colQueue.Enqueue(startCol);
            visited[startRow, startCol] = true;

            while (rowQueue.Count > 0) // || columnQueue.Count > 0
            {
                int row = rowQueue.Dequeue();
                int col = colQueue.Dequeue();

                // If we encounter 9 (destination point), then we have 
                // reached destination, so we stop.
                if (area[row, col] == 9)
                {
                    reachedDest = true;
                    break;
                }

                // Explore neighbors of the current point. We can have at 
                // most 4 neighbors (top, bottom, right, left).
                for (int i = 0; i < 4; i++)
                {
                    int newRow = row + dirRow[i];
                    int newCol = col + dirCol[i];

                    // We skip the points that are out of bounds.
                    if (newRow < 0 || newCol < 0 ||
                        newRow >= numRows || newCol >= numCols) continue;

                    // We skip the visited points or points without roads.
                    if (visited[newRow, newCol] || area[newRow, newCol] == 0) continue;

                    // Otherwise, we add the new point to the queue, mark it 
                    // as visited, and increment the node count in current layer.
                    rowQueue.Enqueue(newRow);
                    colQueue.Enqueue(newCol);
                    visited[newRow, newCol] = true;
                    nodesInNextLayer++;
                }

                // If the nodes in the current layer reach 0, we increment the 
                // distance and move to the next layer.
                nodesLeftInlayer--;
                if (nodesLeftInlayer == 0)
                {
                    nodesLeftInlayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    distance++;
                }
            }
            ///////////////////////////
            // End of BFS algorithm. //
            ///////////////////////////

            // If we reached the destination we return the distance,
            // otherwise, we return -1 (meaning destination not found).
            if (reachedDest) Console.WriteLine("Total distance: {0}", distance);
            else Console.WriteLine("Total distance: -1");

            Console.Read();
        }
    }
}