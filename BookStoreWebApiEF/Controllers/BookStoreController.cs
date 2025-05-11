using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Model;
using Model.Request;
using System.Runtime.InteropServices;

namespace BookStoreWebApiEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        public BookStoreController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddNewBook")]
        public IActionResult POST(TblBookstore request)
        {
            try
            {
                _dbContext.TblBookstores.Add(request);
                _dbContext.SaveChanges();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ViewAllBooks")]
        public IActionResult GET()
        {
            try
            {
                var data = _dbContext.TblBookstores.Select(x => x).ToList();
                List<TblBookstore> tblBookstores = new List<TblBookstore>();
                foreach(var d in data)
                {
                    TblBookstore bookstore = new TblBookstore();
                    bookstore.Id = d.Id;
                    bookstore.BookCategoryId = d.BookCategoryId;
                    bookstore.BookCategory = d.BookCategory;
                    bookstore.BookName = d.BookName;
                    bookstore.Stock = d.Stock;
                    tblBookstores.Add(bookstore);
                }
                return Ok(tblBookstores);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateBook")]
        public IActionResult UPDATE(TblBookstore request)
        {
            try
            {
                var existingBookstore = _dbContext.TblBookstores.FirstOrDefault(x => x.Id == request.Id);
                if(existingBookstore != null)
                {
                    existingBookstore.BookCategoryId = request.BookCategoryId;
                    existingBookstore.BookCategory = request.BookCategory;
                    existingBookstore.BookName = request.BookName;
                    existingBookstore.Stock = request.Stock;

                    _dbContext.Entry(existingBookstore).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                return Created();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteBook")]
        public IActionResult DELETE([FromQuery] int id)
        {
            try
            {
                var existingBookstore = _dbContext.TblBookstores.FirstOrDefault(x => x.Id == id);
                if (existingBookstore != null)
                { 
                    _dbContext.TblBookstores.Remove(existingBookstore);
                    _dbContext.SaveChanges();
                }
                return Created();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("BuyBook")]
        public IActionResult BUY(BuyBookRequest request)
        {
            string Message = "";
            try
            {
                var count = _dbContext.TblBookstores.Where(x => x.Id == request.bookId && x.BookCategoryId == request.catagoryId).Count();
                if (count > 0)
                {

                    var bookstore = _dbContext.TblBookstores
                    .FirstOrDefault(x => x.Id == request.bookId && x.BookCategoryId == request.catagoryId);
                    if (bookstore != null)
                    {
                        int newQuantity = Convert.ToInt32(bookstore.Stock) - request.quantity;
                        if (newQuantity > 0)
                        {
                            bookstore.Stock = newQuantity.ToString();
                            _dbContext.SaveChanges();

                            Message = "Success.!";

                        }
                        else
                        {
                            Message = "Not enough to stock to buy.!";

                        }

                    }

                }
                else
                {

                    Message = "No record found.!";

                }
                return Ok(new
                {
                    Message = Message
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //added  for trying rabbitMQ
        //[HttpPost("publish")]
        //public IActionResult PublishMessage([FromBody] string message)
        //{
        //    var publisher = new RabbitMqPublisher();
        //    publisher.Publish(message);
        //    return Ok("Message published: " + message);
        //}

    }
}
