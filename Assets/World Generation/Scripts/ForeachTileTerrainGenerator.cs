namespace WorldGeneration2D
{
    public abstract class ForeachTileTerrainGenerator : TerrainGenerator
    {
        public sealed override void ApplyTerrain(ChunkTerrain chunkTerrain)
        {
            var size = chunkTerrain.Chunk.Settings.ChunkSize;
            var halfChunkSize = chunkTerrain.Chunk.Settings.ChunkSize / 2;
            for (int j = 0; j < size.y; j++)
            {
                for (int i = 0; i < size.x; i++)
                {
                    int tileCoordX = i - halfChunkSize.x;
                    int tileCoordY = j - halfChunkSize.y;
                    ApplyTerrain(tileCoordX, tileCoordY, chunkTerrain);
                }
            }
        }

        protected abstract void ApplyTerrain(int x, int y, ChunkTerrain chunkTerrain);
    }
}
