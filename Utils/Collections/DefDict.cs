namespace Utils.Collections
{
    public class DefDict<Tkey, Tval> : Dictionary<Tkey, Tval>
    {
        public DefDict()
        { }

        public DefDict(IEqualityComparer<Tkey> cmp) : base(cmp)
        { }

        public new Tval this[Tkey key]
        {
            get { base.TryGetValue(key, out Tval res); return res; }
            set { base[key] = value; }
        }
    }
}