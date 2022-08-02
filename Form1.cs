/*
Опишите класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, объем топливного бака, скорость.
Опишите метод, с помощью которого можно вычислить сколько автомобиль может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент.
Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров и груза.
Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько автомобиль его преодолеет.
Реализуйте на его основе классы легковой автомобиль, грузовой автомобиль, спортивный автомобиль. 

У легкового автомобиля добавьте параметр количество перевозимых пассажиров. На основе данного параметра может изменяться запас хода.
Предусмотрите проверку на допустимое количество пассажиров. Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%. 

Класс грузового автомобиля дополните параметром грузоподъемность. Также, как и у легкового автомобиля, грузоподъемность влияет на запас хода автомобиля.
Дополните класс проверкой может ли автомобиль принять полный груз на борт. Каждые дополнительные 200кг веса уменьшают запас хода на 4%
*/

namespace Cars
{
    public enum CarType { Passenger, Freight, Sport }

    public abstract class Car
    {
        protected CarType type;
        protected float AverageFuelConsumption; // Средний расход топлива литры/100км
        protected float FuelTankVolume; // Объем топливного бака
        float Speed;

        public Car(float averageFuelConsumption,float fuelTankVolume)
        {
            //Type = type;
            AverageFuelConsumption = averageFuelConsumption;
            FuelTankVolume = fuelTankVolume;
        }

        public float DistanceByRemainingFuel(float remainingFuel)
        {
            return AverageFuelConsumption * remainingFuel;
        }

        public float DistanceByFullFuelTank() // Для полного бака
        {
            return DistanceByRemainingFuel(FuelTankVolume);
        }
    }

    public class PassengerCar:Car
    {
        public int PassengerCount; // Количество пассажиров

        public PassengerCar(float averageFuelConsumption,float fuelTankVolume)
            : base(averageFuelConsumption,fuelTankVolume)
        {
            type = CarType.Passenger;
        }

        public float PowerReserve()
        {
            float powerReserveWithoutPassengers = AverageFuelConsumption * FuelTankVolume; // Запас хода в км без пассажиров
            return powerReserveWithoutPassengers * (float)(1 - PassengerCount * 0.06); ;
        }
    }

    public class FreightCar:Car
    {
        float LoadCapacity; // Грузоподъемность в тоннах
        public FreightCar(float averageFuelConsumption,float fuelTankVolume,float loadCapacity)
            : base(averageFuelConsumption,fuelTankVolume)
        {
            type = CarType.Freight;
            LoadCapacity = loadCapacity;

        }

        public bool ChackLoadCapacity(int freightWeight) // Mасса в кг
        {
            return LoadCapacity >= freightWeight / 1000;
        }

        public float PowerReserve(int freightWeight) // Mасса в кг
        {
            float powerReserveWithoutFreight = AverageFuelConsumption * FuelTankVolume; // Запас хода в км без груза
            float powerReserveByOneKilogram = (float)0.04 / 200; // Если 200 кг уменьшают запас хода на 4%, то 1 кг в 200 раз меньше
            return powerReserveWithoutFreight - powerReserveWithoutFreight * (float)(powerReserveByOneKilogram * freightWeight);
        }
    }

    public partial class Form1:Form
    {
        public Form1()
        {
            InitializeComponent();

            // Легковой
            var passengerCar = new PassengerCar(10,200);
            // Дистанция
            var distance = passengerCar.DistanceByFullFuelTank(); // С полным баком
            distance = passengerCar.DistanceByRemainingFuel(50); // С 50 литрами
            // Запас хода
            var powerReserve = passengerCar.PowerReserve(); // Без пассажиров
            passengerCar.PassengerCount = 1;
            powerReserve = passengerCar.PowerReserve(); // С 1 пассажиром
            passengerCar.PassengerCount = 2;
            powerReserve = passengerCar.PowerReserve(); // С 2 пассажирами




            // Грузовой
            var freightCar = new FreightCar(10,300,3);
            distance = freightCar.DistanceByFullFuelTank(); // С полным баком
            distance = freightCar.DistanceByRemainingFuel(50); // С 50 литрами

            // Проверка может ли автомобиль принять полный груз на борт
            var chackLoadCapacity = freightCar.ChackLoadCapacity(2000);
            chackLoadCapacity = freightCar.ChackLoadCapacity(4000);
            //Запас хода
            powerReserve = freightCar.PowerReserve(0); // Без груза
            powerReserve = freightCar.PowerReserve(200); // С грузом 200кг
            powerReserve = freightCar.PowerReserve(400); // С грузом 400кг
        }
    }
}