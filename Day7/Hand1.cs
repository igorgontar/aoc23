class Hand1
{
    public static readonly HandComparer Comparer = new ();
    
    public readonly int bid;

    const string faces  = "23456789TJQKA";
    const string values = "23456789ABCDE"; // like hex values
    
    readonly int type;
    readonly string cards;
    readonly string hand;
    
    public Hand1(string cards, int bid)
    {
        this.cards = cards;
        this.bid = bid;

        // convert, so we can simply compare hands alphabetically, if the hand values are the same
        this.hand = string.Concat( cards.Select(c => values[faces.IndexOf(c)]) );
        this.type = DetectType(cards);
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
#if true        
        var sorted = dict.Values.Order().ToArray();
        if(cmp(sorted,         5)) return 7;
        if(cmp(sorted,       1,4)) return 6;
        if(cmp(sorted,       2,3)) return 5;
        if(cmp(sorted,     1,1,3)) return 4;
        if(cmp(sorted,     1,2,2)) return 3;
        if(cmp(sorted,   1,1,1,2)) return 2;
        if(cmp(sorted, 1,1,1,1,1)) return 1;
 #else           
        if(dict.Count==1) // AAAAA - five
            return 7;

        if(dict.Count==2 && (dict.Values.First()==1 || dict.Values.Last()==1) ) // AAAA + B - four
            return 6;
        
        if(dict.Count==2 && (dict.Values.First()==2 || dict.Values.Last()==2)) // AAA + BB - full haus
            return 5;
        
        if(dict.Count==3 && dict.ContainsValue(3)) // AAA + B + C - tree
            return 4;
        
        if(dict.Count==3 && dict.ContainsValue(2)) // AA + BB + C - two pairs
            return 3;
        
        if(dict.Count==4) // AA + B + C + D  - one pair
            return 2;
        
        if(dict.Count==5) // staight, do we need to check if it's sequental?
            return 1;
 #endif
        
        throw new Exception("invalid hand type of cards");
    }

    static bool cmp(int[] a1, params int[] a2)
    {
        if(a1.Length != a2.Length) 
            return false;
        for(int i=0; i<a1.Length; i++)
            if(a1[i] != a2[i])
                return false;
        return true;         
    }

    public class HandComparer : IComparer<Hand1>
    {
        public int Compare(Hand1 x, Hand1 y) 
        { 
            if(x.type < y.type) return -1;
            if(x.type > y.type) return 1;
            
            return string.Compare(x.hand, y.hand, StringComparison.Ordinal); 
        }
    }
}



