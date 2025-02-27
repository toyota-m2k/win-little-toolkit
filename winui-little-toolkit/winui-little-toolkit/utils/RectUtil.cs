using Windows.Foundation;

namespace io.github.toyota32k.toolkit.winui.utils;
public struct Vector {
    float _dx;
    float _dy;
    public double X {
        get {
            return _dx;
        }
        set {
            _dx = (float)value;
        }
    }
    public double Y {
        get {
            return _dy;
        }
        set {
            _dy = (float)value;
        }
    }
    public Vector(float x, float y) {
        _dx = x;
        _dy = y;
    }
    public Vector(double x, double y)
        : this((float)x, (float)y) {
    }
    public static Vector operator *(Vector v, double scalar) {
        return new Vector(v.X * scalar, v.Y * scalar);
    }

    public static Vector operator *(double scalar, Vector v) {
        return new Vector(v.X * scalar, v.Y * scalar);
    }

    public static Vector operator /(Vector v, double scalar) {
        return new Vector(v.X / scalar, v.Y / scalar);
    }
    public static Vector operator +(Vector v1, Vector v2) {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y);
    }
    public static Vector operator -(Vector v1, Vector v2) {
        return new Vector(v1.X - v2.X, v1.Y - v2.Y);
    }
    public static Vector operator -(Vector v) {
        return new Vector(-v.X, -v.Y);
    }
    //public static Vector operator +(Vector v, Point p) {
    //    return new Vector(v.X + p.X, v.Y + p.Y);
    //}
    public static Point operator +(Point p, Vector v) {
        return new Point(v.X + p.X, v.Y + p.Y);
    }
    //public static Vector operator -(Vector v, Point p) {
    //    return new Vector(v.X - p.X, v.Y - p.Y);
    //}
    public static Point operator -(Point p, Vector v) {
        return new Point(p.X - v.X, p.Y - v.Y);
    }
}

public static class RectUtil {
    public static double CenterX(this Rect r) {
        return r.Left + r.Width / 2;
    }
    public static double CenterY(this Rect r) {
        return r.Top + r.Height / 2;
    }
    public static Point Center(this Rect r) {
        return new Point(r.CenterX(), r.CenterY());
    }
    public static Point LeftTop(this Rect r) {
        return new Point(r.Left, r.Top);
    }
    public static Point RightTop(this Rect r) {
        return new Point(r.Right, r.Top);
    }
    public static Point LeftBottom(this Rect r) {
        return new Point(r.Left, r.Bottom);
    }
    public static Point RightBottom(this Rect r) {
        return new Point(r.Right, r.Bottom);
    }
    public static Size Size(this Rect r) {
        return new Size(r.Width, r.Height);
    }
    public static Rect Size(this Rect r, Size size) {
        r.Width = size.Width;
        r.Height = size.Height;
        return r;
    }

    public static Vector Minus(this Point p, Point p2) {
        return new Vector(p.X - p2.X, p.Y - p2.Y);
    }
    
    public static Point Plus(this Point p, double dx, double dy) {
        return new Point(p.X + dx, p.Y + dy);
    }
    
    public static Point Plus(this Point p, Vector v) {
        return p + v;
    }


    public static Rect Move(this Rect r, double dx, double dy) {
        r.X += dx;
        r.Y += dy;
        return r;
    }

    public static Rect Move(this Rect r, Vector v) {
        return r.Move(v.X, v.Y);
    }
    public static Rect MoveLTTo(this Rect r, Point p) {
        r.X = p.X;
        r.Y = p.Y;
        return r;
    }
    public static Rect MoveRBTo(this Rect r, Point p) {
        return r.Move(p.Minus(r.RightBottom()));
    }
    public static Rect MoveCenterTo(this Rect r, Point p) {
        return r.Move(p.Minus(r.Center()));
    }

    public static Size Zoom(this Size s, double zoom) {
        s.Width *= zoom;
        s.Height *= zoom;
        return s;
    }

    public static Rect Zoom(this Rect r, double zoom, Point pivot) {
        r.Size(r.Size().Zoom(zoom));
        var v = r.LeftTop().Minus(pivot);
        return r.MoveLTTo(pivot + v*zoom);
    }
}
