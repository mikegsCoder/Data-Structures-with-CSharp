namespace _01._BrowserHistory
{
    using _01._BrowserHistory.Interfaces;

    public class Link : ILink
    {
        public Link(string url, int loadingTime)
        {
            this.Url = url;
            this.LoadingTime = loadingTime;
        }

        public string Url { get; set; }
        public int LoadingTime { get; set; }

        public override string ToString()
        {
            return $"-- {this.Url} {this.LoadingTime}s";
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            Link that = (Link)obj;
            return Url == that.Url;
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode();
        }
    }
}
