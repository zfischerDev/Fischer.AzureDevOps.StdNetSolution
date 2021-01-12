/* Notes:
 * Added the following NuGet packages:
 * 1. Microsoft.VisualStudio.Services
 * 2. Microsoft.TeamFoundation.Client
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Libraries;
using Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Objects;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;


namespace Fischer.AzureDevOps.StdNetSolution.TestHarness
{
    public partial class TestHarnessForm : Form
    {
        public TestHarnessForm()
        {
            InitializeComponent();
        }

        private void btnExecute01_Click(object sender, EventArgs e)
        {
            ADOProcessingLib zfLibrary = new ADOProcessingLib();

            #region This section should be customized to the project. Added here to test own instance
            //This should be added to the UI to allow user to either select from file or create by builder functionality
            string queryWiqlString = "SELECT System.Id, System.WorkItemType, System.Title, System.Description, System.AssignedTo, " +
                                     "System.IterationPath, System.State FROM workitems WHERE System.WorkItemType = 'Product Backlog Item' " +
                                     @"AND System.IterationPath = 'zfPartsUnlimited\Sprint 6'";


            string[] fields = new string[6];
            fields[0] = "System.Id";
            fields[1] = "System.WorkItemType";
            fields[2] = "System.Title";
            fields[3] = "System.Description";
            fields[4] = "System.AssignedTo";
            fields[5] = "System.IterationPath";
            #endregion

            VssBasicCredential vssCredential = new VssBasicCredential("", txtProjectPAToken.Text);
            List<WorkItemInstance> theWorkItems =
                zfLibrary.GetAllWorkItemsFromAzureDevOps(new Uri(txtProjectURI.Text), vssCredential, queryWiqlString,
                    fields);

            //Iterate through the results
            foreach (var workItemInstance in theWorkItems)
            {
                /* The TreeView should look as follows:
                     |-->Main Node
                     |---->CommentSection
                     |------->Comment Text on its own level
                 */
                TreeNode mainNode = adoMainTreeView.Nodes.Add(string.Format($"ID: {workItemInstance.AdoWorkItem.Id} - Title: {workItemInstance.AdoWorkItem.Fields["System.Title"]}"));

                //Check the comments list, iterate through result
                for (int i = 0; i < workItemInstance.AdoCommentsList.Count; i++)
                {
                    //Get the result
                    TreeNode commentSectionNode = new TreeNode();
                    commentSectionNode.Text = string.Format($"{workItemInstance.AdoCommentsList[i].RevisedDate.ToLongDateString()} -" +
                                                            $" {workItemInstance.AdoCommentsList[i].RevisedBy.Name}");
                    mainNode.Nodes.Add(commentSectionNode);
                    //Add the comments Text at the chile node level
                    TreeNode commentTextTreeNode = new TreeNode(workItemInstance.AdoCommentsList[i].Text);
                    commentSectionNode.Nodes.Add(commentTextTreeNode);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
