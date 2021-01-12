
namespace Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Objects
{
    public class WorkItemStateChange
    {
        public string WorkItemStateChangeId { get; set; }
        public string Revision { get; set; }
        public string RevisedBy { get; set; }
        public string ReasonOldValue { get; set; }
        public string ReasonNewValue { get; set; }
        public string RevisedDate { get; set; }
    }
}
