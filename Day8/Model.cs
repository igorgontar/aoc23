using Utils.Collections;

class Node
{
    public readonly string id;
    public readonly string left; 
    public readonly string right;
    public readonly bool A;
    public readonly bool Z;

    public Node l;
    public Node r;
    
    public Node(string id, string left, string right)
    {
        this.id = id;
        this.left = left;
        this.right = right;
        this.A = id.EndsWith('A');
        this.Z = id.EndsWith('Z');
    }
}

class NodeMap : DefDict<string, Node>
{}

class Graf
{
    readonly NodeMap map = new();

    public void Add(Node node)
    {
        map[node.id] = node;
    }  

    public Node Left(Node node)
    {
        if(node.l == null)
        {
            //Console.WriteLine("{0} resolving L", node.id);
            node.l = map[node.left];
        }
        return node.l;
    } 

    public Node Right(Node node)
    {
        if(node.r == null)
        {
            //Console.WriteLine("{0} resolving R", node.id);
            node.r = map[node.right];
        }
        return node.r;
    } 

    public Node this[string id] { get { return map[id]; } } 
    
    public ICollection<Node> Nodes => map.Values;

}
