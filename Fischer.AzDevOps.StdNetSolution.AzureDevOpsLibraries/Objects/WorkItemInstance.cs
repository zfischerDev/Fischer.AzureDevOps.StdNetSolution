using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Objects
{
    public class WorkItemInstance
    {
        public string WorkItemId { get; set; }

        public WorkItem AdoWorkItem { get; set; }

        public List<WorkItemStateChange> WorkItemUpdateList { get; set; }

        public List<WorkItemComment> AdoCommentsList { get; set; }

        public string ErrorMessage { get; set; }

    }
}
