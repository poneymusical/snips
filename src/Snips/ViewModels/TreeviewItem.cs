using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Snips.Domain.BusinessObjects;

namespace Snips.ViewModels
{
    public enum TreeviewItemType
    {
        Node,
        Leaf
    }

    public class TreeviewItem : IEquatable<TreeviewItem>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public TreeviewItemType Type { get; set; }

        private readonly List<TreeviewItem> _children;

        public TreeviewItem Parent;

        public IReadOnlyList<TreeviewItem> Children
            => _children;

        public TreeviewItem(Directory directory)
            : this(directory.Id, directory.Name, TreeviewItemType.Node)
        {
        }

        public TreeviewItem(Snippet snippet)
            : this(snippet.Id, snippet.Title, TreeviewItemType.Leaf)
        {
        }

        private TreeviewItem(Guid id, string text, TreeviewItemType treeviewItemType)
        {
            Type = treeviewItemType;
            if (treeviewItemType == TreeviewItemType.Node)
                _children = new List<TreeviewItem>();
            Id = id;
            Text = text;
        }

        public void AddChild(TreeviewItem treeviewItem)
        {
            if (treeviewItem == null)
                throw new ArgumentNullException(nameof(treeviewItem));

            if (_children.Contains(treeviewItem))
                throw new ArgumentException($"Node {treeviewItem.Id} is already a child of node {Id}",
                    nameof(treeviewItem));

            if (treeviewItem.Parent != null)
                throw new ArgumentException($"Node {treeviewItem.Id} already has a parent {treeviewItem.Parent.Id}",
                    nameof(treeviewItem));

            if (treeviewItem == this)
                throw new ArgumentException($"Node {treeviewItem.Id} cannot be added as child to itself");

            _children.Add(treeviewItem);
            treeviewItem.Parent = this;
        }

        public bool Equals(TreeviewItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TreeviewItem) obj);
        }

        public override int GetHashCode() =>
            Id.GetHashCode();

        public static bool operator ==(TreeviewItem left, TreeviewItem right) =>
            Equals(left, right);

        public static bool operator !=(TreeviewItem left, TreeviewItem right) =>
            !Equals(left, right);
    }
}