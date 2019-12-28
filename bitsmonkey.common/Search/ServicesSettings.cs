namespace bitsmonkey.common.Search
{
    public class ServicesSettings
    {
        public Service[] Services { get; set; }
    }

    public class Service
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public Service[] Services { get; set; }
        public string[] Tags { get; set; }
    }
}