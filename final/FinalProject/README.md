# Ruins of the First Star

This open-ended final project is a small console adventure game. The player explores ancient ruins, manages health and supplies, and tries to collect 3 relic shards before time runs out.

## How to Run

From the repository root:

```bash
dotnet run --project final/FinalProject/FinalProject.csproj
```

## How to Play

- Choose from the main menu each turn.
- `Explore the ruins` spends a day and triggers a random encounter.
- `Rest at camp` spends a day and restores some health.
- `Use a backpack item` lets you consume supplies without spending a day.
- Win by collecting 3 relic shards before your health reaches 0 or your days run out.

## OOP Highlights

- `Encounter` is an abstract base class with derived encounter types that override behavior.
- `Item` is an abstract base class with derived item types that apply different effects.
- `Player`, `ExpeditionLog`, and `ConsoleHelper` each have focused responsibilities.
- Member variables are private and accessed through public behaviors instead of direct access.
