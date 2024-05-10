using System;
using System.IO;
using MyGame.Interfaces;
using MyGame.Model.Models;

namespace MyGame.Utilities;

public static class Helpers
{
    public static void DrawRectangle(this ICollidable collidable, SpriteBatch spriteBatch, Texture2D pixelTexture, Color color)
    {
        Rectangle hb = new Rectangle(
            (int)Math.Floor(collidable.HitBox.Position.X),  // Convert width to integer
            (int)Math.Floor(collidable.HitBox.Position.Y),   // Convert height to integer
            (int)Math.Floor(collidable.HitBox.Box.X),       // Convert X coordinate to integer
            (int)Math.Floor(collidable.HitBox.Box.Y)        // Convert Y coordinate to integer
        );
        spriteBatch.Draw(pixelTexture, hb, color);
    }

    public static Vector2 GenerateRandomPosBelow(int upperLimit)
    {
        // Ensure topLeft is actually top-left and bottomRight is bottom-right
        float minX = Math.Min(Constants.LeftTop.X, Constants.RightBottom.X - 100); // Offset for image width
        float minY = Math.Min(Constants.LeftTop.Y, Constants.RightBottom.Y);
        float maxX = Math.Max(Constants.LeftTop.X, Constants.RightBottom.X - 250);
        float maxY = Math.Max(Constants.LeftTop.Y, Constants.RightBottom.Y);

        // Generate random x and y coordinates within the specified bounds
        Random random = new Random();
        float randomX = (float)(random.NextDouble() * (maxX - minX) + minX);
        float randomY = (float)(random.NextDouble() * (maxY - minY - upperLimit) + minY);

        // Ensure the generated position is above the lowerLimit
        float posY = Math.Max(randomY, minY - upperLimit);

        // Return the generated position
        return new Vector2(randomX, posY);
    }

    /// <summary>
    /// Checks if position is in the border boundaries
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>True if pos is out of border</returns>
    public static bool IsOutOfBorder(Vector2 pos)
    {
        if (pos.X > Constants.RightBottom.X || pos.X < Constants.LeftTop.X ||
            pos.Y > Constants.RightBottom.Y || pos.Y < Constants.LeftTop.Y)
            return true;
        return false;
    }

    /// <summary>
    /// Checks if position is in the border boundaries plus some
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>True if pos is out of border with extra</returns>
    public static bool IsOutOfProjectileBorder(Vector2 pos)
    {
        if (pos.X > Constants.RightBottom.X + 200f || pos.X < Constants.LeftTop.X - 200f ||
            pos.Y > Constants.RightBottom.Y + 200f || pos.Y < Constants.LeftTop.Y - 200f)
            return true;
        return false;
    }

    /// <summary>
    /// Finds a unit vector in given direction
    /// </summary>
    /// <param name="vector"></param>
    /// <returns> Unit vector in direction of given vector </returns> 
    public static Vector2 GetUnitVector(this Vector2 vector)
    {
        float magnitude = vector.Length(); // Calculate magnitude (length) of the vector

        // Avoid division by zero by checking if magnitude is not zero
        if (magnitude > 0)
        {
            // Normalize the vector by dividing each component by the magnitude
            float x = vector.X / magnitude;
            float y = vector.Y / magnitude;

            // Return the unit vector
            return new Vector2(x, y);
        }
        else
        {
            // If magnitude is zero, return a zero vector (or handle this case as needed)
            return Vector2.Zero;
        }
    }


    /// <summary>
    /// Gets path to file or folder
    /// </summary>
    /// <param name="path_name"></param>
    /// <returns> String path to name </returns>
    /// <exception cref="DirectoryNotFoundException"></exception> <summary>
    public static string GetPath(string name)
    {
        // Start from the current directory
        string currentDirectory = Environment.CurrentDirectory;

        // Find "MyGame" directory
        string myGameDirectory = FindMyGameDirectoryInSubDirs(currentDirectory);

        if (myGameDirectory == null)
            myGameDirectory = FindMyGameDirectoryInParentDirs(currentDirectory);

        if (myGameDirectory == null)
            throw new DirectoryNotFoundException("Folder 'MyGame' not found in the path.");


        // Search for the specified folder or file within "MyGame" directory and its subdirectories
        string fullPath = SearchDirectoryForItem(myGameDirectory, name);

        if (fullPath == null)
        {
            throw new FileNotFoundException($"File or folder '{name}' not found within 'MyGame' directory and its subdirectories.");
        }

        return fullPath;
    }

    private static string FindMyGameDirectoryInSubDirs(string startDirectory)
    {
        // Search for "MyGame" directory recursively within startDirectory and its subdirectories
        return SearchSubDirectoriesForMyGame(startDirectory);
    }

    private static string SearchSubDirectoriesForMyGame(string directory)
    {
        // Check if the current directory is "MyGame"
        if (string.Equals(Path.GetFileName(directory), "MyGame", StringComparison.OrdinalIgnoreCase))
        {
            return directory;
        }

        // Search within subdirectories
        string[] subdirectories = Directory.GetDirectories(directory);
        foreach (string subdirectory in subdirectories)
        {
            string result = SearchSubDirectoriesForMyGame(subdirectory);
            if (result != null)
            {
                return result;
            }
        }

        // If "MyGame" is not found in the current directory or its subdirectories, return null
        return null;
    }


    private static string FindMyGameDirectoryInParentDirs(string startDirectory)
    {
        // Traverse up through parent folders until reaching a folder named "MyGame" or until the root directory is reached
        while (startDirectory != null && !string.Equals(Path.GetFileName(startDirectory), "MyGame", StringComparison.OrdinalIgnoreCase))
        {
            // Move up one directory
            startDirectory = Directory.GetParent(startDirectory)?.FullName;
        }

        return startDirectory;
    }

    private static string SearchDirectoryForItem(string directory, string itemName)
    {
        // Search for the specified folder or file within the directory itself
        string fullPath = Path.Combine(directory, itemName);
        if (File.Exists(fullPath) || Directory.Exists(fullPath))
        {
            return fullPath;
        }

        // Recursively search within each subdirectory
        string[] subdirectories = Directory.GetDirectories(directory);
        foreach (string subdirectory in subdirectories)
        {
            string result = SearchDirectoryForItem(subdirectory, itemName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}