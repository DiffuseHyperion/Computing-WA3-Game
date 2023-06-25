using System.Collections.Generic;
using BuildableObjects.LinkPorts;
using UnityEngine;

namespace BuildableObjects
{
    public abstract class LinkableObject : BuildableObject
    {
        private bool _linked;
        private Dictionary<LinkInput, LinkOutput> _linkPairs = new();
        private Dictionary<LinkOutput, LinkInput> _reverseLinkPairs = new();

        protected LinkableObject(string name, string description, int cost, BuildableObjectTypes type) : base(name, description, cost, type)
        {
        }

        public bool GetLinked()
        {
            return _linked;
        }

        public LinkOutput GetLinkOutput(LinkInput linkInput)
        {
            return _linkPairs[linkInput];
        }

        public LinkInput GetLinkInput(LinkOutput linkOutput)
        {
            return _reverseLinkPairs[linkOutput];
        }

        public void AddPair(LinkInput linkInput, LinkOutput linkOutput)
        {
            _linkPairs.Add(linkInput, linkOutput);
            _reverseLinkPairs.Add(linkOutput, linkInput);
        }

        public void RemovePair(LinkInput linkInput)
        {
            LinkOutput linkOutput = _linkPairs[linkInput];
            _linkPairs.Remove(linkInput);
            _reverseLinkPairs.Remove(linkOutput);
        }
        
        public void RemovePair(LinkOutput linkOutput)
        {
            LinkInput linkInput = _reverseLinkPairs[linkOutput];
            _linkPairs.Remove(linkInput);
            _reverseLinkPairs.Remove(linkOutput);
        }
        
        public List<LinkInput> GetInputs()
        {
            return new List<LinkInput>(_linkPairs.Keys);
        }

        public List<LinkOutput> GetOutputs()
        {
            return new List<LinkOutput>(_linkPairs.Values);
        }
    }
}