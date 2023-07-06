namespace Create.Space;

/// <summary>
/// Zestaw parametrów i unikalnych właściwości poszczegulnych światów
/// </summary>
public abstract class Dimention : Elements.Baze
{
    public sealed override Type ElementBazicType => typeof(Dimention);

    /// <summary>
    /// Generuje kawałek terenu o pozycji <see cref="GenerateChunkStruct.pozition"/> w <paramref name="args"/>
    /// </summary>
    /// <param name="args"></param>
    public abstract void GenerateChunk(GenerateChunkStruct args);

    /// <summary>
    /// Zwraca punk w którym gracz pojawi się w świecie w momencie pierwszego dołączenia
    /// </summary>
    /// <returns></returns>
    public virtual (int x, int y, int z) GetNewSpawnPoint()
    {
        return (0, 0, 0);
    }

    public struct GenerateChunkStruct
    {
        public ChunkPoz pozition;
        public Chunk chunk;
    }
}
