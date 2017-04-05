using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TriangleTypes
{
    /*
     * Provides the information about a triangle given the length of three sides
     */
    class TriangleTypes
    {
        static void Main()
        {
            // Account SID from twilio.com/console
            var accountSid = "AC************************";
            // Auth Token from twilio.com/console
            var authToken = "*************************";
            TwilioClient.Init(accountSid, authToken);

            TriangleTypes obj = new TriangleTypes();
            obj.ReadData();
            obj.GetTriangleType();

        }
        double side1;
        double side2;
        double side3;

        public void ReadData()
        {
            Console.WriteLine("Enter the first side of the Triangle : ");
            side1 = GetValidInput();
            Console.WriteLine("Enter the second side of the Triangle : ");
            side2 = GetValidInput();
            Console.WriteLine("Enter the third side of the Triangle : ");
            side3 = GetValidInput();
        }
        public void GetTriangleType()
        {
            var responseMessage = "This is a valid triangle. ";
            // Is this a valid triangle
            if ((side1 + side2) > side3 && (side1 + side3) > side2 && (side3 + side2) > side1)
            {
                if (side1 == side2 && side2 == side3)
                {
                    responseMessage += "Your triangle is an Equilateral triangle.";
                }
                else if ((side1 == side2 || side2 == side3 || side3 == side1))
                {
                    responseMessage += "Your triangle is an Isosceles triangle.";
                }
                else if (IsRightTriangle())
                {
                    responseMessage += "Your triangle is an Right triangle.";
                }
            }
            else
            {
                responseMessage = "These side lengths do not produce a valid triangle: " + side1 + " " + side2 + " " + side3;
            }

            // Send (SMS) text message
            var message = MessageResource.Create(
                to: new PhoneNumber("Your_Number"),
                from: new PhoneNumber("Twilio_Number"),
                body: responseMessage);

            Console.WriteLine(message);

            Console.ReadLine();
        }
        public double GetValidInput()
        {
            double side = 0;
            while (!double.TryParse(Console.ReadLine(), out side))
            {
                Console.WriteLine("Please Enter a numerical value for the length of a side:");
            }
            return side;
        }
        public bool IsRightTriangle()
        {
            var side1Squared = side1 * side1;
            var side2Squared = side2 * side2;
            var side3Squared = side3 * side3;
            return ((side1Squared + side2Squared) == side3Squared
                || (side1Squared + side3Squared) == side2Squared
                || (side2Squared + side3Squared) == side1Squared);
        }
    }
}
