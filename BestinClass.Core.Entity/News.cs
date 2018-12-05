using System.Diagnostics.Contracts;

namespace BestinClass.Core.Entity
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Picture { get; set; }
        public string ShortDesc { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
    }
}