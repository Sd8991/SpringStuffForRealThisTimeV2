using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class Spring
{
    public double beginPointX, endPointX, beginPointY, endPointY;
    public double restLength, newLength, height, stiffness, radius, move, area, innerArea, length, mass;
    public double criticDamp, actualDamp, dampFactor;
    public double Fgravity, Fspring, Fres, acceleration, velocity;
    public double angularVel, angularAcc, angle;
    public double stretch = 0.0f;
    public bool hasStopped = false;

    public Spring(Vector2 beginPoint, Vector2 endPoint, int nrOfWinding, float radius, float stretch, float materialThickness, float mass, float dampFactor, float displacmentThingy = 69000, float move = 0)
    {
        this.beginPointX = beginPoint.X;
        this.endPointX = endPoint.X;
        this.beginPointY = beginPoint.Y;
        this.radius = radius;
        this.stretch = stretch;
        this.move = move;
        this.mass = mass;
        this.dampFactor = dampFactor;
        height = endPoint.Y - beginPoint.Y;
        this.newLength = restLength;
        area = Math.PI * radius;
        innerArea = Math.PI * (radius - materialThickness);
        length = area * nrOfWinding;
        if (nrOfWinding != 0)
            stiffness = (displacmentThingy * Math.Pow(materialThickness, 4)) / (8 * nrOfWinding * Math.Pow((area - innerArea) / 2, 3));
        else stiffness = 0;
        this.restLength = height + (mass * 9.81 / stiffness);
        this.endPointY = endPoint.Y;
        this.velocity = 0;
        this.acceleration = 0;
        this.Fgravity = this.mass * 9.81;
        this.Fspring = 0;
        this.Fres = 0;
    }

    public void SLS(GameTime gameTime, float mass)//Simple Liniear Spring using Mathematics
    {
        //thyme += gameTime.ElapsedGameTime.Milliseconds / 1000;
        //move = Math.Sin(thyme * Math.Sqrt(stiffness / mass));
        newLength += move;
        stretch = newLength - restLength;
    }

    public void SLSMkII(double thyme, float mass = 5, float gravity = 9.81f)//Simple Liniear Spring using Laws of Physics
    {
        criticDamp = Math.Sqrt(mass * stiffness) * 2;
        actualDamp = dampFactor * criticDamp;
        Fgravity = mass * gravity;
        Fspring = -stiffness * stretch;
        Fres = Fgravity + Fspring - (actualDamp * velocity);
        acceleration = Fres / mass;
        velocity = velocity + acceleration * thyme;
        endPointY = endPointY + velocity * thyme;
        stretch = endPointY - restLength;
    }
}
