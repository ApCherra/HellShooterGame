using System;
using System.Net.Http.Headers;

namespace MyGame.Utilities;

public static class Constants
{
    public const int ScreenWidth = 1200;
    public const int ScreenHeight = 650;

    public static Vector2 RightBottom = new Vector2(ScreenWidth, ScreenHeight);
    public static Vector2 LeftTop = new Vector2(0, 0);

    public const int PlayerInitialLives = 3;
    public const float playerMoveSpeed = 5.0f;

    public const int EnemySpawnRate = 2;

    public const string GameTitle = "Bullet Hell Game";

    public const string PlayerSpritePath = "Textures/Player";
    public const string EnemySpritePath = "Textures/Enemy";

    public const string ContentPath = "Content/DesktopGL";
}