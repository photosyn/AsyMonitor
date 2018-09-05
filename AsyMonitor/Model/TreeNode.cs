using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyMonitor
{
    class TreeNode
    {
        public TreeNode()
        {
            this.Nodes = new List<TreeNode>();
            this.ParentKey = "";//主节点的父key默认为""
        }
  
        public List<TreeNode> Nodes { get; set; }
        public string Key { get; set; }//key
        public string ParentKey { get; set; }//parentKey
        public string NodeName { get; set; }
}
}
