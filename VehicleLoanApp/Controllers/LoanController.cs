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
    public class LoanController : ControllerBase
    {
        VehicleLoanDatabaseContext db = new VehicleLoanDatabaseContext();

        /// <summary>
        /// Getting the Basic Applicant Details of the user who have registered into the application
        /// </summary>
        /// <returns>Applicants Details</returns>
        [HttpGet]
        [Route("ApplicantDetails")]
        public IActionResult GetApplicantDetails()
        {
            var data = from ad in db.ApplicantDetails select new { CId =ad.CustomerId, Fname = ad.FirstName, Lname = ad.LastName, Age = ad.Age,
            ContactNo = ad.ContactNo, EmailId = ad.EmailId, address = ad.Address, State = ad.State, City = ad.City, Pincode = ad.Pincode, 
            UserId = ad.UserId, Password = ad.Password};
            return Ok(data);
        }

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
        /// Getting Loan Details of all the loans present in the database
        /// </summary>
        /// <returns>Loan Details of multiple Loans</returns>
        [HttpGet]
        [Route("LoanDetails")]
        public IActionResult GetLoanDetails()
        {
            var data = from ld in db.LoanDetails
                       select new
                       {
                           LId = ld.LoanId,
                           LAmount = ld.LoanAmount,
                           LInterestRate = ld.LoanInterestRate,
                           LTenure = ld.LoanTenure,
                           LStatusId = ld.StatusId,
                           LCustomerId = ld.CustomerId
                       };
            return Ok(data);
        }


        /// <summary>
        /// Method called when admin approves a particular loan. Changes the Status ID of a particular loan from 1(Default) to 3(Approved)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success/Failure</returns>
        [HttpPut]
        [Route("ApproveLoan/{id}")]
        public IActionResult ApproveLoan(int id)
        {
            if (ModelState.IsValid)
            {
                LoanDetail old = db.LoanDetails.Find(id);
                if (old.StatusId != 1) return BadRequest("Loan already Reviewed");
                old.StatusId = 3;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest("Unable to Approve Loan.");
        }


        /// <summary>
        /// Method called when admin rejects a particular loan. Changes the Status ID of a particular loan from 1(Default) to 2(Rejected)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Success/Failure</returns>[HttpPut]
        [Route("RejectLoan/{id}")]
        public IActionResult RejectLoan(int id)
        {
            if (ModelState.IsValid)
            {
                LoanDetail old = db.LoanDetails.Find(id);
                if (old.StatusId != 1) return BadRequest("Loan already Reviewed");
                old.StatusId = 2;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest("Unable to Reject Loan.");
        }


        /// <summary>
        /// Getting the Pending List of all the loans in the database which requires action from the admin to either approve/reject. Pending Loans
        /// with the default status ID of 1. Calls a Stored Procedure which returns the Loan Details with status ID of 1(Pending).
        /// </summary>
        /// <returns>List of Pending Loan Applications details.</returns>
        [HttpGet]
        [Route("PendingList")]
        public IActionResult GetPendingList()
        {
            var data = db.PendingList.FromSqlInterpolated<PendingList>($"PendingList");
            return Ok(data);
        }

        /// <summary>
        /// Getting the Pending List of all the loans in the database which has already been approved by the admin. Accepted Loans
        /// with the status ID of 3. Calls a Stored Procedure which returns the Loan Details with status ID of 3(Accepted).
        /// </summary>
        /// <returns>List of Accepted Loan Applications details.</returns>[HttpGet]
        [Route("AcceptedList")]
        public IActionResult GetAcceptedList()
        {
            var data = db.AcceptedList.FromSqlInterpolated<AcceptedList>($"AcceptedList");
            return Ok(data);
        }


        /// <summary>
        /// Getting the Pending List of all the loans in the database which has already been rejected by the admin. Rejected Loans
        /// with the status ID of 2. Calls a Stored Procedure which returns the Loan Details with status ID of 2(Rejected).
        /// </summary>
        /// <returns>List of Pending Loan Applications details.</returns>[HttpGet]
        [Route("RejectedList")]
        public IActionResult GetRejectedList()
        {
            var data = db.RejectedList.FromSqlInterpolated<RejectedList>($"RejectedList");
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
    }
}
