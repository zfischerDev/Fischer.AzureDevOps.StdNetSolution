/* Notes:
 * Added the following NuGet packages:
 * 1. Microsoft.VisualStudio.Services.Client
 * 2. Microsoft.TeamFoundation.Client
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Objects;

namespace Fischer.AzDevOps.StdNetSolution.AzureDevOpsLibraries.Libraries
{
    public class ADOProcessingLib
    {
        #region Public Methods

        #region GetAllWorkItemsFromAzureDevOps - string arguments
        /// <summary>
        /// 
        /// </summary>
        /// <param name="azureDevOpsUri">URI as a string</param>
        /// <param name="personalAccessToken">Personal Access Token as a string</param>
        /// <param name="azureDevOpsWiqlString">The DevOps Query as a string</param>
        /// <param name="fields">The specific fields to show as an array</param>
        /// <returns></returns>
        public List<WorkItemInstance> GetAllWorkItemsFromAzureDevOps(string azureDevOpsUri, string personalAccessToken,
string azureDevOpsWiqlString, string[] fields)
        {
            List<WorkItemInstance> workItemsAndCommentsList = new List<WorkItemInstance>();
            try
            {
                //Set the Uri and Access token
                Uri devOpsUri = new Uri(azureDevOpsUri);
                VssBasicCredential credentials = new VssBasicCredential("", personalAccessToken);
                using (WorkItemTrackingHttpClient workItemTrackingHttpClient =
         new WorkItemTrackingHttpClient(devOpsUri, credentials))
                {
                    //Retrieve the work items based on the Wiql string provided
                    WorkItemQueryResult workItemQueryResult = WorkItemResponse(devOpsUri, credentials, azureDevOpsWiqlString);
                    var selectedWorkItemIDs = workItemQueryResult.WorkItems.Select(workItemReference => workItemReference.Id).ToList();
                    var retrievedWorkItems = workItemTrackingHttpClient.GetWorkItemsAsync(selectedWorkItemIDs, fields, workItemQueryResult.AsOf).Result;

                    foreach (WorkItem workItem in retrievedWorkItems)
                    {
                        int workItemId = (int)workItem.Id;
                        List<WorkItemStateChange> WorkItemStateChangesList = new List<WorkItemStateChange>();
                        List<WorkItemUpdate> updates = workItemTrackingHttpClient.GetUpdatesAsync(workItemId).Result;

                        foreach (var WorkItemStateChange in updates)
                        {
                            WorkItemStateChange myWorkItemStateChange = new WorkItemStateChange();
                            myWorkItemStateChange.WorkItemStateChangeId = WorkItemStateChange.Id.ToString();
                            myWorkItemStateChange.Revision = WorkItemStateChange.Rev.ToString();
                            myWorkItemStateChange.RevisedBy = WorkItemStateChange.RevisedBy.Name;
                            if (WorkItemStateChange.Fields != null && WorkItemStateChange.Fields.ContainsKey("System.Reason"))
                            {
                                WorkItemFieldUpdate workItemUpdateReason = (WorkItemFieldUpdate)WorkItemStateChange.Fields["System.Reason"];
                                myWorkItemStateChange.ReasonOldValue = workItemUpdateReason.OldValue != null ? workItemUpdateReason.OldValue.ToString() : "OLD VALUE IS NULL";
                                myWorkItemStateChange.ReasonNewValue = workItemUpdateReason.NewValue != null ? workItemUpdateReason.NewValue.ToString() : "NEW VALUE IS NULL";
                                myWorkItemStateChange.RevisedDate = WorkItemStateChange.RevisedDate.ToShortDateString();
                                WorkItemStateChangesList.Add(myWorkItemStateChange);
                            }
                        }

                        WorkItemInstance adoWorkItemInstanceClass = new WorkItemInstance();
                        adoWorkItemInstanceClass.WorkItemUpdateList = WorkItemStateChangesList;

                        //Get the WorkItemId add it to the main record
                        adoWorkItemInstanceClass.WorkItemId = workItem.Fields["System.Id"].ToString();

                        //Add the work item to the class
                        adoWorkItemInstanceClass.AdoWorkItem = workItem;

                        WorkItemComments workItemComments = workItemTrackingHttpClient.GetCommentsAsync(workItem.Id.Value).Result;
                        //adoWorkItemInstanceClass.AdoCommentsList = new List<WorkItemComment>();
                        List<WorkItemComment> commentsList = new List<WorkItemComment>();

                        foreach (WorkItemComment commentItem in workItemComments.Comments)
                        {
                            //Add the comment to the list
                            commentsList.Add(commentItem);
                        }

                        //Add the comments list to the WorkItemInstance class
                        adoWorkItemInstanceClass.AdoCommentsList = commentsList;
                        workItemsAndCommentsList.Add(adoWorkItemInstanceClass);
                    }
                }
            }
            catch (AggregateException aggException)
            {
                WorkItemInstance aggExceptionWorkItem = new WorkItemInstance();
                aggExceptionWorkItem.ErrorMessage = aggException.InnerException.ToString();
                workItemsAndCommentsList.Add(aggExceptionWorkItem);
            }
            catch (VssException vssException)
            {
                WorkItemInstance vssExceptionWorkItem = new WorkItemInstance();
                vssExceptionWorkItem.ErrorMessage = vssException.InnerException.ToString();
                workItemsAndCommentsList.Add(vssExceptionWorkItem);
            }
            //Output is the Work Items and their comments 
            return workItemsAndCommentsList;
        }
        #endregion

        #region GetAllWorkItemsFromAzureDevOps - URI and VSSBasicCredentials version
        /// <summary>
        /// 
        /// </summary>
        /// <param name="azureDevOpsUri">The URI as a URI</param>
        /// <param name="personalAccessToken">The Personal Access Token as a VSSBasicCredential</param>
        /// <param name="azureDevOpsWiqlString">The DevOps Query as a string</param>
        /// <param name="fields">The specific fields to show as an array</param>
        /// <returns></returns>
        public List<WorkItemInstance> GetAllWorkItemsFromAzureDevOps(Uri azureDevOpsUri, VssBasicCredential personalAccessToken,
    string azureDevOpsWiqlString, string[] fields)
        {
            List<WorkItemInstance> workItemsAndCommentsList = new List<WorkItemInstance>();
            try
            {
                using (WorkItemTrackingHttpClient workItemTrackingHttpClient =
         new WorkItemTrackingHttpClient(azureDevOpsUri, personalAccessToken))
                {
                    //Retrieve the work items based on the Wiql string provided
                    WorkItemQueryResult workItemQueryResult = WorkItemResponse(azureDevOpsUri, personalAccessToken, azureDevOpsWiqlString);
                    var selectedWorkItemIDs = workItemQueryResult.WorkItems.Select(workItemReference => workItemReference.Id).ToList();
                    var retrievedWorkItems = workItemTrackingHttpClient.GetWorkItemsAsync(selectedWorkItemIDs, fields, workItemQueryResult.AsOf).Result;

                    foreach (WorkItem workItem in retrievedWorkItems)
                    {
                        int workItemId = (int)workItem.Id;
                        List<WorkItemStateChange> WorkItemStateChangesList = new List<WorkItemStateChange>();
                        List<WorkItemUpdate> updates = workItemTrackingHttpClient.GetUpdatesAsync(workItemId).Result;

                        foreach (var WorkItemStateChange in updates)
                        {
                            WorkItemStateChange myWorkItemStateChange = new WorkItemStateChange();
                            myWorkItemStateChange.WorkItemStateChangeId = WorkItemStateChange.Id.ToString();
                            myWorkItemStateChange.Revision = WorkItemStateChange.Rev.ToString();
                            myWorkItemStateChange.RevisedBy = WorkItemStateChange.RevisedBy.Name;
                            if (WorkItemStateChange.Fields != null && WorkItemStateChange.Fields.ContainsKey("System.Reason"))
                            {
                                WorkItemFieldUpdate workItemUpdateReason = (WorkItemFieldUpdate)WorkItemStateChange.Fields["System.Reason"];
                                myWorkItemStateChange.ReasonOldValue = workItemUpdateReason.OldValue != null ? workItemUpdateReason.OldValue.ToString() : "OLD VALUE IS NULL";
                                myWorkItemStateChange.ReasonNewValue = workItemUpdateReason.NewValue != null ? workItemUpdateReason.NewValue.ToString() : "NEW VALUE IS NULL";
                                myWorkItemStateChange.RevisedDate = WorkItemStateChange.RevisedDate.ToShortDateString();
                                WorkItemStateChangesList.Add(myWorkItemStateChange);
                            }
                        }

                        WorkItemInstance adoWorkItemInstanceClass = new WorkItemInstance();
                        adoWorkItemInstanceClass.WorkItemUpdateList = WorkItemStateChangesList;

                        //Get the WorkItemId
                        adoWorkItemInstanceClass.WorkItemId = workItem.Fields["System.Id"].ToString();

                        //Add the work item to the class
                        adoWorkItemInstanceClass.AdoWorkItem = workItem;
                        var wkIdVal = workItem.Id.Value;
                        WorkItemComments workItemComments = workItemTrackingHttpClient.GetCommentsAsync(workItem.Id.Value).Result;
                        List<WorkItemComment> commentsList = new List<WorkItemComment>();

                        foreach (WorkItemComment commentItem in workItemComments.Comments)
                        {
                            //Add the comment to the list
                            commentsList.Add(commentItem);
                        }

                        //Add the comments list to the WorkItemInstance class
                        adoWorkItemInstanceClass.AdoCommentsList = commentsList;
                        workItemsAndCommentsList.Add(adoWorkItemInstanceClass);
                    }
                }
            }
            catch (AggregateException aggException)
            {
                WorkItemInstance aggExceptionWorkItem = new WorkItemInstance();
                aggExceptionWorkItem.ErrorMessage = aggException.InnerException.ToString();
                workItemsAndCommentsList.Add(aggExceptionWorkItem);
            }
            catch (VssException vssException)
            {
                WorkItemInstance vssExceptionWorkItem = new WorkItemInstance();
                vssExceptionWorkItem.ErrorMessage = vssException.InnerException.ToString();
                workItemsAndCommentsList.Add(vssExceptionWorkItem);
            }
            //Output is the Work Items and their comments 
            return workItemsAndCommentsList;
        }
        #endregion

        #endregion

        #region Private Methods
        private WorkItemQueryResult WorkItemResponse(Uri azureDevOpsUri, VssBasicCredential personalAccessToken, string azureDevOpsWiqlString)
        {
            WorkItemQueryResult workItemQueryResult;
            Wiql azureDevOpsWiqlQuery = new Wiql()
            {
                Query = azureDevOpsWiqlString
            };

            using (WorkItemTrackingHttpClient workItemTrackingHttpClient =
            new WorkItemTrackingHttpClient(azureDevOpsUri, personalAccessToken))
            {
                //execute the query to get the list of work items in the results
                workItemQueryResult =
                workItemTrackingHttpClient.QueryByWiqlAsync(azureDevOpsWiqlQuery).Result;
            }

            return workItemQueryResult;
        }

        #endregion
    }
}
