namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private Deque<ILink> links;

        public BrowserHistory()
        {
            this.links = new Deque<ILink>();
        }

        public int Size => this.links.Count;

        public void Clear()
        {
            this.links.Clear();
        }

        public bool Contains(ILink link)
        {
            return this.links.Contains(link);
        }

        public ILink DeleteFirst()
        {
            this.CheckIsEmpty();

            return this.links.RemoveFromFront();
        }

        public ILink DeleteLast()
        {
            this.CheckIsEmpty();

            return this.links.RemoveFromBack();
        }

        public ILink GetByUrl(string url)
        {
            return this.links.FirstOrDefault(x => x.Url == url);
        }

        public ILink LastVisited()
        {
            this.CheckIsEmpty();

            return this.links.GetAtBack();
        }

        private void CheckIsEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
        }

        public void Open(ILink link)
        {
            this.links.Add(link);
        }

        public int RemoveLinks(string url)
        {
            var result = new List<ILink>();

            foreach (var link in this.links)
            {
                if (link.Url.ToLower().Contains(url.ToLower()))
                {
                    result.Add(link);
                }
            }

            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }
            
            foreach (var link in result)
            {
                this.links.Remove(link);
            }

            return result.Count;
        }

        public ILink[] ToArray()
        {
            return this.ToList().ToArray();
        }

        public List<ILink> ToList()
        {
            var result = this.links.ToList();
            result.Reverse();

            return result;
        }

        public string ViewHistory()
        {
            if (this.Size == 0)
            {
                return "Browser history is empty!";
            }

            string result = string.Empty;
            var collection = this.ToList();

            foreach (var link in collection)
            {
                result += link.ToString() + "\r\n";
            }

            return result;
        }
    }
}
