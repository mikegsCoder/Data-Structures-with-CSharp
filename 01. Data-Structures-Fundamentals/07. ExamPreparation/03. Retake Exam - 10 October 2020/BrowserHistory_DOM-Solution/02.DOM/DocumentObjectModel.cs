namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        private List<IHtmlElement> elements;

        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.elements = new List<IHtmlElement>();

            this.Root = new HtmlElement(ElementType.Document,
                new HtmlElement(ElementType.Html,
                    new HtmlElement(ElementType.Head, null),
                    new HtmlElement(ElementType.Body, null)));
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl.Type == type)
                {
                    return currentEl;
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var result = new List<IHtmlElement>();

            this.FindElementsByTypeDfs(this.Root, type, result);

            return result;
        }

        private void FindElementsByTypeDfs(IHtmlElement current, ElementType type, List<IHtmlElement> result)
        {
            foreach (var child in current.Children)
            {
                this.FindElementsByTypeDfs(child, type, result);
            }

            if (current.Type == type)
            {
                result.Add(current);
            }
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl == htmlElement)
                {
                    return true;
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return false;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            IHtmlElement currentEl = null;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                currentEl = queue.Dequeue();

                if (currentEl == parent)
                {
                    break;
                }

                foreach (var currChild in currentEl.Children)
                {
                    queue.Enqueue(currChild);
                }
            }

            child.Parent = currentEl;
            currentEl.Children.Insert(0, child);
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            IHtmlElement currentEl = null;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                currentEl = queue.Dequeue();

                if (currentEl == parent)
                {
                    break;
                }

                foreach (var currChild in currentEl.Children)
                {
                    queue.Enqueue(currChild);
                }
            }

            child.Parent = currentEl;
            currentEl.Children.Add(child);
        }

        public void Remove(IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }

            this.RemoveReferences(htmlElement, htmlElement.Parent);
        }

        private void RemoveReferences(IHtmlElement currentEl, IHtmlElement parent)
        {
            parent.Children.Remove(currentEl);
            currentEl.Parent = null;
            currentEl.Children.Clear();
        }

        public void RemoveAll(ElementType elementType)
        {
            var toRemove = this.GetElementsByType(elementType);

            foreach (var element in toRemove)
            {
                this.Remove(element);
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }

            if (htmlElement.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            htmlElement.Attributes.Add(attrKey, attrValue);
            return true;
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            if (!this.Contains(htmlElement))
            {
                throw new InvalidOperationException();
            }

            if (!htmlElement.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            htmlElement.Attributes.Remove(attrKey);
            return true;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl.Attributes.ContainsKey("id") && currentEl.Attributes["id"] == idValue)
                {
                    return currentEl;
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            this.CreateTextDfs(this.Root, result, 0);

            return result.ToString();
        }

        private void CreateTextDfs(IHtmlElement current, StringBuilder result, int depth)
        {
            result.AppendLine($"{new string(' ', depth)}{current.Type.ToString()}");

            foreach (var child in current.Children)
            {
                this.CreateTextDfs(child, result, depth + 2);
            }
        }
    }
}
