namespace _2024.Day09;

public class Solution
{
    abstract record Block(int Length);

    private record FileBlock(int Id, int Length) : Block(Length);

    private record SpaceBlock(int Length) : Block(Length);

    private const bool UseTestInput = false;

    private readonly string input;
    private readonly List<FileBlock> fileBlocks;
    private readonly List<(FileBlock, SpaceBlock)> fileAndSpace;
    private readonly List<Block> blockList;

    public Solution(string testInput, string input)
    {
        this.input = UseTestInput ? testInput : input;

        this.fileAndSpace = this.input.Chunk(2)
            .Select((it, i) => (
                new FileBlock(i, int.Parse(it[0].ToString())),
                new SpaceBlock(it.Length == 1 ? 0 : int.Parse(it[1].ToString()))))
            .ToList();

        this.blockList = this.fileAndSpace.SelectMany(it => new Block[]{it.Item1, it.Item2}).ToList();

        this.fileBlocks = fileAndSpace.Select(it => it.Item1).ToList();
    }

    public object PartOne()
    {
        var blocks = this.ProcessFileBlocksWithSpaces();
        var reversedBlocks = ReversedCopyOf(this.fileBlocks);
        var nextIndex = blocks.IndexOf(-1);

        foreach (var fileBlock in reversedBlocks)
        {
            for (int i = 0; i < fileBlock.Length; i++)
            {
                blocks[nextIndex] = fileBlock.Id;
                nextIndex = blocks.IndexOf(-1);
                if (nextIndex == -1) break;
            }
            if (nextIndex == -1) break;
        }

        return blocks.Select((a, i) => (long) a*i).Sum();

    }

    public object PartTwo()
    {

        var newList = this.blockList.ToList();
        for (int i = newList.Count - 1; i > 0; i--)
        {
            if (newList[i] is SpaceBlock) continue;
            var source = (FileBlock)newList[i];

            for (int j = 0; j < i; j++)
            {
                if (newList[j] is FileBlock) continue;
                var target = (SpaceBlock)newList[j];

                if (source.Length == target.Length)
                {
                    newList[j] = newList[i];
                    newList[i] = new SpaceBlock(source.Length);
                    break;
                }
                if (source.Length < target.Length)
                {
                    var composite = new[] { newList[i], new SpaceBlock(target.Length - source.Length) };
                    newList[i] = new SpaceBlock(source.Length);
                    newList.RemoveAt(j);
                    newList.InsertRange(j, composite);
                    break;
                }
            }
        }

        var totalLength = this.blockList.Sum(it => it.Length);
        var values = new long[totalLength];
        var counter = 0;

        foreach (var block in newList)
        {
            switch (block)
            {
                case SpaceBlock:
                    counter += block.Length;
                    break;
                case FileBlock b:
                {
                    for (int i = 0; i < block.Length; i++)
                    {
                        values[counter] = b.Id * counter;
                        counter++;
                    }
                    break;
                }
            }
        }

        return values.Sum();
    }

    private static List<FileBlock> ReversedCopyOf(List<FileBlock> blocks)
    {
        var reversedFileBlocks = blocks.ToList();
        reversedFileBlocks.Reverse();
        return reversedFileBlocks;
    }

    private List<int> ProcessFileBlocksWithSpaces()
    {
        var totalLength = this.fileBlocks.Sum(it => it.Length);
        var all = new int[totalLength];

        int counter = 0;
        foreach (var (fileBlock, spaceBlock) in this.fileAndSpace)
        {
            for (int i = 0; i < fileBlock.Length; i++)
            {
                if (counter >= totalLength) break;
                all[(counter)] = fileBlock.Id;
                counter++;
            }
            if (counter >= totalLength) break;

            for (int i = 0; i < spaceBlock.Length; i++)
            {
                all[(counter)] = -1;
                if (counter >= totalLength) break;
                counter++;
            }
            if (counter >= totalLength) break;
        }

        return all.ToList();
    }
}