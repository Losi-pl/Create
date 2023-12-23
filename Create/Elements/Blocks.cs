using Create.Elements.Bazic.Blocks;
using Create.SourceGenerators;

namespace Create.Elements;

/// <summary>
/// Wrzystkie bloki
/// </summary>
[Register(typeof(Block))]
public static class Blocks
{
    public static readonly Block AIR = new Air();
    public static readonly Block STONE = new Stone();
    public static readonly Block DIRT = new Dirt();
    public static readonly Block GRASS_BLOCK = new GrassBlock();
    public static readonly Block BEDROCK = new Bedrock();
    public static readonly Block OAK_LOG = new OakLog();
    public static readonly Block OAK_PLANKS = new OakPlanks();
    public static readonly Block CRAFTING_TABLE = new CraftingTable();
}
