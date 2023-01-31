using NextHome.Helpers;
using NextHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Core.Persistence;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace NextHome.Controllers
{
    [PluginController("WebRequests")]
    public class WRApiController : UmbracoAuthorizedJsonController
    {
        #region Website Requests
        public IEnumerable<InquiryRequest> GetAllRquests()
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.GetAll<InquiryRequest>();
        }

        public PagedResult<InquiryRequest> GetPagedRequests(int itemsPerPage, int pageNumber, string sortColumn, string sortOrder, string searchTerm)
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.GetByPage<InquiryRequest>(itemsPerPage, pageNumber, sortColumn, sortOrder, searchTerm);
        }

        public int InsertInquiryRequest(InquiryRequest request)
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.InsertEntity<InquiryRequest>(request);
        }
        #endregion

        #region Applicants
        public IEnumerable<Applicant> GetAllApplicants()
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.GetAll<Applicant>();
        }

        public PagedResult<Applicant> GetPagedApplicants(int itemsPerPage, int pageNumber, string sortColumn, string sortOrder, string searchTerm)
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.GetByPage<Applicant>(itemsPerPage, pageNumber, sortColumn, sortOrder, searchTerm);
        }

        public int InsertApplicant(Applicant Applicant)
        {
            WRDataAccess db = new WRDataAccess(DatabaseContext.Database);
            return db.InsertEntity<Applicant>(Applicant);
        }
        #endregion
    }

}
