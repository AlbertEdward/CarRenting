﻿using CarRenting.Data;

namespace CarRenting.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly CarRentingDbContext data;

        public StatisticsService(CarRentingDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUser = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalRents = totalUser,
                TotalUsers = totalCars,
            };
        }

    }
}
