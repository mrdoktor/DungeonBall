using UnityEngine;
using System.Collections.Generic;
public static class RSMath
{

    public static float GetAngleY(Vector3 v, bool upIsZero = false)
    {
        return FixAngle(GetAngle(v.x, v.z, upIsZero));
    }

    public static float GetAngle(float dx, float dy, bool upIsZero = false)
    {
        float a = 0;
        if (dx == 0)
        {
            if (dy > 0)
            {
                a = 90;
            }
            else
            {
                a = 270;
            }

            if (upIsZero)
            {
                return 90-a; 
            }
            else
            {
                return a;
            }
        }
        a = Mathf.Atan(1.0f * dy / dx) * 57.2957795131f;
        if (dx < 0)
        {
            a = a + 180;
        }
        if ((dx >= 0) && (dy < 0))
        {
            a = a + 360;
        }

        if (upIsZero)
        {
            return 90-a; 
        }
        else
        {
            return a;
        }
    }

    public static float FixAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle <= -180) angle += 360;
        return angle;
    }

    public static Vector2 RotateVector(Vector2 v, float degrees)
    {
        degrees = degrees * Mathf.PI / 180f;
        Vector2 result = new Vector2();
        result.x = v.x * Mathf.Cos(degrees) - v.y * Mathf.Sin(degrees);
        result.y = v.x * Mathf.Sin(degrees) + v.y * Mathf.Cos(degrees);
        return result;
    }

    public static Vector2 Perpendicular(Vector2 v)
    {
        return RotateVector(v,90);
    }

    public static Vector3 RotateVector(Vector3 v, float degrees)
    {
        degrees = degrees * Mathf.PI / 180f;
        Vector3 result = new Vector3();
        result.x = v.x * Mathf.Cos(degrees) - v.z * Mathf.Sin(degrees);
        result.z = v.x * Mathf.Sin(degrees) + v.z * Mathf.Cos(degrees);
        return result;
    }

    public static Vector3 RotateVector2(Vector3 v, float degrees)
    {
        degrees = degrees * Mathf.PI / 180f;
        Vector3 result = new Vector3();
        result.x = v.x * Mathf.Cos(degrees) - v.z * Mathf.Sin(degrees);
        result.y = v.x * Mathf.Sin(degrees) + v.y * Mathf.Cos(degrees);
        return result;
    }

    public static Vector3 Perpendicular(Vector3 v)
    {
        return RotateVector(v,90);
    }

    public static float Sin(float a)
    {
        return Mathf.Sin(a*Mathf.PI/180);
    }

    public static float Cos(float a)
    {
        return Mathf.Cos(a*Mathf.PI/180);
    }

    public static Vector2 ReflectVector(Vector2 move, Vector2 normal)
        
    {
        normal  = normal.normalized;

        return move - 2*(Vector2.Dot(move, normal))*normal;
    }

    public static Vector3 ReflectVector(Vector3 move, Vector3 normal)
    {
        normal  = normal.normalized;

        return move - 2*(Vector3.Dot(move, normal))*normal;
    }

    public class HitVector
    {
        public bool inside;
        public float time;
        public Vector2 position;
    }

    //    public static HitVector FindIntersection(Vector2 B1, Vector2 B2, Vector2 W1, Vector2 W2)
    //    {
    //
    //        HitVector hit = new HitVector();
    //
    //        float k1 = (B2.x-B1.x)/(B2.y-B1.y);
    //        float k2 = (W2.x-W1.x)/(W2.y-W1.y);
    //
    //        if (k1==k2) return hit;
    //
    //        float m1 = B1.y - (k1*B1.x);
    //        float m2 = W1.y - (k2*W1.x);
    //
    //        float y = (m1+m2);
    //
    ////        y = k1*x + m1
    ////        y = k2*x + m2
    //
    ////        y = x + 2
    ////        y = -x - 1
    ////
    ////        2y = 0 + 1
    ////
    ////        y = 1/2
    //      
    ////        x = y - 2
    ////        x = -1.5
    //          
    //        float x = (y - m1)/k1;
    //
    //        hit.position = new Vector2(x,y);
    //        Vector2 wall = (W2-W1);
    //        Vector2 hitDiff = (hit.position-W1);
    //        hit.time = Vector2.Dot(wall, hitDiff);
    //
    //        if (hit.time>=0 && hit.time<=1)
    //        {
    //            hit.inside = true;
    //        }
    //
    //        return hit;
    //    }

    //    bool IsIntersecting(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    //    {
    //        float denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));
    //        float numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
    //        float numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));
    //
    //        // Detect coincident lines (has a problem, read below)
    //        if (denominator == 0) return numerator1 == 0 && numerator2 == 0;
    //
    //        float r = numerator1 / denominator;
    //        float s = numerator2 / denominator;
    //
    //        return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
    //    }

    public static HitVector FindIntersection(Vector2 B1, Vector2 B2, Vector2 W1, Vector2 W2)
    {

        HitVector hit = new HitVector();

        float denominator = ((B2.x - B1.x) * (W2.y - W1.y)) - ((B2.y - B1.y) * (W2.x - W1.x));
        float numerator1 = ((B1.y - W1.y) * (W2.x - W1.x)) - ((B1.x - W1.x) * (W2.y - W1.y));
        float numerator2 = ((B1.y - W1.y) * (B2.x - B1.x)) - ((B1.x - W1.x) * (B2.y - B1.y));

        // Detect coincident lines (has a problem, read below)
        if (denominator == 0) return hit;

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        //  return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);


        hit.position = W1 + (W2 - W1) * s;
        hit.time = r;


        if (s >= 0 && s <= 1)
        {
            hit.inside = true;
        }

        return hit;
    }

    public static float PolygonArea(List<Vector2> vectors)
    {
        float area = 0;         // Accumulates area in the loop
        int j = vectors.Count - 1;  // The last vertex is the 'previous' one to the first

        for (int i = 0; i < vectors.Count; i++)
        {
            area = area + (vectors[j].x + vectors[i].x) * (vectors[j].y - vectors[i].y);
            j = i;  //j is previous vertex to i
        }
        return area / 2;
    }


    public static Vector2 ClosestPointonLine(float lx1, float ly1,
     float lx2, float ly2, float x0, float y0)
    {
        float A1 = ly2 - ly1;
        float B1 = lx1 - lx2;
        float C1 = ((ly2 - ly1) * lx1) + ((lx1 - lx2) * ly1);
        float C2 = (-B1 * x0) + (A1 * y0);
        float det = (A1 * A1) - (-B1 * B1);
        float cx = 0;
        float cy = 0;
        if (Mathf.Abs(det) > 0.00001f)
        {
            cx = ((A1 * C1 - B1 * C2) / det);
            cy = ((A1 * C2 - -B1 * C1) / det);
        }
        else
        {
            cx = x0;
            cy = y0;
        }
        return new Vector2(cx, cy);
    }

   

    public class Circle
    {
        public float x;
        public float y;
        public float vx;
        public float vy;
        public float radius;
        public bool didCollide;

        public Circle(float x, float y, float vx, float vy, float radius)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.radius = radius;
            didCollide = false;

        }
    }

    public static Circle DynamicCircleStaticCircleCollision(Circle circle1, Circle circle2)
    {
        Vector2 d = ClosestPointonLine(circle1.x, circle1.y, circle1.x + circle1.vx, circle1.y + circle1.vy, circle2.x, circle2.y);
        float closestdistsq = Mathf.Pow(circle2.x - d.x, 2) + Mathf.Pow(circle2.y - d.y, 2);
        if (closestdistsq <= Mathf.Pow(circle1.radius + circle2.radius, 2))
        {
            // a collision has occurred
            float backdist = Mathf.Sqrt(Mathf.Pow(circle1.radius + circle2.radius, 2) - closestdistsq);
            float movementvectorlength = Mathf.Sqrt(Mathf.Pow(circle1.vx, 2) + Mathf.Pow(circle1.vy, 2));
//            float c_x = d.x - backdist * (circle1.vx / movementvectorlength);
//            float c_y = d.y - backdist * (circle1.vy / movementvectorlength);
            circle1.x = d.x - backdist * (circle1.vx / movementvectorlength);
            circle1.y = d.y - backdist * (circle1.vy / movementvectorlength);
            circle1.didCollide = true;

            //double collisiondist = Math.sqrt(Math.pow(circle2.x - c_x, 2) + Math.pow(circle2.y - c_y, 2);
            //double n_x = (circle2.x - c_x) / collisiondist;
            //double n_y = (circle2.y - c_y) / collisiondist;
            //double p = 2 * (circle1.vx * n_x + circle1.vy * n_y) /
            //            (circle1.mass + circle2.mass);
            //double w_x = circle1.vx - p * circle1.mass * n_x - p * circle2.mass * n_x;
            //double w_y = circle2.vy - p * circle1.mass * n_y - p * circle2.mass * n_y;
        }
        else
        {
            // no collision has occurred
        }

		return circle1;
    }

    public static PosDir BallCollidePole(PosDir posDir, float ballRadius, PolePos polePos)
    {
        Circle circle1 = null;
        Circle circle2 = null;

        if (polePos.axis == Vector3.up)
        {
            circle1 = new Circle(posDir.pos.x, posDir.pos.z, posDir.dir.x, posDir.dir.z, ballRadius);
            circle2 = new Circle(polePos.pos.x, polePos.pos.z, 0, 0, polePos.radius);

            circle1 = DynamicCircleStaticCircleCollision(circle1, circle2);
            posDir.pos.x = circle1.x;
            posDir.pos.z = circle1.y;
            posDir.didCollide = circle1.didCollide;
        }
        else
        {
            circle1 = new Circle(posDir.pos.z, posDir.pos.y, posDir.dir.z, posDir.dir.y, ballRadius);
            circle2 = new Circle(polePos.pos.z, polePos.pos.y, 0, 0, polePos.radius);

            circle1 = DynamicCircleStaticCircleCollision(circle1, circle2);
            posDir.pos.z = circle1.x;
            posDir.pos.y = circle1.y;
            posDir.didCollide = circle1.didCollide;
        }



        return posDir;
    }
}

public class PosDir
{
    public Vector3 pos;
    public Vector3 dir;
    public bool didCollide;
    public PosDir(Vector3 pos, Vector3 dir)
    {
        this.pos = pos;
        this.dir = dir;
    }
}

public class PolePos
{
    public Vector3 pos;
    public Vector3 axis;
    public float height;
    public float radius;

    public PolePos(Vector3 pos, Vector3 axis, float height, float radius)
    {
        this.pos = pos;
        this.axis = axis;
        this.height = height;
        this.radius = radius;
    }
}
