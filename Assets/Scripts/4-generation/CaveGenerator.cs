using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
/**
* This class is used to generate a random "cave" map.
* The map is generated as a two-dimensional array of ints, where "0" denotes floor and "1" denotes wall.
* Initially, the boundaries of the cave are set to "wall", and the inner cells are set at random.
* Then, a cellular automaton is run in order to smooth out the cave.
* 
* Based on Unity tutorial https://www.youtube.com/watch?v=v7yyZZjF1z4 
* Code by Habrador: https://github.com/Habrador/Unity-Programming-Patterns/blob/master/Assets/Patterns/7.%20Double%20Buffer/Cave/GameController.cs
* Using a double-buffer technique explained here: https://github.com/Habrador/Unity-Programming-Patterns#7-double-buffer
* 
* Adapted by: Erel Segal-Halevi
* Since: 2020-12
*/
public class CaveGenerator {
    //Used to init the cellular automata by spreading random dots on a grid,
    //and from these dots we will generate caves.
    //The higher the fill percentage, the smaller the caves.
    protected float randomFillPercent;

    //The height and length of the grid
    protected int gridSize;
    readonly int tileNumber = 4;       // Number of tiles
    readonly int areaMinNeighbors = 3; // Minimum neighbors in order to form an area
    //The double buffer
    private int[,] bufferOld;
    private int[,] bufferNew;


    private Random random;

    public CaveGenerator(float randomFillPercent=0.5f, int gridSize=100) {
        this.randomFillPercent = randomFillPercent;
        this.gridSize = gridSize;

        this.bufferOld = new int[gridSize, gridSize];
        this.bufferNew = new int[gridSize, gridSize];

        random = new Random();
    }

    public int[,] GetMap() {
        return bufferOld;
    }



    /**
     * Generate a random map.
     * The map is not smoothed; call Smooth several times in order to smooth it.
     */
    public void RandomizeMap()  {
        //Init the old values so we can calculate the new values
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1) {
                    //We dont want holes in our walls, so the border is always a wall
                    bufferOld[x, y] = 1;
                }                
                else {
                    //Random walls and caves
                    bufferOld[x, y] = Random.Range(0, tileNumber);
                }
            }
        }
    }


    /**
     * Generate caves by smoothing the data
     * Remember to always put the new results in bufferNew and use bufferOld to do the calculations
     */
    public void SmoothMap()   {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                //Border is always wall
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1) {
                    bufferNew[x, y] = 1;
                    continue;
                }
                bufferNew[x, y] = SurroundingCheck(x, y);
            }
        }

        //Swap the pointers to the buffers
        (bufferOld, bufferNew) = (bufferNew, bufferOld);
    }

    // Check which tile is more common around cell [x, y] and change it to match the majority if needed.
    private int SurroundingCheck(int cellX, int cellY)
    {
        int[] countTiles = { 0, 0, 0, 0};
        for (int i = cellX - 1; i < cellX + 2; i++)
        {
            for (int j = cellY - 1; j < cellY + 2; j++)
            {
                if (i == cellX && j == cellY)
                    continue;
                else
                    countTiles[bufferOld[i, j]]++;                    
            }
        }
        // If the tile has at least three similar tiles around - do not change it.
        if (countTiles[bufferOld[cellX, cellY]] > areaMinNeighbors)
            return bufferOld[cellX, cellY];
        // If there is more common tile kind around - change it to the same tile value.
        else
        {
            int max = countTiles.Max();
            int maxIndex = countTiles.ToList().IndexOf(max);
            return maxIndex;
        }
    }
}
