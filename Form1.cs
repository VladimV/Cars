/*
������� ����� ���������� � �������� ���� ������� ��������� � ���� ���� ��, �������� ������� �������, ����� ���������� ����, ��������.
������� �����, � ������� �������� ����� ��������� ������� ���������� ����� �������� �� ������ ���� ������� ��� �� ���������� ���������� ������� � ���� �� ������ ������.
����� ��� ����������� ������� ���������� � ��������� ������ ���� � ����������� �� ���������� � �����.
�����, ������� �� ������ ���������� ���������� ������� � ��������� ���������� ��������� �� ������� ���������� ��� ����������.
���������� �� ��� ������ ������ �������� ����������, �������� ����������, ���������� ����������. 

� ��������� ���������� �������� �������� ���������� ����������� ����������. �� ������ ������� ��������� ����� ���������� ����� ����.
������������� �������� �� ���������� ���������� ����������. ������ �������������� �������� ��������� ����� ���� �� �������������� 6%. 

����� ��������� ���������� ��������� ���������� ����������������. �����, ��� � � ��������� ����������, ���������������� ������ �� ����� ���� ����������.
��������� ����� ��������� ����� �� ���������� ������� ������ ���� �� ����. ������ �������������� 200�� ���� ��������� ����� ���� �� 4%
*/

namespace Cars
{
    public enum CarType { Passenger, Freight, Sport }

    public abstract class Car
    {
        protected CarType type;
        protected float AverageFuelConsumption; // ������� ������ ������� �����/100��
        protected float FuelTankVolume; // ����� ���������� ����
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

        public float DistanceByFullFuelTank() // ��� ������� ����
        {
            return DistanceByRemainingFuel(FuelTankVolume);
        }
    }

    public class PassengerCar:Car
    {
        public int PassengerCount; // ���������� ����������

        public PassengerCar(float averageFuelConsumption,float fuelTankVolume)
            : base(averageFuelConsumption,fuelTankVolume)
        {
            type = CarType.Passenger;
        }

        public float PowerReserve()
        {
            float powerReserveWithoutPassengers = AverageFuelConsumption * FuelTankVolume; // ����� ���� � �� ��� ����������
            return powerReserveWithoutPassengers * (float)(1 - PassengerCount * 0.06); ;
        }
    }

    public class FreightCar:Car
    {
        float LoadCapacity; // ���������������� � ������
        public FreightCar(float averageFuelConsumption,float fuelTankVolume,float loadCapacity)
            : base(averageFuelConsumption,fuelTankVolume)
        {
            type = CarType.Freight;
            LoadCapacity = loadCapacity;

        }

        public bool ChackLoadCapacity(int freightWeight) // M���� � ��
        {
            return LoadCapacity >= freightWeight / 1000;
        }

        public float PowerReserve(int freightWeight) // M���� � ��
        {
            float powerReserveWithoutFreight = AverageFuelConsumption * FuelTankVolume; // ����� ���� � �� ��� �����
            float powerReserveByOneKilogram = (float)0.04 / 200; // ���� 200 �� ��������� ����� ���� �� 4%, �� 1 �� � 200 ��� ������
            return powerReserveWithoutFreight - powerReserveWithoutFreight * (float)(powerReserveByOneKilogram * freightWeight);
        }
    }

    public partial class Form1:Form
    {
        public Form1()
        {
            InitializeComponent();

            // ��������
            var passengerCar = new PassengerCar(10,200);
            // ���������
            var distance = passengerCar.DistanceByFullFuelTank(); // � ������ �����
            distance = passengerCar.DistanceByRemainingFuel(50); // � 50 �������
            // ����� ����
            var powerReserve = passengerCar.PowerReserve(); // ��� ����������
            passengerCar.PassengerCount = 1;
            powerReserve = passengerCar.PowerReserve(); // � 1 ����������
            passengerCar.PassengerCount = 2;
            powerReserve = passengerCar.PowerReserve(); // � 2 �����������




            // ��������
            var freightCar = new FreightCar(10,300,3);
            distance = freightCar.DistanceByFullFuelTank(); // � ������ �����
            distance = freightCar.DistanceByRemainingFuel(50); // � 50 �������

            // �������� ����� �� ���������� ������� ������ ���� �� ����
            var chackLoadCapacity = freightCar.ChackLoadCapacity(2000);
            chackLoadCapacity = freightCar.ChackLoadCapacity(4000);
            //����� ����
            powerReserve = freightCar.PowerReserve(0); // ��� �����
            powerReserve = freightCar.PowerReserve(200); // � ������ 200��
            powerReserve = freightCar.PowerReserve(400); // � ������ 400��
        }
    }
}