using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLoanApp.Models;
using VehicleLoanApp.ViewModel;

namespace VehicleLoanApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        VehicleLoanDatabaseContext db = new VehicleLoanDatabaseContext();


        /// <summary>
        /// Getting the basic Applicant details of a particular user who has been registered into the application based on their customerID as
        ///a Parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of a particular applicant based on their CId</returns>
        [HttpGet]
        [Route("ApplicantDetails/{id}")]
        public IActionResult GetApplicantDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Can't be Null");
            }

            var data = (from ad in db.ApplicantDetails
                        where ad.CustomerId == id
                        select new
                        {
                            CId = ad.CustomerId,
                            Fname = ad.FirstName,
                            Lname = ad.LastName,
                            Age = ad.Age,
                            Gender = ad.Gender,
                            ContactNo = ad.ContactNo,
                            EmailId = ad.EmailId,
                            address = ad.Address,
                            State = ad.State,
                            City = ad.City,
                            Pincode = ad.Pincode,
                            UserId = ad.UserId,
                            Password = ad.Password
                        }).FirstOrDefault();


            if (data == null)
            {
                return NotFound($"Department {id} not present");
            }
            return Ok(data);
        }


        /// <summary>
        /// Getting the entire details of a particular application of a particular loan from all the tables which possess the details of a
        /// particular loan. Highly detailed data of a particular loan filtered based on the Id of a Customer. Calls a Stored Procedure which
        /// extracts data from all the tables based on the customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON object containing details of a particular application</returns>
        [HttpGet]
        [Route("ApplicationDetailed/{id}")]
        public IActionResult GetApplicationDetailed(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Can't be Null");
            }

            var data = db.ApplicationDetailed.FromSqlInterpolated<ApplicationDetailed>($"ApplicationDetailed {id}").AsEnumerable().FirstOrDefault();

            if (data == null)
            {
                return NotFound($"Application of Customer with ID: {id} is not present");
            }
            return Ok(data);
        }

        
        /// <summary>
        /// Validation method which verifies whether an user can apply for a loan based on their previous application status. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>0/1</returns>
        [HttpGet]
        [Route("ApplyLoanValidation/{id}")]
        public IActionResult GetApplyLoanValidation(int? id)
        {
            if (id == null)
            {
                return BadRequest("Login to Apply for Loan");
            }
            int y = 1;
            int n = 0;
            var data = (from loan in db.LoanDetails where loan.CustomerId == id select new { Id = loan.LoanId }).FirstOrDefault();
            //var data = db.Depts.Where(d => d.Id == id).Select(d => new { Id = d.Id, Name = d.Dname, Location = d.Location }).FirstOrDefault();
            if (data == null)
            {
                return Ok($"{y}");
            }
            return Ok($"{n}");
        }


        /// <summary>
        /// Validation method which verifies whether an user can view his application status for a loan based on their applied status. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>0/1</returns>
        [HttpGet]
        [Route("ApplicationStatusValidation/{id}")]
        public IActionResult GetApplicationStatusValidation(int? id)
        {
            if (id == null)
            {
                return BadRequest("Login to Apply for Loan");
            }
            int y = 1;
            int n = 0;
            var data = (from loan in db.LoanDetails where loan.CustomerId == id select new { Id = loan.LoanId }).FirstOrDefault();
            //var data = db.Depts.Where(d => d.Id == id).Select(d => new { Id = d.Id, Name = d.Dname, Location = d.Location }).FirstOrDefault();
            if (data == null)
            {
                return Ok($"{y}");
            }
            return Ok($"{n}");
        }

        
        /// <summary>
        /// Posting the documents data to the table received from the client.
        /// </summary>
        /// <param name="idoc"></param>
        /// <returns>Success/Failure</returns>
        [HttpPost]
        [Route("AddDocuments")]
        public IActionResult PostIdentityDocuments(IdentityDocument idoc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*db.Depts.Add(dept);
                    db.SaveChanges();*/
                    db.Database.ExecuteSqlInterpolated($"adddocs {idoc.Adharcard}, {idoc.Pancard}, {idoc.Photo}, {idoc.Salaryslip}, {idoc.CustomerId}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Created("Documents Successfully Uploaded", idoc);
        }
    }
}
