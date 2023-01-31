using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace NextHome.Models
{
    public interface IEntity
    {
        DateTime CreationDate { get; set; }
    }

    [TableName("InquiryRequest")]
    [PrimaryKey("Id", autoIncrement =true)]
    public class InquiryRequest : IEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

        //this property is trap for bot requests
        [Ignore]
        public string PhoneNo { get; set; }
    }

    [TableName("Applicant")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class Applicant : IEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }
        public string VacancyName { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CVFileName { get; set; }
        public DateTime CreationDate { get; set; }

        //this property is trap for bot requests
        [Ignore]
        public string PhoneNo { get; set; }
    }
}