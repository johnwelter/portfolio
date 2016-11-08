package rockyDataStructs;

public class RockyQuat {

	
	public double scale;
	public RockyVector vector;
	
	public RockyQuat()
	{
		scale = 0.0;
		vector = new RockyVector();
	}
	public RockyQuat(double s, RockyVector v)
	{
		scale = s;
		vector = v;
	}

	public String toString()
	{
		
		return "(" + scale + ", " + vector.toString() + ")";
		
	}
	
	/**
	 * Rotates quaternion around an arbitrary vector.  
	 * @param angle of rotation
	 * @param u Arbitary vector
	 * @return 
	 * RockyVector of new point location
	 */
	public RockyVector rotate(double angle, RockyVector u)
	{
		double theta = (angle*Math.PI)/180;
		
		double s = 0;
		RockyVector v = new RockyVector ();
		
		RockyQuat qp = new RockyQuat();
		RockyQuat q = new RockyQuat();
		
		RockyQuat qqp = new RockyQuat();
		RockyQuat qin = new RockyQuat();
		
		RockyVector pp = new RockyVector();
		
		//initial quaternion setup
		qp = new RockyQuat(scale, vector);
				
		RockyVector uNormal = u.normalize();
				
		System.out.println("uNormal = " + uNormal.toString() + "\n");
				
		s = Math.cos(theta/2);
		v.x = Math.sin(theta/2) * uNormal.x;
		v.y = Math.sin(theta/2) * uNormal.y;
		v.z = Math.sin(theta/2) * uNormal.z;
				
		q = new RockyQuat(s, v);
				
		System.out.println("initial quaternions");
		System.out.print("qp = " + qp.toString() + "\nq = " + q.toString() + "\n");
		System.out.println("");
				
		//find cross product for quaternion math
		RockyVector qqpcross = v.cross(vector);
				
		System.out.println("cross product of v and p, as well as the resulting quaternion");
		System.out.println("qqpcross = " + qqpcross.toString());
				
		//quaternion math for finding qqp
		qqp.scale= (qp.scale * q.scale) - (vector.dot(v));
		qqp.vector.x = (s*vector.x)+(qp.scale*v.x)+(qqpcross.x);		
		qqp.vector.y = (s*vector.y)+(qp.scale*v.y)+(qqpcross.y);	
		qqp.vector.z = (s*vector.z)+(qp.scale*v.z)+(qqpcross.z);
				
		System.out.println("qqp = " + qqp.toString());
		System.out.println("");
				
		qin = new RockyQuat(q.scale, q.vector.scale(-1.0));
				
		//find cross product for quaternion math
		RockyVector ppcross = qqp.vector.cross(qin.vector);
				
		System.out.println("cross product of qqp and qin, as well as the resulting point");
		System.out.println("ppcross = " + ppcross.toString());
				
		//quaternion math for finding qqpqin, or the final point
		pp.x = (qqp.scale*qin.vector.x)+(qin.scale*qqp.vector.x)+(ppcross.x);
		pp.y = (qqp.scale*qin.vector.y)+(qin.scale*qqp.vector.y)+(ppcross.y);
		pp.z = (qqp.scale*qin.vector.z)+(qin.scale*qqp.vector.z)+(ppcross.z);
				
		System.out.println("pp = " + pp.toString());

		return pp;

	}
}
