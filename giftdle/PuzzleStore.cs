using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace giftdle;

public sealed class PuzzleStore
{
    private readonly ConcurrentDictionary<string, GiftPuzzle> puzzles = new();

    public string Create(GiftPuzzle puzzle)
    {
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLowerInvariant();
        puzzles[token] = puzzle;
        return token;
    }

    public bool TryGet(string token, out GiftPuzzle? puzzle) => puzzles.TryGetValue(token, out puzzle);
}

public sealed record GiftPuzzle(
    string GiftName,
    string Usefulness,
    string Texture,
    string Shape,
    string Colors,
    string LifeStatus,
    string OfficeStatus,
    string BedroomStatus,
    string CostRange,
    string ZooStatus,
    DateTime GiftDate);
