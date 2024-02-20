using ExploreJordan.Data;
using ExploreJordan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

public class ReservationController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult ReserveCar(int carId, string startDate, string endDate)
    {
        Car car = _dbContext.Cars.Find(carId);

        if (car != null)
        {
            DateTime parsedStartDate;
            DateTime parsedEndDate;

            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartDate) &&
                DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndDate))
            {
                // Check if the selected dates are available
                if (IsDatesAvailable(carId, parsedStartDate, parsedEndDate))
                {
                    Reservation reservation = new Reservation
                    {
                        CarId = car.Id,
                        Car = car,
                        StartDate = parsedStartDate,
                        EndDate = parsedEndDate
                    };

                    _dbContext.Reservations.Add(reservation);
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Booking confirmed!" });
                }
                else
                {
                    return Json(new { success = false, message = "Selected dates are not available." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid date format." });
            }
        }

        return Json(new { success = false, message = "Car not found." });
    }

    private bool IsDatesAvailable(int carId, DateTime startDate, DateTime endDate)
    {
        // Check if there are any reservations that overlap with the selected dates
        var overlappingReservations = _dbContext.Reservations
            .Where(r => r.CarId == carId && !(r.EndDate < startDate || r.StartDate > endDate))
            .ToList();

        return overlappingReservations.Count == 0;
    }
}
