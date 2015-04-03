using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Geometry;

namespace DynamoABB
{
    internal class RobotUtils
    {
        /// <summary>
        /// Create a list of 4 Quaternian values from a Plane based on it's principle vectors.
        /// </summary>
        /// <param name="plane">The plane.</param>
        ///  <param name="list of doubles ">The 4 Quaternian values for the Plane.</param>
        /// <returns></returns>

        public static List<double> PlaneToQuaternian(Plane plane)
        {

            //implemented PlaneToQuaternian based on logic from the 
            //Design Robotics Group @ Harvard Gsd with contributions from Sola Grantham, Anthony Kane, Nathan King, Jonathan Grinham, and others. 
            //converted to Dynamo_ABB utility function at the Virginia Tech Robot Summit

            //Point origin = plane.Origin;
            Vector xVect = plane.XAxis;
            Vector yVect = plane.YAxis;
            Vector zVect = plane.Normal;

            double s, trace;
            double x1, x2, x3, y1, y2, y3, z1, z2, z3;
            double q1, q2, q3, q4;

            x1 = xVect.X;
            x2 = xVect.Y;
            x3 = xVect.Z;
            y1 = yVect.X;
            y2 = yVect.Y;
            y3 = yVect.Z;
            z1 = zVect.X;
            z2 = zVect.Y;
            z3 = zVect.Z;

            trace = x1 + y2 + z3 + 1;

            if (trace > 0.00001)
            {
                // s = (trace) ^ (1 / 2) * 2
                s = Math.Sqrt(trace) * 2;
                q1 = s / 4;
                q2 = (-z2 + y3) / s;
                q3 = (-x3 + z1) / s;
                q4 = (-y1 + x2) / s;
            }
            else if (x1 > y2 && x1 > z3)
            {
                //s = (x1 - y2 - z3 + 1) ^ (1 / 2) * 2
                s = Math.Sqrt(x1 - y2 - z3 + 1) * 2;
                q1 = (z2 - y3) / s;
                q2 = s / 4;
                q3 = (y1 + x2) / s;
                q4 = (x3 + z1) / s;

            }
            else if (y2 > z3)
            {
                //s = (-x1 + y2 - z3 + 1) ^ (1 / 2) * 2
                s = Math.Sqrt(-x1 + y2 - z3 + 1) * 2;
                q1 = (x3 - z1) / s;
                q2 = (y1 + x2) / s;
                q3 = s / 4;
                q4 = (z2 + y3) / s;
            }

            else
            {
                //s = (-x1 - y2 + z3 + 1) ^ (1 / 2) * 2
                s = Math.Sqrt(-x1 - y2 + z3 + 1) * 2;
                q1 = (y1 - x2) / s;
                q2 = (x3 + z1) / s;
                q3 = (z2 + y3) / s;
                q4 = s / 4;
            }
            List<double> quatDoubles = new List<double>();
            quatDoubles.Add(q1);
            quatDoubles.Add(q2);
            quatDoubles.Add(q3);
            quatDoubles.Add(q4);
            return quatDoubles;
        }
    }
}
