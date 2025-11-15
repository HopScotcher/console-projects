// "using" brings in an external "neighborhood" (namespace) so we can use its classes.
using System;

// LEVEL 1: The "Neighborhood" (namespace)
namespace MyFirstApp
{
    // LEVEL 2: The "Blueprint" (class) for an incomplete idea.
    // [Accessibility] [Behavior] [Type] [Name]
    public abstract class Vehicle
    {
        // LEVEL 3: Member (Data)
        // This 'static' field is "owned" by the Vehicle CLASS, not the object.
        public static int TotalVehiclesCreated = 0;

        // This 'private' field is "owned" by the OBJECT (instance).
        private int _speed; 
        
        // This is a 'public' property to safely access the private field.
        public string Name { get; set; }

        // This is a 'constructor' method. It runs when you create a 'new' object.
        public Vehicle(string name)
        {
            this.Name = name;
            _speed = 0;
            TotalVehiclesCreated++; // Increment the static counter
        }

        // LEVEL 3: Member (Behavior)
        // This is an 'abstract' method. It has no body.
        // It's a *requirement* for any class that inherits Vehicle.
        public abstract void StartEngine();

        // This is a 'void' method (returns nothing).
        public void Stop()
        {
            Console.WriteLine("Vehicle is stopping.");
            _speed = 0;
        }

        // This method 'returns' a value (an int).
        public int GetSpeed()
        {
            return _speed;
        }
    }

    // LEVEL 2: Another "Blueprint" that INHERITS from Vehicle.
    // This class *completes* the 'abstract' design.
    public class Car : Vehicle
    {
        // The constructor for Car *calls* the base (Vehicle) constructor.
        public Car(string name) : base(name)
        {
        }

        // We MUST 'override' the abstract method to provide a body (implementation).
        public override void StartEngine()
        {
            Console.WriteLine($"The {this.Name}'s car engine rumbles to life!");
        }

        // This is a new method only for the Car class.
        public void Honk()
        {
            Console.WriteLine("Beep beep!");
        }
    }


    // LEVEL 2: The main Program class.
    class Program
    {
        // LEVEL 3: The main Method.
        // [Accessibility] [Scope] [Return Type] [Name]
        // It's 'static' so the OS can run it without creating a 'Program' object.
        static void Main(string[] args)
        {
            // LEVEL 2.5: Creating an OBJECT (instance) from the 'Car' blueprint.
            Car myCar = new Car("Honda Civic");

            // We can't do this, because Vehicle is 'abstract':
            // Vehicle myVehicle = new Vehicle("Some Vehicle"); // <-- ERROR!

            // Calling 'public' instance methods on the 'myCar' object.
            myCar.StartEngine();
            myCar.Honk();
            myCar.Stop();

            // Calling a 'public' instance method that returns a value.
            int currentSpeed = myCar.GetSpeed();
            Console.WriteLine($"Current speed: {currentSpeed}");

            // Accessing a 'static' field through the CLASS name, not the object.
            Console.WriteLine($"Total vehicles created: {Vehicle.TotalVehiclesCreated}");
        }
    }
}