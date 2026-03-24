using System;

namespace JustMoby_TestWork
{
    public interface IConfigRepository
    {
        GameConfig GameConfig { get; }
        PlayerConfig Player { get; }
        EnemyConfig Enemy { get; }
    }
}