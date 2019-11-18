using System.Windows.Forms;

namespace SentrySSMS
{
    public interface IObjectExplorerExtender
    {
        bool GetNodeExpanding(TreeNode node);
        string GetNodeUrnPath(TreeNode node);
        TreeView GetObjectExplorerTreeView();
        void ReorganizeNodes(TreeNode node, string nodeTag);
    }
}
