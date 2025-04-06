namespace Api.VM
{
    public class Item
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }

        public Item(string name, string icon, string description)
        {
            Name = name;
            Icon = icon;
            Description = description;
        }
    }
}
