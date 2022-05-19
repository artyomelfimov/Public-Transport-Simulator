﻿using System.Collections.Generic;
using System.Xml;

/// Этот класс содержит информацию о линиях общественного транспорта. С помощью
// с помощью этой информации мы можем определить, какие пути являются железными дорогами, а какие
/// есть дороги. Мы также можем определить, какие узлы являются станциями.

class OsmRelation : BaseOsm
{
    public bool Route { get; private set; }

    public string TransportType { get; private set; }

    public string Name { get; private set; }

    public List<ulong> StoppingNodeIDs { get; private set; }

    public List<ulong> WayIDs { get; private set; }

    public List<string> StationNames { get; set; }

    /// <param name="node">XML node</param>
    public OsmRelation(XmlNode node)
    {
        StoppingNodeIDs = new List<ulong>();
        WayIDs = new List<ulong>();
        StationNames = new List<string>();

        IsPublicRoute(node);
    }

    /// <param name="node">XML node</param>
    void IsPublicRoute(XmlNode node)
    {
        XmlNodeList tags = node.SelectNodes("tag");
        foreach (XmlNode t in tags)
        {
            string key = GetAttribute<string>("k", t.Attributes);
            if (key == "route")
            {
                string value = GetAttribute<string>("v", t.Attributes);
                switch (value)
                {
                    case "subway":
                        TransportType = "subway";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                    case "tram":
                        TransportType = "tram";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                    case "train":
                        TransportType = "train";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                    case "railway":
                        TransportType = "railway";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                    case "light_rail":
                        TransportType = "light_rail";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                    case "bus":
                        TransportType = "bus";
                        Route = true;
                        NameFinder(node);
                        RelationMembers(node);
                        break;
                }
            }
        }
    }

    /// <param name="node">XML node</param>
    void NameFinder(XmlNode node)
    {
        XmlNodeList tags = node.SelectNodes("tag");
        foreach(XmlNode t in tags)
        {
            string key = GetAttribute<string>("k", t.Attributes);
            if (key == "name")
            {
                Name = GetAttribute<string>("v", t.Attributes);
            }
        }
    }

    /// <param name="node">XML node</param>
    void RelationMembers(XmlNode node)
    {
        XmlNodeList members = node.SelectNodes("member");
        foreach (XmlNode n in members)
        {
            string type = GetAttribute<string>("type", n.Attributes);
            if (type == "node")
            {
                string role = GetAttribute<string>("role", n.Attributes);
                if (role == "stop")
                {
                    ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
                    StoppingNodeIDs.Add(refNo);
                }
            }
            else if (type == "way")
            {
                string role = GetAttribute<string>("role", n.Attributes);
                if (role != "platform")
                {
                    ulong refNo = GetAttribute<ulong>("ref", n.Attributes);
                    WayIDs.Add(refNo);
                }
            }
        }
    }
}