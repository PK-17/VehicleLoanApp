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
        [HttpGet]
        [Route("ApplicantDetails")]
        public IActionResult GetApplicantDetails()
        {
            var data = from ad in db.ApplicantDetails select new { CId =ad.CustomerId, Fname = ad.FirstName, Lname = ad.LastName, Age = ad.Age,
            ContactNo = ad.ContactNo, EmailId = ad.EmailId, address = ad.Address, State = ad.State, City = ad.City, Pincode = ad.Pincode, 
            UserId = ad.UserId, Password = ad.Password};
            return Ok(data);
        }

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


        [HttpPut]
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


        [HttpGet]
        [Route("PendingList")]
        public IActionResult GetPendingList()
        {
            var data = db.PendingList.FromSqlInterpolated<PendingList>($"PendingList");
            return Ok(data);
        }

        [HttpGet]
        [Route("AcceptedList")]
        public IActionResult GetAcceptedList()
        {
            var data = db.AcceptedList.FromSqlInterpolated<AcceptedList>($"AcceptedList");
            return Ok(data);
        }

        [HttpGet]
        [Route("RejectedList")]
        public IActionResult GetRejectedList()
        {
            var data = db.RejectedList.FromSqlInterpolated<RejectedList>($"RejectedList");
            return Ok(data);
        }

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
