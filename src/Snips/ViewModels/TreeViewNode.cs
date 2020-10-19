using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Snips.ViewModels
{
    public enum NodeType
    {
        Directory,
        File
    }

    public class TreeViewNode : IEquatable<TreeViewNode>
    {
        [JsonProperty("id")] 
        public Guid Id { get; set; }
        [JsonProperty("text")] 
        public string Text { get; set; }
        [JsonProperty("icon")] 
        public string Icon { get; set; }

        [JsonProperty("selectable")] 
        public bool Selectable { get; private set; }

        [JsonProperty("href", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Href { get; private set; }

        private readonly List<TreeViewNode> _children;

        [JsonIgnore] 
        public TreeViewNode Parent;

        [JsonProperty("nodes")]
        public IReadOnlyList<TreeViewNode> Children
            => _children;

        public TreeViewNode(Guid id, string text, NodeType nodeType)
        {
            Selectable = nodeType == NodeType.File;
            // Href = nodeType == NodeType.File ? $"#{id}" : null;
            _children = new List<TreeViewNode>();
            Id = id;
            Text = text;
            Icon = nodeType == NodeType.Directory ? "fas fa-folder" : "fas fa-file";
        }

        public void AddChild(TreeViewNode treeViewNode)
        {
            if (treeViewNode == null)
                throw new ArgumentNullException(nameof(treeViewNode));

            if (_children.Contains(treeViewNode))
                throw new ArgumentException($"Node {treeViewNode.Id} is already a child of node {Id}",
                    nameof(treeViewNode));

            if (treeViewNode.Parent != null)
                throw new ArgumentException($"Node {treeViewNode.Id} already has a parent {treeViewNode.Parent.Id}",
                    nameof(treeViewNode));

            if (treeViewNode == this)
                throw new ArgumentException($"Node {treeViewNode.Id} cannot be added as child to itself");

            _children.Add(treeViewNode);
            treeViewNode.Parent = this;
        }

        public bool Equals(TreeViewNode other)
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
            return Equals((TreeViewNode) obj);
        }

        public override int GetHashCode() =>
            Id.GetHashCode();

        public static bool operator ==(TreeViewNode left, TreeViewNode right) =>
            Equals(left, right);

        public static bool operator !=(TreeViewNode left, TreeViewNode right) =>
            !Equals(left, right);
    }
}