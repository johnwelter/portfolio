package dataStructs;

public class Vector3D {
	
	public double x;
	public double y;
	public double z;
	
	public Vector3D()
	{
		x = 0.0;
		y = 0.0;
		z = 0.0;
	}
	
	public Vector3D(double x, double y, double z)
	{
		
		this.x = x;
		this.y = y;
		this.z = z;
		
	}
	
	public double dot (Vector3D B)
	{
		double dotx = x * B.x;
		double doty = y * B.y;
		double dotz = z * B.z;
		
		return dotx+doty+dotz;
		
	}
	
	public Vector3D cross (Vector3D B)
	{
		
		double crossx = (y*B.z)-(z*B.y);
		double crossy = (z*B.x)-(x*B.z);
		double crossz = (x*B.y)-(y*B.x);
		
		return new Vector3D(crossx, crossy, crossz);
		
	}
	public Vector3D scale(double s)
	{
		double scalex = x*s;
		double scaley = y*s;
		double scalez = z*s;
		
		return new Vector3D(scalex, scaley, scalez);
		
	}
	public double magnitude()
	{
		
		return Math.sqrt((x*x) + (y*y) + (z*z)); 
		
	}
	public Vector3D normalize()
	{
		if(this.magnitude() != 0)
			return new Vector3D (x/this.magnitude(), y/this.magnitude(), z/this.magnitude());
		else
			return new Vector3D();
		
	}
	public String toString()
	{
		
		return  "(" + Math.round(x*1000.0)/1000.0 + ", " + Math.round(y*1000.0)/1000.0 + ", " + Math.round(z*1000.0)/1000.0 + ")";	
	}

}
