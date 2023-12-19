class Hand2
{
    public static readonly HandComparer Comparer = new ();
    
    public readonly int bid;

    const string faces  = "23456789TJQKA";
    const string values = "23456789A1CDE"; // like hex values, Jocker is now => 1
    
    readonly int type;
    readonly string cards;
    readonly string hand;
    
    public Hand2(string cards, int bid)
    {
        this.cards = cards;
        this.bid = bid;

        // convert, so we can simply compare hands alphabetically, if the hand values are the same
        this.hand = string.Concat( cards.Select(c => values[faces.IndexOf(c)]) );

        int j = cards.Count(c => c=='J');
        if (j == 0)
            this.type = DetectType(cards);
        else
            this.type = DetectTypeWithJocker(cards, j);
    }

    static int DetectTypeWithJocker(string cards, int j)
    {
        if(j==5) return 7; // five
        if(j==4) return 7; // five
        if(j==3) return 6; // four
        if (j == 1)
        {
            int type = cards.Where(c => c != 'J').Select(c => DetectType(cards.Replace('J',c))).Max();
            return type;
        }
        if (j == 2)
        {
            var dict = new Dictionary<char, int>();
            foreach (var c in cards.Where(c => c != 'J'))
            {
                int count = dict.GetValueOrDefault(c);
                if(count == 0)
                    dict[c] = 1;
                else
                    dict[c] = count+1;
            }
            var sorted = dict.Values.Order().ToArray();
            if(sorted.eq(    3)) return 7; // five
            if(sorted.eq(  1,2)) return 6; // four
            if(sorted.eq(1,1,1)) return 4; // three

            return 1;
        }

        throw new Exception($"invalid jocker count: {j}");
    }
    

    static int DetectType(string cards)
    {
        var dict = new Dictionary<char, int>();
        foreach (var c in cards)
        {
            int count = dict.GetValueOrDefault(c);
            if(count == 0)
                dict[c] = 1;
            else
                dict[c] = count+1;
        }
        var sorted = dict.Values.Order().ToArray();
        if(sorted.eq(        5)) return 7;
        if(sorted.eq(      1,4)) return 6;
        if(sorted.eq(      2,3)) return 5;
        if(sorted.eq(    1,1,3)) return 4;
        if(sorted.eq(    1,2,2)) return 3;
        if(sorted.eq(  1,1,1,2)) return 2;
        if(sorted.eq(1,1,1,1,1)) return 1;
        
        throw new Exception("invalid hand type of cards");
    }

    public class HandComparer : IComparer<Hand2>
    {
        public int Compare(Hand2 x, Hand2 y) 
        { 
            if(x.type < y.type) return -1;
            if(x.type > y.type) return 1;
            
            return string.Compare(x.hand, y.hand, StringComparison.Ordinal); 
        }
    }
}



