using System.Collections.Concurrent;
using Giftdle.Api.Models;

namespace Giftdle.Api.Services;

public sealed class GameStore
{
    private readonly ConcurrentDictionary<Guid, StoredGame> games = new();

    public IReadOnlyCollection<StoredGame> All() => games.Values.ToArray();
    public StoredGame? Get(Guid id) => games.TryGetValue(id, out var game) ? game : null;
    public StoredGame Create(CreateGameRequest request)
    {
        var game = new StoredGame(Guid.NewGuid(), request.Title, request.Recipient, request.Hints, request.Wildcard, request.ExpiresAt);
        games[game.Id] = game;
        return game;
    }
}

public sealed class StoredGame(Guid id, string title, string recipient, IReadOnlyList<string> hints, string wildcard, DateTimeOffset? expiresAt)
{
    public Guid Id { get; } = id;
    public string Title { get; } = title;
    public string Recipient { get; } = recipient;
    public IReadOnlyList<string> Hints { get; } = hints;
    public string Wildcard { get; } = wildcard;
    public DateTimeOffset? ExpiresAt { get; } = expiresAt;
    public int RemainingGuesses { get; set; } = 5;
    public string State { get; set; } = "active";
    public string ShareCode { get; } = Convert.ToHexString(Guid.NewGuid().ToByteArray())[..6].ToLowerInvariant();
}
