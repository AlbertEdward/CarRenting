namespace CarRenting.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int CarMakeMaxLength = 20;
            public const int CarModelMaxLength = 20;
            public const int CarDescriptionMinLength = 50;
            public const int CarDescriptionMaxLength = 10000;
            public const int CarYearMinValue = 1960;
            public const int CarYearMaxValue = 2030;
        }

        public class User
        {
            public const int FullNameMaxLength = 50;
        }
    }
}
