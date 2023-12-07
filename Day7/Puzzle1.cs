class Puzzle1
{
    const StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries;

    public static long Solve(string file)
    {
        using var reader = new StreamReader(file);

        SortedSet<Hand1> hands = new(Hand1.Comparer);
        
        string line = null;
        while((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var split = line.Split(' ', splitOptions);
            var h = split[0];
            int bid = int.Parse(split[1]);
            var hand = new Hand1(h, bid);
            hands.Add(hand);
            
        }

        long sum = 0;
        int rank = 0;
        foreach (var hand in hands)
        {
            ++rank;
            sum += (rank * hand.bid);
        }

        return sum;
    }
}
